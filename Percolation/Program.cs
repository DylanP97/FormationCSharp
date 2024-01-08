using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    class Program
    {
        static void Main(string[] args)
        {
            // question 1.a : Est ce que la case (i, j) est ouverte/pleine ?
            // réponse : 

            // question 1.b : Est-ce que la percolation a lieu ?
            // réponse : 

            // question 3.b : Quelle est la performance de cette méthode dans le pire cas ?        
            // réponse : La performance de la méthode Open() dans le pire des cas sera déterminé par la taille initial des
            // tableaux et par le nombre d'appels récursifs qui peut être important.

            // question 3.c : Expliquer, intuitivement, pour quelles raisons ce cas a-t-il peu de chances de se produire?
            // réponse : Ce cas est assez peu probable puisqu'il y a un surement un partage équilibré des cases ouvertes
            // dans la grille dans différentes directions et donc vers le bas.

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
