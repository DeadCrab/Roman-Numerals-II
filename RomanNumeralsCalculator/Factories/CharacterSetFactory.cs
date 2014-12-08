using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomanNumeralsCalculator.Domain_Service.Value_Objects;

namespace RomanNumeralsCalculator.Factories
{
	public class CharacterSetFactory
	{
		public ICharacterSet GetCharacterSetFor(byte exponent)
		{
			if (exponent == 0)
				return new RomanCharacterSetForExponentZero();

			if (exponent == 1)
				return new RomanCharacterSetForExponentOne();

			return new RomanCharacterSetForExponentTwo();
		}
	}
}
