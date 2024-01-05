using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_III
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exercice I - Conseil de classe
            Console.WriteLine("------------------------------");
            Console.WriteLine("Exercice I - Conseil de classe");
            Console.WriteLine("------------------------------");

            string path = Directory.GetCurrentDirectory();
            string input = path + @"\class.csv";
            string output = path + @"\result.csv";
            ClassCouncil.SchoolMeans(input, output);
            #endregion

            #region Exercice II - Performances des tris
            //Console.WriteLine("-----------------------------------");
            //Console.WriteLine("Exercice II - Performances des tris");
            //Console.WriteLine("-----------------------------------");

            //int[] a = { 6, 4, 8, 2, 9, 3, 9, 4, 7, 6, 1 };
            //Console.WriteLine(SortingPerformance.UseInsertionSort(a) + " ms");
            //Console.WriteLine(SortingPerformance.UseQuickSort(a) + " ms");

            //Console.WriteLine();

            ////List<int> sizes = new List<int> { 2000, 5000, 10000 };
            ////int count = 10;
            //List<int> sizes = new List<int> { 2000, 5000, 10000 };
            //int count = 50;

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //SortingPerformance.DisplayPerformances(sizes, count);

            //stopwatch.Stop();
            //Console.WriteLine();
            //Console.WriteLine("Temps d'exécution cumulé totale pour l'évaluation des" +
            //    " performances : ");
            //Console.WriteLine("En millisecondes : " + stopwatch.ElapsedMilliseconds + "ms");
            //Console.WriteLine("En secondes : " + stopwatch.ElapsedMilliseconds / 1000 
            //    + "s");
            #endregion

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
