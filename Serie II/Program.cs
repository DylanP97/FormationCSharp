using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exercice I - Recherche d'un élément
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Exercice I - Recherche d'un élément");
            Console.WriteLine("-----------------------------------");

            int[] arr = { 1, -5, 10, -3, 0, 4, 2, -7 };
            int val = 2;

            Console.WriteLine();
            Console.WriteLine(val + " se trouve à l'index : " + Search.LinearSearch(arr, val));
            Array.Sort(arr);
            Console.WriteLine(val + " se trouve à l'index : " + Search.BinarySearch(arr, val));
            Console.WriteLine("Il faut noter que pour la recherche dichotomique/binaire, le tableau a " +
                "été initialement trier. L'ordre des valeurs a donc été modifié.");
            Console.WriteLine();
            #endregion

            #region Exercice II - Bases du calcul matriciel
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Exercice II - Bases du calcul matriciel");
            Console.WriteLine("---------------------------------------");

            int[] u = { 1, 2, 3 };
            int[] v = { -1, -4, 0 };
            int[][] matrice = Matrix.BuildingMatrix(u, v);
            Console.WriteLine();
            Console.WriteLine("Une nouvelle matrice");
            Console.WriteLine();
            Matrix.DisplayMatrix(matrice);

            int[][] matriceGauche = new int[3][]
            {
                new int[2] { 1, 2 },
                new int[2] { 4, 6 },
                new int[2] { -1, 8 }
            };

            int[][] matriceDroite = new int[3][]
            {
                new int[2] { -1, 5 },
                new int[2] { -4, 0 },
                new int[2] { 0, 2 }
            };

            Console.WriteLine("Addition");
            Console.WriteLine();
            Matrix.DisplayMatrix(Matrix.Addition(matriceGauche, matriceDroite));
            Console.WriteLine("Soustraction");
            Console.WriteLine();
            Matrix.DisplayMatrix(Matrix.Substraction(matriceGauche, matriceDroite));
            matriceDroite = new int[2][]
            {
                new int[3] { -1, 5, 0 },
                new int[3] { -4, 0, 1 }
            };
            Console.WriteLine("Multiplication");
            Console.WriteLine();
            Matrix.DisplayMatrix(Matrix.Multiplication(matriceGauche, matriceDroite));
            #endregion

            //#region Exercice III - Crible d'Eratosthène
            //Console.WriteLine("-----------------------------------");
            //Console.WriteLine("Exercice III - Crible d'Eratosthène");
            //Console.WriteLine("-----------------------------------");

            //int[] res = Eratosthene.EratosthenesSieve(100);
            //foreach (int nbr in res)
            //{
            //    if (nbr != int.MinValue)
            //    {
            //        Console.WriteLine(nbr);
            //    }
            //}
            //#endregion

            //#region Exercice IV - Questionnaire à choix multiple
            //Console.WriteLine("--------------------------------------------");
            //Console.WriteLine("Exercice IV - Questionnaire à choix multiple");
            //Console.WriteLine("--------------------------------------------");

            //Qcm[] qcms = new Qcm[3]
            //{
            //    new Qcm
            //    {
            //        Question = "Quelle est l'année du sacre de Charlemagne ?",
            //        Answers = new string[]
            //        {
            //            "476",
            //            "800",
            //            "1066",
            //            "1789",
            //        },
            //        Solution = 1,
            //        Weight = 1,
            //    },
            //    new Qcm
            //    {
            //        Question = "Quel est le nom du président de la République en 2021 ?",
            //        Answers = new string[]
            //        {
            //            "Chirac",
            //            "De Gaulle",
            //            "Macron",
            //        },
            //        Solution = 2,
            //        Weight = 1,
            //    },
            //    new Qcm
            //    {
            //        Question = "Quel est le nom du président de la République en 2021 ?",
            //        Answers = new string[]
            //        {
            //            "Chirac",
            //            "De Gaulle",
            //            "Macron",
            //        },
            //        Solution = 2,
            //        Weight = -1,
            //    }
            //};
            //Quiz.AskQuestions(qcms);
            //#endregion

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
