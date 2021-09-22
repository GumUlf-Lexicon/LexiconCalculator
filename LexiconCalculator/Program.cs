using System;

namespace LexiconCalculator
{
	class Program
	{

		static void Main()
		{
			bool endProgram = false;

			while(!endProgram)
			{
				Console.Clear();
				Console.WriteLine("       Menu");
				Console.WriteLine();
				Console.WriteLine("  1. Addition");
				Console.WriteLine("  2. Subtraction");
				Console.WriteLine("  3. Multiplication");
				Console.WriteLine("  4. Division");
				Console.WriteLine("  5. Power");
				Console.WriteLine();
				Console.WriteLine("  0. End program");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine(" 99. Advanced calculator (Bonus)");
				Console.ResetColor();
				Console.WriteLine();

				Console.Write("Enter selection: ");
				int selection;
				while(!int.TryParse(Console.ReadLine() ?? "", out selection))
				{
					Console.Write("Enter selection: ");
				}

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

					case 99:
						AdvancedCalc();
						break;

					default:
						Console.WriteLine("Selection does not exist!");
						break;
				}
				if(!endProgram)
				{
					Console.WriteLine();
					Console.Write("Press any key to continue!");
					_ = Console.ReadKey(true);
				}
			}
			Console.Clear();
		}


		// Helper function the get a double value from the user
		// If the user enters something that is not a double value,
		// the function will print an error message and the provided
		// message to get the user to provide a valid value.
		private static double ReadDouble(string message)
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

		// Read two numbers from the user.
		// Add the two numbers and present the sum to the user.
		private static void Add()
		{
			Console.WriteLine("\nAddition\n");

			double term1 = ReadDouble("Enter the first term: ");
			double term2 = ReadDouble("Enter the second term: ");

			Console.WriteLine();
			double sum = term1 + term2;
			Console.WriteLine($"{term1} + {term2} = {sum}");
		}

		// Read two numbers from the user.
		// Substract the second number from the first and present
		// the difference to the user
		private static void Substract()
		{
			Console.WriteLine("\nSubtraction\n");

			double minuend = ReadDouble("Enter the minuend: ");
			double subtrahend = ReadDouble("Enter the subtrahend: ");

			Console.WriteLine();
			double difference = minuend - subtrahend;
			Console.WriteLine($"{minuend} - {subtrahend} = {difference}");
		}

		// Read two numbers from the user.
		// Multiply the numbers and present the product to the user
		private static void Multiply()
		{
			Console.WriteLine("\nMultiplication\n");

			double multiplier = ReadDouble("Enter the multiplier: ");
			double multiplicand = ReadDouble("Enter the multiplicand: ");

			Console.WriteLine();
			double product = multiplier * multiplicand;
			Console.WriteLine($"{multiplier} * {multiplicand} = {product}");
		}

		// Read two numbers from the user.
		// Divide one number by another and present the quotient.
		private static void Divide()
		{
			Console.WriteLine("\nDivision\n");

			double dividend = ReadDouble("Enter the dividend: ");
			double divisor = ReadDouble("Enter the divisor: ");

			Console.WriteLine();
			// Catch divide by zero
			if(divisor == 0.0)
			{
				Console.WriteLine("Error: You cannot divide by zero!");
			}
			else
			{
				double quotient = dividend / divisor;
				Console.WriteLine($"{dividend} / {divisor} = {quotient}");
			}
		}

		// Read two numbers from the user.
		// Then rise the first to the power of the other and
		// present the power to the user.
		private static void Power()
		{
			Console.WriteLine("\nPower of\n");

			double baseValue = ReadDouble("Enter the base: ");
			double exponent = ReadDouble("Enter the power: ");

			Console.WriteLine();
			double power = Math.Pow(baseValue, exponent);
			Console.WriteLine($"{baseValue}^{exponent} = {power}");
		}



		// I missunderstood the task at first, and this is what I came up with then. :-)
		// It should be able to handle things like a + b * c - d / e * f correctly
		private static void AdvancedCalc()
		{

			Console.WriteLine("\nAdvanced Calculator");
			Console.WriteLine("Valid operators: + - * / =");
			Console.WriteLine("\n");

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

		// Get a valid operator from the user.
		// The method keeps trying until a valid operator is entered.
		private static Operator ReadOperator(string message)
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


		// Get a operator from a string.
		// If the string contains a operator, and only a operator,
		// the function will return true and the found operator is in op.
		// If that fails the function will return false, and op is set to
		// Operator.None.
		private static bool TryParseOperator(string opString, out Operator op)
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

					default:
						op = Operator.None;
						returnValue = false;
						break;
				}
			}

			return returnValue;
		}

		// Availible operators
		private enum Operator
		{
			None,
			Equals,
			Plus,
			Minus,
			Multiply,
			Divide
		}
	}
}
