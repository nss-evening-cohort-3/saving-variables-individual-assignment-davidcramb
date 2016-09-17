using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Expression
    {
        public Expression()
        {
             Constant.userVars = new Dictionary<char, int>();
        }
        public Stack Constant = new Stack();
        public char[] validOperand = { '+','-','%','/','*'};
        public char[] digits = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public string expression_as_string { get; set; }
        public string[] expression_array { get; set; }
        public int LHS { get; set; }
        public int RHS { get; set; }
        public char operand { get; set; }
        int operationcounter { get; set; }
        //oh god why didn't I use regex
        public void ExpressionSplit (string expression_as_string)
        {
            try
            {
                this.expression_as_string = expression_as_string.Trim();
                this.expression_as_string = expression_as_string.Replace(" ",String.Empty);
                bool containsConstant = false;
                int x;
                int y;
                expression_array = this.expression_as_string.Split(validOperand);
                if (!Int32.TryParse(expression_array[0], out x))
                {
                
                    x = Constant.userVars[Convert.ToChar(expression_array[0])];
                }
                if (!Int32.TryParse(expression_array[1], out y))
                {
                    y = Constant.userVars[Convert.ToChar(expression_array[1])];
                }
                LHS = x;
                RHS = y;
                findOperand();
                if (expression_array.Length > 2) 
                {
                    throw new Exception ("This program only accepts two integers and an operator (ie, x + y)");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid format, try again. For example: 10 + 5 or 700 / 7");
                Console.WriteLine(e.Message);
            }
        }


        public void findOperand()
        {
            try
            {
                int LHS_Length = LHS.ToString().Length;
                char operand_from_string = expression_as_string[LHS_Length];
                //if there is trailing whitespace before the operand, this while loop will continue until the operand is found
                while (char.IsWhiteSpace(operand_from_string))
                {
                    LHS_Length++;
                    operand_from_string = expression_as_string[LHS_Length];
                }
                operand = operand_from_string;
            }
            catch (FormatException e)
            {
                throw new Exception ("Try another format, for example: 10 + 5 or 7 * 12");
            }
        }
        public void CreateVariable(string variable, string value)
        {
            char x = char.Parse(variable);
            int y = int.Parse(value);
            Constant.SetUserVariableToThisValue(x, y);
        }
    }
}
