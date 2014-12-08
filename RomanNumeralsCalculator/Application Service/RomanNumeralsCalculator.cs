using System;
using RomanNumeralsCalculator.Domain_Service;
using RomanNumeralsCalculator.Domain_Service.Value_Objects;
using RomanNumeralsCalculator.Exceptions;
using RomanNumeralsCalculator.Factories;

namespace RomanNumerals.ApplicationService
{
	public class RomanCharacter
	{
		private String _romanNumeral;
		private int _arabicNumber;

		public RomanCharacter(String romanNumeral, int arabicNumber)
		{
			_romanNumeral = romanNumeral;
			_arabicNumber = arabicNumber;
		}

		public String RomanNumeral { get { return _romanNumeral; } }
		public int ArabicNumber { get { return _arabicNumber; } }
	}


	public interface INumeralsCalculator
	{
		String GetFullNumeralFor(int arabicNumeral);
	}


	public class RomanNumeralsCalculator : INumeralsCalculator
	{
		private CharacterSetFactory _characterLookupFactory;
		private BaseTenExponentFinder _exponentFinder;
		private byte _tensHundredsEtc;
		private byte _previousExponent;
		private BuildMode _mode;
		private String _completeNumeral;
		private String _subNumeral;
		private int _remainder;
		private int _subSectionRemainder;
		private PrependStatus _prependStatus;

		public RomanNumeralsCalculator(CharacterSetFactory characterLookupFactory, BaseTenExponentFinder exponentFinder)
		{
			_characterLookupFactory = characterLookupFactory;
			_exponentFinder = exponentFinder;
		}


		/// <summary>
		/// Converts arabic number into roman numeral string
		/// I	V	X	L	C	D	M
		/// </summary>
		/// <param name="arabicNumeral"></param>
		/// <returns></returns>
		public String GetFullNumeralFor(int arabicNumeral)
		{
			_tensHundredsEtc = _exponentFinder.GetExponentFrom(arabicNumeral);
			_previousExponent = 100;
			_mode = BuildMode.AppendCharToMainString;
			_completeNumeral = String.Empty;
			_subNumeral = String.Empty;
			_remainder = arabicNumeral;
			_prependStatus = PrependStatus.Pending;
			ICharacterSet characterSet = _characterLookupFactory.GetCharacterSetFor(_tensHundredsEtc);
			bool onFirstIteration = true;
			_subSectionRemainder = 0;


			try
			{
				if (arabicNumeral <= 0 || arabicNumeral > 3000)
					throw new RomanNumeralsCalculatorException("Only numbers between 1 and 3000 inclusive are supported");

				while (_remainder > 0)
				{
					_tensHundredsEtc = _exponentFinder.GetExponentFrom(_remainder);		// We can use the exponent to work out which section of the number we are dealing with e.g. 100's, 10's, 1's
					if (!onFirstIteration && WeHaveMovedFromOneSectionOfTheNumberToTheNext())
					{
						UpdatePreviousExponent();
						characterSet = _characterLookupFactory.GetCharacterSetFor(_tensHundredsEtc);
					}

					if (NotStillPrependingFromPreviousIteration())
						DeriveBuildModeToUse();

					if (_mode == BuildMode.BuildSubSectionByPrepending)
					{
						BuildSubSectionByPrepending(characterSet);
					}
					else
					{
						SimplyAppendCharacterToMainString(characterSet);
					}
					onFirstIteration = false;
				}
			}
			catch (RomanNumeralsCalculatorException ex)
			{
				throw ex;
			}

			catch (Exception ex)
			{
				// Wrap exceptions in our class so it is clear where the error occurred
				throw new RomanNumeralsCalculatorException("An error occurred", ex);
			}

			return _completeNumeral;
		}


