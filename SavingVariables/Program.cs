using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepGoing = true;
            string constPat = @"(?<variable>[a-z])\s=\s(?<integer>[0-9]+)";
            Regex constCheck = new Regex(constPat, RegexOptions.IgnoreCase);
            string[] quitArray = { "no", "quit", "stop", "exit" };
            Stack lastExpression = new Stack();
            Expression userExpression = new Expression();

            Console.WriteLine("This is a Calculator. There are many like it but this one is mine.");

            while (keepGoing)
            {
                var prompt = ">";
                Console.WriteLine("Add, Subtract, Multiply, Divide, or get a Remainder");
                Console.Write(prompt);
                string userPrompt = Console.ReadLine().ToLower();
                Match m = constCheck.Match(userPrompt);

                if (quitArray.Any(userPrompt.Contains))
                {
                    keepGoing = false;
                    Console.WriteLine("Bye!");
                    Console.ReadLine();
                }
                else if (userPrompt == "last")
                {
                    try
                    {
                        lastExpression.GetLastAnswer();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (userPrompt == "lastq")
                {

                    try
                    {
                        lastExpression.GetLastExpressionQueried();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (m.Success)
                {
                    userExpression.CreateVariable(m.Groups[1].ToString(), m.Groups[2].ToString());
                }
                else
                {
                    try
                    {
                        userExpression.ExpressionSplit(userPrompt);
                        Calculate calculation = new Calculate(userExpression.LHS, userExpression.RHS, userExpression.operand);
                        calculation.DetermineOperation();
                        lastExpression.SetLastExpressionQueried(userPrompt);
                        lastExpression.SetLastAnswer(calculation.result);
                        Console.WriteLine(calculation.result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
