using System;
using System.Collections;
using System.Globalization;

namespace LexiconCalculator
{
	public class Calculator
	{

		static void Main()
		{
			// Only end program when the user requests it
			bool endProgram = false;

			// Delimiters used when reading in a list of numbers from the user
			char[] delimiters = new char[] { ' ', ';', ':' };

			while(!endProgram)
			{

				// Menu block, just to be able to collapse it
				{
					Console.ResetColor();
					Console.Clear();
					Console.WriteLine("       Menu");
					Console.WriteLine();
					Console.WriteLine("  1. Addition");
					Console.WriteLine("  2. Subtraction");
					Console.WriteLine("  3. Multiplication");
					Console.WriteLine("  4. Division");
					Console.WriteLine("  5. Power");
					Console.WriteLine();
					Console.WriteLine(" 11. Addition with numberlist");
					Console.WriteLine(" 12. Subtraction with numberlist");
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.WriteLine(" 99. Advanced calculator (Bonus)");
					Console.ResetColor();
					Console.WriteLine();
					Console.WriteLine("  0. End program");
					Console.WriteLine();

					Console.Write("Enter selection: ");
				}
				int selection;
				while(!int.TryParse(Console.ReadLine() ?? "", out selection))
				{
					Console.Write("Enter selection: ");
				}

				// Start the selected operation
				Console.Clear();
				switch(selection)
				{
					case 0:
						endProgram = true;
						break;

					case 1:
						Add();
						break;

					case 2:
						Substract();
						break;

					case 3:
						Multiply();
						break;

					case 4:
						Divide();
						break;

					case 5:
						Power();
						break;

					case 11:
						ListAddAndSubstract(Operator.Plus, delimiters);
						break;

					case 12:
						ListAddAndSubstract(Operator.Minus, delimiters);
						break;

					case 99:
						AdvancedCalc();
						break;

					default:
						Console.WriteLine("Selection does not exist!");
						break;
				}

				if(!endProgram)
				{
					// Wait for the user to be done before
					// going back to the menu

					Console.WriteLine();
					Console.Write("Press any key to continue!");
					_ = Console.ReadKey(true);
				}
			}

			// Clean up the console before the program ends
			Console.ResetColor();
			Console.Clear();
		}


		// Read two numbers from the user.
		// Add the two numbers and present the sum to the user.
		public static void Add()
		{
			Console.WriteLine();
			Console.WriteLine("Addition");
			Console.WriteLine();

			double term1 = ReadDouble("Enter the first term: ");
			double term2 = ReadDouble("Enter the second term: ");

			Console.WriteLine();
			double sum = Addition(term1, term2);
			Console.WriteLine($"{term1} + {term2} = {sum}");
		}

		// Read two numbers from the user.
		// Substract the second number from the first and present
		// the difference to the user
		public static void Substract()
		{
			Console.WriteLine();
			Console.WriteLine("Subtraction");
			Console.WriteLine();

			double minuend = ReadDouble("Enter the minuend: ");
			double subtrahend = ReadDouble("Enter the subtrahend: ");

			Console.WriteLine();
			double difference = Subtraction(minuend, subtrahend);
			Console.WriteLine($"{minuend} - {subtrahend} = {difference}");
		}

