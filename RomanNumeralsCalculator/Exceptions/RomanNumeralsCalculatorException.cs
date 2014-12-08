using System;

namespace RomanNumeralsCalculator.Exceptions
{
	public class RomanNumeralsCalculatorException : Exception
	{

		public RomanNumeralsCalculatorException()
		{
		}

		public RomanNumeralsCalculatorException(string message)
			: base(message)
		{
		}

		public RomanNumeralsCalculatorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
