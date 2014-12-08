using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RomanNumerals.ApplicationService;

namespace RomanNumeralsCalculator.Domain_Service.Value_Objects
{
	public interface ICharacterSet
	{
		RomanCharacter GetRomanCharacterFor(int number);

		/// <summary>
		/// You can only subtract I from V and X
		/// You can only subtract X from L and C
		/// And so on...		
		/// </summary>		
		/// <returns></returns>
		RomanCharacter GetRomanCharacterThatCanBePrepended();
	}


	public abstract class RomanCharacterSet : ICharacterSet
	{
		protected byte _divisor;
		protected RomanCharacter _baseChar;
		protected RomanCharacter _midChar;
		protected RomanCharacter _topChar;

		public virtual RomanCharacter GetRomanCharacterFor(int number)
		{
			RomanCharacter romanNumeralSegment = _baseChar;
			var numberToCheck = number / _divisor;

			if (numberToCheck >= 4 && numberToCheck < 9)
				romanNumeralSegment = _midChar;

			if (numberToCheck >= 9)
				romanNumeralSegment = _topChar;

			return romanNumeralSegment;
		}

		public RomanCharacter GetRomanCharacterThatCanBePrepended()
		{
			return _baseChar;
		}
	}


	public class RomanCharacterSetForExponentZero : RomanCharacterSet
	{
		public RomanCharacterSetForExponentZero()
		{
			_divisor = 1;
			_baseChar = new RomanCharacter("I", 1);
			_midChar = new RomanCharacter("V", 5);
			_topChar = new RomanCharacter("X", 10);
		}
	}


	public class RomanCharacterSetForExponentOne : RomanCharacterSet
	{
		public RomanCharacterSetForExponentOne()
		{
			_divisor = 10;
			_baseChar = new RomanCharacter("X", 10);
			_midChar = new RomanCharacter("L", 50);
			_topChar = new RomanCharacter("C", 100);
		}
	}

	public class RomanCharacterSetForExponentTwo : RomanCharacterSet
	{
		public RomanCharacterSetForExponentTwo()
		{
			_divisor = 100;
			_baseChar = new RomanCharacter("C", 100);
			_midChar = new RomanCharacter("D", 500);
			_topChar = new RomanCharacter("M", 1000);
		}
	}
}