		// Read a list of numbers, separated from eachother with the
		// delimiters, from the user. Then try to perform the
		// operation (addition or subtraction) indicated by op.
		// The header is printed as a heder of the subpage.
		// An ArgumentException is thrown if the op is not
		// Operator.Plus or Operator.Minus.
		public static void ListAddAndSubstract(Operator op, char[] delimiters)
		{

			// Some initialization of values that depend on the operator. 
			// If a non-supported operator used passed, and exeption is thrown.
			char opChar;
			double result;
			string header;
			if(op == Operator.Plus)
			{
				opChar = '+';
				header = "Addition, list of numbers";
			}
			else if(op == Operator.Minus)
			{
				opChar = '-';
				header = "Subtraction, list of numbers";
			}
			else
			{
				throw new ArgumentException("The supplied operator is not valid: " + op.ToString());
			}

			// Print out info to the user about the function
			Console.WriteLine();
			Console.WriteLine(header);

			Console.Write("Delimiters: ");
			foreach(var delimiter in delimiters)
			{
				Console.Write($"'{delimiter}' ");
			}

			Console.WriteLine();
			Console.WriteLine();


			// Get get a list of numbers from the user
			// and try to parse them and operate on them
			Console.Write("Enter a list of numbers: ");

			if(TryParseStringListOfValues(Console.ReadLine(), delimiters, out double[] values) && values.Length > 0)
			{
				// Yay, we have a list of numbers, with at least on number in it

				// Perform the appropirate operations on the numbers
				if(op == Operator.Plus)
				{
					result = Addition(values);
				}
				else if(op == Operator.Minus)
				{
					result = Subtraction(values);
				}
				else
				{
					throw new ArgumentException("The supplied operator is not valid: " + op.ToString());
				}


				// Write out the equation and the result
				Console.WriteLine();
				for(int i = 0; i < values.Length; i++)
				{
					if(values[i] >= 0)
					{
						Console.Write($"{values[i]}");
					}
					else
					{
						Console.Write($"({values[i]})");
					}

					if(i < values.Length - 1)
					{
						Console.Write($" {opChar} ");
					}
					else
					{
						Console.Write($" = {result}");
					}
				}
			}
			else
			{
				// Better enter the list of numbers correctly next time
				Console.WriteLine();
				Console.WriteLine("You entered an empty or malformed list of numbers!");
			}
			Console.WriteLine();
		}

		// Read two numbers from the user.
		// Multiply the numbers and present the product to the user
		public static void Multiply()
		{
			Console.WriteLine();
			Console.WriteLine("Multiplication");

			double multiplier = ReadDouble("Enter the multiplier: ");
			double multiplicand = ReadDouble("Enter the multiplicand: ");

			Console.WriteLine();
			double product = Multiplication(multiplier, multiplicand);
			Console.WriteLine($"{multiplier} * {multiplicand} = {product}");
		}

		// Read two numbers from the user.
		// Divide one number by another and present the quotient.
		public static void Divide()
		{
			Console.WriteLine();
			Console.WriteLine("Division");
			Console.WriteLine();

			double dividend = ReadDouble("Enter the dividend: ");
			double divisor = ReadDouble("Enter the divisor: ");

			Console.WriteLine();

			double quotient = Division(dividend, divisor);
			if(double.IsNegativeInfinity(quotient))
			{
				Console.WriteLine($"{dividend} / {divisor} = -Inf");
			}
			else if(double.IsPositiveInfinity(quotient))
			{
				Console.WriteLine($"{dividend} / {divisor} = +Inf");
			}
			else if(double.IsNaN(quotient))
			{
				Console.WriteLine($"{dividend} / {divisor} = NaN");
			}
			else
			{
				Console.WriteLine($"{dividend} / {divisor} = {quotient}");
			}
		}

		// Read two numbers from the user.
		// Then rise the first to the power of the other and
		// present the power to the user.
		public static void Power()
		{
			Console.WriteLine();
			Console.WriteLine("Power of");
			Console.WriteLine();

			double baseValue = ReadDouble("Enter the base: ");
			double exponent = ReadDouble("Enter the power: ");

			Console.WriteLine();
			double power = PowerOf(baseValue, exponent);
			Console.WriteLine($"{baseValue}^{exponent} = {power}");
		}

