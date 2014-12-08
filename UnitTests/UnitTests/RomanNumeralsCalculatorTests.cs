using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumerals.ApplicationService;
using RomanNumeralsCalculator.Factories;
using RomanNumeralsCalculator.Domain_Service.Value_Objects;
using RomanNumeralsCalculator.Domain_Service;
using System.Globalization;
using RomanNumeralsCalculator.Exceptions;

namespace UnitTests
{
	[TestClass]
	public class RomanNumeralsCalculatorTests
	{
		[TestMethod]
		[ExpectedException(typeof(RomanNumeralsCalculatorException))]
		public void aa_zero()
		{	
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(0);			
		}


		[TestMethod]
		public void a_one()
		{
			var expected = "I";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(1);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void b_three()
		{
			var expected = "III";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(3);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void c_four()
		{
			var expected = "IV";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(4);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void d_five()
		{
			var expected = "V";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(5);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void e_six()
		{
			var expected = "VI";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(6);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void f_eight()
		{
			var expected = "VIII";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(8);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void g_nine()
		{
			var expected = "IX";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(9);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void h_ten()
		{
			var expected = "X";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(10);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void i_nineteen()
		{
			var expected = "XIX";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(19);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void j_twenty()
		{
			var expected = "XX";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(20);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void k_fortynine()
		{
			var expected = "XLIX";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(49);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void k_fiftyone()
		{
			var expected = "LI";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(51);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void k_ninetynine()
		{
			var expected = "XCIX";	// You can only subtract from the next two highest numbers, so we cannot subtract I from C
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(99);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void l_fourhundredandone()
		{
			var expected = "CDI";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(401);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void l_ninehundredandninety()
		{
			var expected = "CMXC";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(990);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void m_tensixtysix()
		{
			var expected = "MLXVI";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(1066);
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void n_threethousand()
		{
			var expected = "MMM";
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(3000);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(RomanNumeralsCalculatorException))]
		public void aa_threethousandandone()
		{
			var calculator = new RomanNumeralsCalculatorFactory().GetNumeralsCalculator();
			var actual = calculator.GetFullNumeralFor(3001);
		}


		#region base ten exponent finder
		[TestMethod]
		public void one_to_nine()
		{
			var baseFinder = new BaseTenExponentFinder();
			var expected = 0;

			var failedInts = "";

			for (int i = 0; i < 10; i++)
			{
				var actual = baseFinder.GetExponentFrom(i);
				if (expected != actual)
				{
					failedInts = String.Format("{0}, {1}", failedInts, i.ToString(CultureInfo.InvariantCulture));
				}
			}
			Assert.AreEqual(String.Empty, failedInts);
		}

		[TestMethod]
		public void ten_to_ninetynine()
		{
			var baseFinder = new BaseTenExponentFinder();
			var expected = 1;

			var firstFailedInt = "";

			for (int i = 10; i < 100; i++)
			{
				var actual = baseFinder.GetExponentFrom(i);
				if (expected != actual)
				{
					firstFailedInt = String.Format("{0}, {1}", firstFailedInt, i.ToString(CultureInfo.InvariantCulture));
					break;
				}
			}
			Assert.AreEqual(String.Empty, firstFailedInt);
		}

		[TestMethod]
		public void onehundred_to_ninehundredandninetynine()
		{
			var baseFinder = new BaseTenExponentFinder();
			var expected = 2;

			var fistFailedInt = "";

			for (int i = 100; i < 1000; i++)
			{
				var actual = baseFinder.GetExponentFrom(i);
				if (expected != actual)
				{
					fistFailedInt = String.Format("{0}, {1}", fistFailedInt, i.ToString(CultureInfo.InvariantCulture));
					break;
				}
			}
			Assert.AreEqual(String.Empty, fistFailedInt);
		}

		[TestMethod]
		public void onethousand_to_ninetyninethousand()
		{
			var baseFinder = new BaseTenExponentFinder();
			var expected = 3;

			var firstFailedInt = "";

			for (int i = 1000; i < 10000; i++)
			{
				var actual = baseFinder.GetExponentFrom(i);
				if (expected != actual)
				{
					firstFailedInt = String.Format("{0}, {1}", firstFailedInt, i.ToString(CultureInfo.InvariantCulture));
					break;
				}
			}
			Assert.AreEqual(String.Empty, firstFailedInt);
		}
		#endregion
	}
}
