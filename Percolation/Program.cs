using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{

    // Les questions qui nécessitent des réponses écrites sont en bas de cette page.
    class Program
    {
        static void Main(string[] args)
        {
            #region Partie 1 - Percolation Simple Test
            //Percolation perc = new Percolation(15);
            //Random random = new Random();
            //while (!perc._percolate)
            //{
            //    int randomRow = random.Next(0, 15);
            //    int randomCol = random.Next(0, 15);
            //    // Check if the case is already open, and if so, continue to the next iteration
            //    if (perc.IsOpen(randomRow, randomCol))
            //    {
            //        continue;
            //    }

            //    perc.Open(randomRow, randomCol);
            //};
            #endregion

            #region Partie 2 - 50 Percolations Pour Obtenir Des Moyennes Statistiques 
            // Pour finir, on test à 50 reprises pour obtenir des 
            // moyennes, écart-type et fractions assez précis et représentatifs.
            PercolationSimulation percSiml = new PercolationSimulation();
            percSiml.MeanPercolationValue(15, 50);
            #endregion

            Console.WriteLine();
            // Keep the console window open
            //Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}


// question 1.a : Est ce que la case (i, j) est ouverte/pleine ?
// réponse : -

// question 1.b : Est-ce que la percolation a lieu ?
// réponse : -

// question 3.b : Quelle est la performance de cette méthode dans le pire cas ?        
// réponse : La performance de la méthode Open() dans le pire des cas sera déterminé par la taille initial des
// tableaux et par le nombre d'appels récursifs qui peut être important.

// question 3.c : Expliquer, intuitivement, pour quelles raisons ce cas a-t-il peu de chances de se produire?
// réponse : Ce cas est assez peu probable puisqu'il y a un surement un partage équilibré des cases ouvertes
// dans la grille dans différentes directions et donc vers le bas.