		// I missunderstood the task at first, and this is what I came up with then. :-)
		// It should be able to handle things like a + b * c - d / e * f correctly
		public static void AdvancedCalc()
		{
			Console.WriteLine();
			Console.WriteLine("Advanced Calculator");
			Console.WriteLine("Valid operators: + - * / =");
			Console.WriteLine();
			Console.WriteLine();

			// expression is used to record the expression entered and presented to the user at the end
			string expression = "";

			double partResult, currentValue;
			double totalResult = 0.0;

			// Get the start value from the user
			currentValue = ReadDouble("Enter value: ");
			partResult = currentValue;
			expression += partResult.ToString() + " ";

			// End main loop if "=" has been entered as en operator or an error has occured
			bool done = false;
			while(!done)
			{
				Operator op = ReadOperator("Enter operator: ");

				if(op == Operator.Equals)
				{
					// Time to finish up and present the result to the user
					totalResult += partResult;
					expression += "= " + totalResult;

					Console.WriteLine();
					Console.WriteLine(expression);

					done = true;
				}
				else
				{
					// Read the next value in the expression
					currentValue = ReadDouble("Enter value: ");

					switch(op)
					{
						// We are at the lowest operator priority level and
						// therefore we can add the partResult is ready to be added
						// to the totalResult.
						// partResult is then reset to the last read value.
						// We cannot add the last read value to totalResult yet, as the
						// next operator might have a higher priority level.
						case Operator.Plus:
							expression += "+ " + currentValue + " ";
							totalResult += partResult;
							partResult = currentValue;
							break;

						// We are at the lowest operator priority level and therefore
						// we can add the partResult to the totalResult.
						// partResult reset is set to the negated last value, as the previous
						// operator was minus and we need to invert the sign of value to
						// make partResult behave correctly when we later add it to totalResult
						// We cannot subtract the last read value from totalResult yet, as the
						// next operator might have a higher priority level.
						case Operator.Minus:
							expression += "- " + currentValue + " ";
							totalResult += partResult;
							partResult = -currentValue;
							break;

						// We are at the highest priority level and therefore
						// we can not add the partResult to the totalResult,
						// as we do not know if the next operator is of the same
						// or lower priotity.
						// We multiply the last read value with the current partResult
						// and store the product in partResult
						case Operator.Multiply:
							expression += "* " + currentValue + " ";
							partResult *= currentValue;
							break;

						// We are at the highest priority level and therefore
						// we can not add the partResult to the totalResult,
						// as we do not know if the next operator is of the same
						// or lower priotity.
						// We divide the current partResult with the last read value
						// and store the quotient in partResult.
						case Operator.Divide:

							// Catch divide by zero and end calculation
							if(currentValue == 0.0)
							{
								Console.WriteLine();
								Console.WriteLine("Error: Division by zero!");
								Console.WriteLine();
								done = true;
								break;
							}

							expression += "/ " + currentValue + " ";
							partResult /= currentValue;
							break;

						default:
							Console.WriteLine();
							Console.WriteLine("Not an operator!");
							Console.WriteLine();
							done = true;
							break;
					}
				}
			}
		}


		/***************** Mathematical operations **************************/

		// Add term1 with term2 and return the sum.
		public static double Addition(double term1, double term2)
		{
			return term1 + term2;
		}

		// Add the numbers in the array and return the sum.
		// An exceotion is thrown if the array is null or empty.
		public static double Addition(double[] numbers)
		{
			if(numbers == null || numbers.Length == 0)
			{
				throw new ArgumentException("The array numbers must contain at least one number!");
			}

			double sum = 0.0;
			foreach(double term in numbers)
			{
				sum = Addition(sum, term);
			}

			return sum;

		}

		// Substract subtrahend from minuend and return the difference.
		public static double Subtraction(double minuend, double subtrahend)
		{
			return minuend - subtrahend;
		}

		// Substact the subsequent numbers from the first and
		// return the difference.
		// An exceotion is thrown if the array is null or empty
		public static double Subtraction(double[] numbers)
		{
			if(numbers.Length == 0)
			{
				throw new ArgumentException("The array numbers must contain at least one number!");
			}

			double difference = numbers[0];

			for(int i = 1; i < numbers.Length; i++)
			{
				difference = Subtraction(difference, numbers[i]);
			}

			return difference;

		}

		// Multiply the multiplier with the multiplicand and return the prodoct.
		public static double Multiplication(double multiplier, double multiplicand)
		{
			return multiplier * multiplicand;
		}

