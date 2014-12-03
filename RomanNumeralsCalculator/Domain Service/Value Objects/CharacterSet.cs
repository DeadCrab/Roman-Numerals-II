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
		/// This method gives you the character that relates to the romanCharacter you pass in
		/// </summary>
		/// <param name="romanNumeral"></param>
		/// <returns></returns>
		RomanCharacter GetRomanCharacterThatCanBePrependedTo(String baseRomanNumeral);
	}


	public class RomanCharacterSetForExponentZero : ICharacterSet
	{
		public RomanCharacter GetRomanCharacterFor(int number)
		{
			RomanCharacter romanNumeralSegment = new RomanCharacter("I", 1);

			switch (number)
			{
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					romanNumeralSegment = new RomanCharacter("V", 5);
					break;
				case 9:
				case 10:								
					romanNumeralSegment = new RomanCharacter("X", 10);
					break;

			}

			return romanNumeralSegment;

		}

		public RomanCharacter GetRomanCharacterThatCanBePrependedTo(String baseRomanNumeral)
		{	
			return new RomanCharacter("I", 1);
		}
	}


	public class RomanCharacterSetForExponentOne : ICharacterSet
	{
		public RomanCharacter GetRomanCharacterFor(int number)
		{
			RomanCharacter romanNumeralSegment = new RomanCharacter("X", 10);

			if(number > 39 && number < 90)
				romanNumeralSegment = new RomanCharacter("L", 50);

			if(number > 89)
				romanNumeralSegment = new RomanCharacter("C", 100);
			
			return romanNumeralSegment;
		}

		public RomanCharacter GetRomanCharacterThatCanBePrependedTo(String baseRomanNumeral)
		{
			return new RomanCharacter("X", 10);
		}
	}

	public class RomanCharacterSetForExponentTwo : ICharacterSet
	{
		public RomanCharacter GetRomanCharacterFor(int number)
		{
			RomanCharacter romanNumeralSegment = new RomanCharacter("C", 100);

			if (number > 399 && number < 900)
				romanNumeralSegment = new RomanCharacter("D", 500);

			if (number > 899)
				romanNumeralSegment = new RomanCharacter("M", 1000);


			return romanNumeralSegment;

		}

		public RomanCharacter GetRomanCharacterThatCanBePrependedTo(String baseRomanNumeral)
		{
			return new RomanCharacter("C", 100);
		}
	}
}


