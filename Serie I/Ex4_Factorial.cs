using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Factorial
    {
        public static int Factorial_(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Le nombre doit être non négatif pour calculer la factorielle.");
            }

            int result = 1;

            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        public static int FactorialRecursive(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Le nombre doit être non négatif pour calculer la factorielle.");
            }

            // Cas de base : factorielle de 0 est 1
            if (n == 0)
            {
                return 1;
            }

            // Appel récursif pour calculer la factorielle
            return n * FactorialRecursive(n - 1);
        }
    }
}
