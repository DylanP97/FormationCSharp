using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Euclide
    {
        public static int Pgcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int PgcdRecursive(int a, int b)
        {
            // Cas de base : si b est égal à 0, le PGCD est a
            if (b == 0)
                return a;

            // Appel récursif avec les arguments inversés
            return PgcdRecursive(b, a % b);
        }
    }
}
