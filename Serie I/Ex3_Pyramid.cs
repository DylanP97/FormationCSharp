using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {

            if (isSmooth)
            {
                Console.WriteLine($"Voici une pyramide style Las Vegas avec {n} étages : ");
            } else
            {
                Console.WriteLine($"Voici une pyramide style Gizeh avec {n} étages : ");
            }
            Console.WriteLine();

            if (n <= 0)
            {
                Console.WriteLine("La nombre d'étages doit être un supérieur à 0.");
                return;
            }

            // Construction de la pyramide
            for (int i = 0; i < n; i++)
            {
                // Affichage des espaces
                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write(" ");
                }

                if (isSmooth)
                {
                    // Style Vegas
                    for (int k = 0; k < 2 * i + 1; k++)
                    {
                        if (k % 2 == 0)
                        {
                            Console.Write("/");
                        }
                        else
                        {
                            Console.Write("\\");
                        }
                    }
                    Console.Write("\\");
                }
                else
                {
                    // Style Gizeh
                    for (int k = 0; k < 2 * i + 1; k++)
                    {
                        if (i == 0)
                        {
                            Console.Write("_");
                        }
                        else if (k % 2 == 0)
                        {
                            Console.Write("|");
                        }
                        else
                        {
                            Console.Write("_");
                        }
                    }
                }
                // Saut de ligne après chaque ligne de la base
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
