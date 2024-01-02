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

            Console.WriteLine();
            Console.WriteLine($"Voici la pyramide avec le nombre {n} et avec le paramètre isSmooth {isSmooth} : ");
            Console.WriteLine();


            if (n <= 0)
            {
                Console.WriteLine("La taille de la base doit être un entier positif.");
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

                // Affichage des étoiles
                for (int k = 0; k < 2 * i + 1; k++)
                {
                    Console.Write("*");
                }

                // Saut de ligne après chaque ligne de la base
                Console.WriteLine();
            }

            // Si la pyramide doit être lisse, afficher les côtés
            if (isSmooth)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    // Affichage des espaces
                    for (int j = 0; j < i + 1; j++)
                    {
                        Console.Write(" ");
                    }

                    // Affichage des étoiles
                    Console.Write("*");

                    for (int k = 0; k < 2 * (n - i - 1) - 1; k++)
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine("*");
                }
            }
        }
    }
}
