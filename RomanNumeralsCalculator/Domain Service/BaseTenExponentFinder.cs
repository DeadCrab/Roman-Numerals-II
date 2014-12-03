using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumeralsCalculator.Domain_Service
{
	public class BaseTenExponentFinder
	{
		public byte GetExponentFrom(int i)
		{
			var exponent = (byte)Math.Floor(Math.Log10(i));
			return exponent;
		}
	}
}