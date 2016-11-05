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
            UserCommands command = new UserCommands();
            Regex constCheck = new Regex(constPat, RegexOptions.IgnoreCase);
            Stack lastExpression = new Stack();
            Expression userExpression = new Expression();
            DAL.SavingVarRepository Repo = new DAL.SavingVarRepository();

            Console.WriteLine("This is a Calculator. There are many like it but this one is mine.");

            while (keepGoing)
            {
                Console.WriteLine("Add, Subtract, Multiply, Divide, or get a Remainder");
                Console.Write(command.prompt);
                string userPrompt = Console.ReadLine().ToLower();
                Match m = constCheck.Match(userPrompt);

                if (command.quitArray.Any(userPrompt.Contains))
                {
                    keepGoing = false;
                    Console.WriteLine("Bye! Thanks for calculating with me today.");
                    Console.ReadLine();
                }
                else if (userPrompt == command.last)
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
                else if (userPrompt == command.lastq)
                {
                    try
                    {
                        lastExpression.GetLastExpressionQueried();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                } else if (command.deleteArray.Any(userPrompt.Contains))
                {
                    Repo.RemoveAllVariablesFromDatabase(Repo.Context.Variables);
                    userExpression.RemoveAllVariables();
                    Console.WriteLine("All variables have been removed from the database. I hope you're proud of yourself.");
                } else if (command.showall.Any(userPrompt.Contains))
                {
                    Repo.CreateDictionaryOfVariablesAndValues();
                    Repo.WriteDictionaryKVPToConsole(Repo.CreateDictionaryOfVariablesAndValues());
                }

                else if (m.Success)
                {
                    userExpression.CreateVariable(m.Groups[1].ToString(), m.Groups[2].ToString());
                    Models.Variables new_variable = new Models.Variables { Variable = m.Groups[1].ToString(), Value = int.Parse(m.Groups[2].ToString()) };
                    Repo.AddVariable(new_variable);
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