		// Divide the dividend with the divisor and return the quotient.
		// If divisor is zero either NaN, positiv or negative Infinity is
		// returned depending on the dividend.
		public static double Division(double dividend, double divisor)
		{
			if(divisor == 0.0 && dividend == 0.0)
			{
				return double.NaN;
			}
			else if(divisor == 0.0 && dividend < 0.0)
			{
				return double.NegativeInfinity;
			}
			else if(divisor == 0.0)
			{
				return double.PositiveInfinity;
			}

			return dividend / divisor;

		}

		// Raise baseValue to the power of the exponent and return the
		// power to the user.
		public static double PowerOf(double baseValue, double exponent)
		{
			return Math.Pow(baseValue, exponent);
		}

		/******************************************************************/


		// Get a valid operator from the user.
		// The method keeps trying until a valid operator is entered.
		public static Operator ReadOperator(string message)
		{
			Console.Write(message);

			Operator op;
			while(!TryParseOperator(Console.ReadLine(), out op))
			{
				Console.WriteLine("Error: Not an operator!");
				Console.WriteLine();
				Console.Write(message);
			}

			return op;
		}

		// Helper function the get a double value from the user
		// If the user enters something that is not a double value,
		// the function will print an error message and the provided
		// message to get the user to provide a valid value.
		public static double ReadDouble(string message)
		{
			double answer;
			Console.Write(message);

			while(!double.TryParse(Console.ReadLine(), out answer))
			{
				Console.WriteLine("Error: Not a number!");
				Console.WriteLine();
				Console.Write(message);
			}
			return answer;
		}


		// Tries to parse a string containing a list of doubles.
		// If the attempt succeds true is returned and the array
		// values are populated with the values found in the string.
		// 
		public static bool TryParseStringListOfValues(string stringOfValues, char[] delimiters, out double[] values)
		{
			// Because I instictly uses '.' as a decimal separtor
			// when the program text is in english, but the local
			// uses ','. I'm debating wether to change the locality
			// completely withing the program or not. For now not.
			if(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
			{
				stringOfValues = stringOfValues.Replace('.', ',');
			}

			// Separate out the different values between the
			// delimiters, and don't care about empty entries.
			string[] valueStrings = stringOfValues.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			// Now try and create an array of doubles from
			// the string with values. If it fails, empty
			// the values array and return false.
			values = new double[valueStrings.Length];

			for(int i = 0; i < valueStrings.Length; i++)
			{
				if(!double.TryParse(valueStrings[i], out values[i]))
				{
					values = Array.Empty<double>();
					return false;
				}
			}

			// The parsing of values succeeded!
			return true;

		}

		// Get a operator from a string.
		// If the string contains a operator, and only a operator,
		// the function will return true and the found operator is in op.
		// If that fails the function will return false, and op is set to
		// Operator.None.
		public static bool TryParseOperator(string opString, out Operator op)
		{
			bool returnValue;
			if(opString == null)
			{
				op = Operator.None;
				returnValue = false;
			}
			else if(opString.Trim().Length == 0)
			{
				// Accept just a pressed enter key as the same as a equals sign
				// to better work with a numerical keyboard.
				op = Operator.Equals;
				returnValue = true;
			}
			else
			{
				switch(opString.Trim())
				{
					case "=":
						op = Operator.Equals;
						returnValue = true;
						break;

					case "+":
						op = Operator.Plus;
						returnValue = true;
						break;

					case "-":
						op = Operator.Minus;
						returnValue = true;
						break;

					case "*":
						op = Operator.Multiply;
						returnValue = true;
						break;

					case "/":
						op = Operator.Divide;
						returnValue = true;
						break;

					case "^":
						op = Operator.PowerTo;
						returnValue = true;
						break;

					case "**":
						op = Operator.PowerTo;
						returnValue = true;
						break;

					default:
						op = Operator.None;
						returnValue = false;
						break;
				}
			}

			return returnValue;
		}

		// Availible operators
		public enum Operator
		{
			None,
			Equals,
			Plus,
			Minus,
			Multiply,
			Divide,
			PowerTo
		}
	}
}
