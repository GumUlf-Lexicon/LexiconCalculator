using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace LexiconCalculator.Tests
{
	public class LexiconCalculatorShould
	{

		[Theory]
		[InlineData(0.0, 0.0)]
		[InlineData(1.0, 2.0)]
		[InlineData(-10.43, 33.62356)]
		[InlineData(-5.75355e-43, 7.1672352e-37)]
		[InlineData(5134, -653.43)]
		[InlineData(double.PositiveInfinity, 0.0)]
		[InlineData(double.NaN, 0)]
		[InlineData(double.PositiveInfinity, double.PositiveInfinity)]
		[InlineData(double.PositiveInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NaN)]
		[InlineData(double.PositiveInfinity, double.NaN)]
		[InlineData(double.NegativeInfinity, double.NaN)]
		[InlineData(double.NaN, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NegativeInfinity)]
		public void BeAbleToAddTwoNumbers(double number1, double number2)
		{

			double expectedSum = number1 + number2;
			double returnedSum = Calculator.Addition(number1, number2);

			Assert.Equal(expectedSum, returnedSum, 10);
		}

		[Fact]
		public void BeAbleToAddAnArrayOfNumbers()
		{
			double[] numbers = { 32.5, 63, -90.4 };

			double expectedSum = 0;
			foreach(double number in numbers)
			{
				expectedSum += number;
			}

			double returendSum = Calculator.Addition(numbers);

			Assert.Equal(5.1, returendSum, 10);
		}

		[Fact]
		public void ReturnExceptionWhenTryingToAddAnEmptyArryOfNumbers()
		{
			double[] numbers = { };
			string expectedText = "The array numbers must contain at least one number!";

			var result = Assert.Throws<ArgumentException>(() => Calculator.Addition(numbers));

			Assert.Equal(expectedText, result.Message);
		}

		[Theory]
		[InlineData(0.0, 0.0)]
		[InlineData(1.0, 2.0)]
		[InlineData(-10.43, 33.62356)]
		[InlineData(-5.75355e-43, 7.1672352e-37)]
		[InlineData(5134, -653.43)]
		[InlineData(double.PositiveInfinity, 0.0)]
		[InlineData(double.NaN, 0)]
		[InlineData(double.PositiveInfinity, double.PositiveInfinity)]
		[InlineData(double.PositiveInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NaN)]
		[InlineData(double.PositiveInfinity, double.NaN)]
		[InlineData(double.NegativeInfinity, double.NaN)]
		[InlineData(double.NaN, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NegativeInfinity)]
		public void BeAbleToSubtractTwoNumbers(double number1, double number2)
		{

			double expectedDiff = number1 - number2;
			double returnedDiff = Calculator.Subtraction(number1, number2);

			Assert.Equal(returnedDiff, expectedDiff, 10);
		}

		[Fact]
		public void BeAbleToSubstractAnArrayOfNumbers()
		{
			double[] numbers = { 32.5, 63, -90.4 };

			double expectedDiff = numbers[0];
			for(int i = 1; i < numbers.Length; i++)
			{
				expectedDiff -= numbers[i];
			}

			double returnedDiff = Calculator.Subtraction(numbers);

			Assert.Equal(expectedDiff, returnedDiff, 10);
		}

		[Fact]
		public void ReturnExceptionWhenTryingToSubtractAnEmptyArryOfNumbers()
		{
			double[] numbers = { };
			string expectedText = "The array numbers must contain at least one number!";

			var result = Assert.Throws<ArgumentException>(() => Calculator.Subtraction(numbers));

			Assert.Equal(expectedText, result.Message);
		}

		[Theory]
		[InlineData(0.0, 0.0)]
		[InlineData(1.0, 2.0)]
		[InlineData(-10.43, 33.62356)]
		[InlineData(-5.75355e-43, 7.1672352e-37)]
		[InlineData(5134, -653.43)]
		[InlineData(double.PositiveInfinity, 0.0)]
		[InlineData(double.NaN, 0)]
		[InlineData(double.PositiveInfinity, double.PositiveInfinity)]
		[InlineData(double.PositiveInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NaN)]
		[InlineData(double.PositiveInfinity, double.NaN)]
		[InlineData(double.NegativeInfinity, double.NaN)]
		[InlineData(double.NaN, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NegativeInfinity)]
		public void BeAbleToMultiplyTwoNumbers(double number1, double number2)
		{
			double expectedResult = number1 * number2;

			double returnedResult = Calculator.Multiplication(number1, number2);

			Assert.Equal(expectedResult, returnedResult);
		}



		[Theory]
		[InlineData(1.0, 2.0)]
		[InlineData(-10.43, 33.62356)]
		[InlineData(-5.75355e-43, 7.1672352e-37)]
		[InlineData(5134, -653.43)]
		[InlineData(double.PositiveInfinity, double.PositiveInfinity)]
		[InlineData(double.PositiveInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NaN)]
		[InlineData(double.PositiveInfinity, double.NaN)]
		[InlineData(double.NegativeInfinity, double.NaN)]
		[InlineData(double.NaN, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NegativeInfinity)]
		public void BeAbleToDivideTwoNumbers(double number1, double number2)
		{
			double expectedResult = number1 / number2;

			double returnedResult = Calculator.Division(number1, number2);

			Assert.Equal(expectedResult, returnedResult);
		}

		// The Division method returns NaN, positive or negative infinity
		// on division by zero, not an exception. This is tested here.
		[Fact]
		public void NotCrashOnDivisionWithZero()
		{
			double posDividend = 3, negDividend = -3, zeroDividend = 0, divisor = 0;

			double returnedPosResult = Calculator.Division(posDividend, divisor);
			double returnedNegResult = Calculator.Division(negDividend, divisor);
			double returnedZeroResult = Calculator.Division(zeroDividend, divisor);

			Assert.Equal(double.PositiveInfinity, returnedPosResult);
			Assert.Equal(double.NegativeInfinity, returnedNegResult);
			Assert.Equal(double.NaN, returnedZeroResult);

		}

		[Theory]
		[InlineData(1.0, 2.0)]
		[InlineData(-10.43, 33.62356)]
		[InlineData(-5.75355e-43, 7.1672352e-37)]
		[InlineData(5134, -653.43)]
		[InlineData(double.PositiveInfinity, double.PositiveInfinity)]
		[InlineData(double.PositiveInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.NegativeInfinity)]
		[InlineData(double.NegativeInfinity, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NaN)]
		[InlineData(double.PositiveInfinity, double.NaN)]
		[InlineData(double.NegativeInfinity, double.NaN)]
		[InlineData(double.NaN, double.PositiveInfinity)]
		[InlineData(double.NaN, double.NegativeInfinity)]
		public void BeAbleToRiseANumberToThePowerOfAnother(double number1, double number2)
		{
			double expectedResult = Math.Pow(number1, number2);

			double returnedResult = Calculator.PowerOf(number1, number2);

			Assert.Equal(expectedResult, returnedResult);
		}




		// The following tests are bonus tests
		[Fact]
		public void HandleUserThatWantsToDividePositiveByZero()
		{
			double dividend = 3, divisor = 0;

			var output = new StringWriter();
			Console.SetOut(output);

			StringReader input = new StringReader($"{dividend}{Environment.NewLine}{divisor}{Environment.NewLine}");
			Console.SetIn(input);

			string expectedOutput = Environment.NewLine + "Division" + Environment.NewLine + Environment.NewLine + "Enter the dividend: " + "Enter the divisor: " + Environment.NewLine + "3 / 0 = +Inf" + Environment.NewLine;

			Calculator.Divide();

			Assert.Equal(expectedOutput, output.ToString());
		}

		[Fact]
		public void HandleUserThatWantsToDivideNegativeByZero()
		{
			double dividend = -3, divisor = 0;

			var output = new StringWriter();
			Console.SetOut(output);

			StringReader input = new StringReader($"{dividend}{Environment.NewLine}{divisor}{Environment.NewLine}");
			Console.SetIn(input);

			string expectedOutput = Environment.NewLine + "Division" + Environment.NewLine + Environment.NewLine + "Enter the dividend: " + "Enter the divisor: " + Environment.NewLine + "-3 / 0 = -Inf" + Environment.NewLine;

			Calculator.Divide();

			Assert.Equal(expectedOutput, output.ToString());
		}

		[Fact]
		public void HandleUserThatWantsToDivideZeroByZero()
		{
			double dividend = 0, divisor = 0;

			var output = new StringWriter();
			Console.SetOut(output);

			StringReader input = new StringReader($"{dividend}{Environment.NewLine}{divisor}{Environment.NewLine}");
			Console.SetIn(input);

			string expectedOutput = Environment.NewLine + "Division" + Environment.NewLine + Environment.NewLine + "Enter the dividend: " + "Enter the divisor: " + Environment.NewLine + "0 / 0 = NaN" + Environment.NewLine;

			Calculator.Divide();

			Assert.Equal(expectedOutput, output.ToString());
		}



	}

}