		private void BuildSubSectionByPrepending(ICharacterSet characterSet)
		{
			RomanCharacter romanCharacterToAdd;
			if (ProcessingingFirstCharOfSubSection())
				romanCharacterToAdd = characterSet.GetRomanCharacterFor(_remainder);
			else
				romanCharacterToAdd = characterSet.GetRomanCharacterThatCanBePrepended();

			PrependToSubNumeral(romanCharacterToAdd);

			if (_prependStatus.Equals(PrependStatus.SecondCharacterProcessed))
			{
				AddAnySubSectionJustProcessedToResult();
				_mode = BuildMode.AppendCharToMainString;
				_prependStatus = PrependStatus.Pending;
			}
		}


		private void SimplyAppendCharacterToMainString(ICharacterSet characterSet)
		{
			var romanCharacterToAdd = characterSet.GetRomanCharacterFor(_remainder);
			UpdateMainStringWith(romanCharacterToAdd);
		}



		private void PrependToSubNumeral(RomanCharacter romanCharacter)
		{
			if (ProcessingFirstCharacterOfSubNumeral())
				_subSectionRemainder = romanCharacter.ArabicNumber;
			else
			{
				_remainder -= (_subSectionRemainder - romanCharacter.ArabicNumber);
				_subSectionRemainder = 0;
			}
			_subNumeral = romanCharacter.RomanNumeral + _subNumeral;

			UpdatePrependStatus();
		}

		private void UpdatePrependStatus()
		{
			switch (_prependStatus)
			{
				case PrependStatus.Pending:
					_prependStatus = PrependStatus.FirstCharacterProcessed;
					break;
				case PrependStatus.FirstCharacterProcessed:
					_prependStatus = PrependStatus.SecondCharacterProcessed;
					break;

				default:
					break;
			}
		}


		private void UpdateMainStringWith(RomanCharacter romanCharacter)
		{
			_completeNumeral += romanCharacter.RomanNumeral;
			_remainder -= romanCharacter.ArabicNumber;
		}

		private void AddAnySubSectionJustProcessedToResult()
		{
			_completeNumeral += _subNumeral;
			_subNumeral = String.Empty;
		}

		private void UpdatePreviousExponent()
		{
			_previousExponent = _tensHundredsEtc;
		}




		private void DeriveBuildModeToUse()
		{
			switch (_mode)
			{
				case BuildMode.AppendCharToMainString:
					RecalculateBuildModeBasedOnRemainder();
					break;

				case BuildMode.BuildSubSectionByPrepending:
					if (_prependStatus.Equals(PrependStatus.SecondCharacterProcessed))
					{
						_prependStatus = PrependStatus.Pending;
						_mode = BuildMode.AppendCharToMainString;
					}
					break;
				default:
					break;
			}
		}


		private void RecalculateBuildModeBasedOnRemainder()
		{
			var exponent = _tensHundredsEtc;
			var divisor = Math.Pow(10, exponent);
			var ratio = _remainder / divisor;
			var coefficient = Math.Floor(ratio);
			if (coefficient == 4 || coefficient == 9)
			{
				_mode = BuildMode.BuildSubSectionByPrepending;
			}
			else
			{
				_mode = BuildMode.AppendCharToMainString;
			}
		}

		#region english language helper methods
		private bool WeHaveMovedFromOneSectionOfTheNumberToTheNext()
		{
			return _tensHundredsEtc != _previousExponent;
		}

		private bool ProcessingingFirstCharOfSubSection()
		{
			return String.IsNullOrEmpty(_subNumeral);
		}


		private bool ProcessingFirstCharacterOfSubNumeral()
		{
			return String.IsNullOrEmpty(_subNumeral);
		}

		private bool NotStillPrependingFromPreviousIteration()
		{
			return !_mode.Equals(BuildMode.BuildSubSectionByPrepending) || _prependStatus.Equals(PrependStatus.SecondCharacterProcessed);
		}
		#endregion

		#region enums
		private enum BuildMode
		{
			AppendCharToMainString = 0,
			BuildSubSectionByPrepending = 1
		}

		private enum PrependStatus
		{
			Pending = 0,
			FirstCharacterProcessed = 1,
			SecondCharacterProcessed = 2
		}
		#endregion
	}
}
