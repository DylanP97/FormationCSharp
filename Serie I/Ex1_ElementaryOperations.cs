using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Serie_I
{
    public static class ElementaryOperations
    {
        public static void BasicOperation(int a, int b, char operation)
        {
            switch (operation)
            {
                case '+':
                    Console.WriteLine($"{a} + {b} = {a + b}");
                    break;
                case '-':
                    Console.WriteLine($"{a} - {b} = {a - b}");
                    break;
                case '*':
                    Console.WriteLine($"{a} * {b} = {a * b}");
                    break;
                case '/':
                    if (b != 0)
                        Console.WriteLine($"{a} / {b} = {a / (double)b}");
                    else
                        Console.WriteLine("Cannot divide by zero.");
                    break;
                default:
                    Console.WriteLine("Invalid operation. Please use '+', '-', '*', or '/'");
                    break;
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            if (b != 0)
                Console.WriteLine($"{a} / {b} (integer division) = {a / b}");
            else
                Console.WriteLine("Cannot perform integer division by zero.");
        }

        public static void Pow(int a, int b)
        {
            if (b >= 0)
            {
                int result = 1;
                for (int i = 0; i < b; i++)
                {
                    result *= a;
                }
                Console.WriteLine($"{a}^{b} = {result}");
            }
            else
            {
                Console.WriteLine("Exponent should be a non-negative integer.");
            }
        }
    }
}