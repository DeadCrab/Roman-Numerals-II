using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RomanNumerals.ApplicationService;
using RomanNumeralsCalculator.Domain_Service;
using RomanNumeralsCalculator.Domain_Service.Value_Objects;

namespace RomanNumeralsCalculator.Factories
{

	public interface INumeralsCalculatorFactory
	{
		INumeralsCalculator GetNumeralsCalculator();
	}


	public class RomanNumeralsCalculatorFactory : INumeralsCalculatorFactory
	{
		public INumeralsCalculator GetNumeralsCalculator()
		{
			return new RomanNumerals.ApplicationService.RomanNumeralsCalculator(new CharacterSetFactory(), new BaseTenExponentFinder());
		}
	}
}
