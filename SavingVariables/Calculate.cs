using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Calculate
    {
        public Calculate(int lhs, int rhs, char operand)
        {
            x = lhs;
            y = rhs;
            this.operand = operand;
        }

        public int x { get; set; }
        public int y { get; set; }
        public char operand { get; set; }
        public int result {get; set; }

        public void DetermineOperation() //based on operand
        {
            switch(operand)
            {
                case '+':
                    add(x, y);
                    break;
                case '-':
                    subtract(x, y);
                    break;
                case '%':
                    modulo(x, y);
                    break;
                case '*':
                    multiply(x, y);
                    break;
                case '/':
                    divide(x, y);
                    break;
            }
        }

        public void add(int x, int y)
        {
             result = x + y;
        }
        public void subtract(int x, int y)
        {
             result = x - y;
        }
        public void modulo(int x, int y)
        {
            try
            {
                result = x % y;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("You cannot divide by zero.");
            }
        }
        public void divide(int x, int y)
        {
            try
            {
                result = x / y;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("You cannot divide by zero.");
            }

        }
        public void multiply(int x, int y)
        {
             result = x * y;
        }

    }
}
