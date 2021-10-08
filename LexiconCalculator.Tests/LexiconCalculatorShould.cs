using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Xunit;


namespace LexiconCalculator.Tests
{
	public class LexiconCalculatorShould
	{
		[Fact]
		public void BeAbleToAddTwoNumbers()
		{
			double number1 = 54.3, number2 = 3.27;

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
		public void BeAbleToSubtractTwoNumbers()
		{
			double number1 = 64.3, number2 = 44.3;

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
		public void ReturnExceptionWhenTryingToAddAnEmptyArryOfNumbers()
		{
			double[] numbers = { };
			string expectedText = "The array numbers must contain at least one number!";

			var result = Assert.Throws<ArgumentException>(() => Calculator.Addition(numbers));

			Assert.Equal(expectedText, result.Message);
		}


		[Fact]
		public void ReturnExceptionWhenTryingToSubtractAnEmptyArryOfNumbers()
		{
			double[] numbers = { };
			string expectedText = "The array numbers must contain at least one number!";

			var result = Assert.Throws<ArgumentException>(() => Calculator.Subtraction(numbers));

			Assert.Equal(expectedText, result.Message);


		}

		[Fact]
		public void NotCrashOnDivisionWithZero()
		{
			double dividend = 3, divisor = 0;

			var output = new StringWriter();
			Console.SetOut(output);

			var input = new StringReader($"{dividend}{Environment.NewLine}{divisor}");
			Console.SetIn(input);

			string expectedOutput = Environment.NewLine + "Division" + Environment.NewLine + Environment.NewLine + "Enter the dividend: " + "Enter the divisor: " + Environment.NewLine + "Error: You cannot divide by zero!" + Environment.NewLine;

			Calculator.Divide();

			Assert.Equal(expectedOutput, output.ToString());

		}

	}

}
