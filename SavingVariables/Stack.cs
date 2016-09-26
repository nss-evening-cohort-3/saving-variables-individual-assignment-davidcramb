using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SavingVariables
{
    public class Stack
    {
        public Stack()
        {
        }
        public Stack<int> last_answer { get; set; }
        public Stack<string> last_expression { get; set; }
        //public Dictionary<int, UserValue> varDictionary { get; set; }
        public string VariableLetter {get; set; }
        public int VariableValue { get; set; }
        public Dictionary<string, int> userVars { get; set; }

        
        public void SetUserVariableToThisValue(string variable, int value)
        {
            if (userVars.ContainsKey(variable))
            {
                Console.WriteLine("You've already set this...");
            }
            else
            {
                VariableLetter = variable;
                VariableValue = value;
                userVars.Add(VariableLetter, VariableValue);
                Console.WriteLine("Set {0} to {1}", VariableLetter, VariableValue);
            }
        }
        public void AddToDictionary()
        {

        }

        public void SetLastAnswer(int result)
        {
            last_answer = new Stack<int>();
            last_answer.Push(result);
        }

        public void GetLastAnswer()
        {
            int answer = last_answer.Pop();
            Console.WriteLine("The last answer was...{0}", answer);
        }
        public void SetLastExpressionQueried(string userExpression)
        {
            last_expression = new Stack<string>();
            last_expression.Push(userExpression);
        }

        public void GetLastExpressionQueried()
        {
            string query = last_expression.Pop();
            Console.WriteLine("The last query was...{0}", query);
        }



    }
}
