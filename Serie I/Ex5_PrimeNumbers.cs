using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class PrimeNumbers
    {
        static bool IsPrime(int valeur)
        {
            if (valeur <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(valeur); i++)
            {
                if (valeur % i == 0)
                    return false;
            }

            return true;
        }

        public static void DisplayPrimes()
        {
            Console.WriteLine("Prime numbers:");

            for (int i = 2; i <= 100; i++) // Adjust the range as needed
            {
                if (IsPrime(i))
                {
                    Console.Write(i + " ");
                }
            }

            Console.WriteLine();
        }
    }
}
