using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_III
{
    public struct SortData
    {
        /// <summary>
        /// Moyenne pour le tri par insertion
        /// </summary>
        public long InsertionMean { get; set; }
        /// <summary>
        /// Écart-type pour le tri par insertion
        /// </summary>
        public long InsertionStd { get; set; }
        /// <summary>
        /// Moyenne pour le tri rapide
        /// </summary>
        public long QuickMean { get; set; }
        /// <summary>
        /// Écart-type pour le tri rapide
        /// </summary>
        public long QuickStd { get; set; }
    }

    public static class SortingPerformance
    {
        public static void DisplayPerformances(List<int> sizes, int count)
        {
            List<SortData> performances = PerformancesTest(sizes, count);

            Console.WriteLine("Size\tInsertion (Mean)\tInsertion (Std)" +
                "\tQuick (Mean)\tQuick (Std)");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < sizes.Count; i++)
            {
                Console.WriteLine($"{sizes[i]}\t{performances[i].InsertionMean}\t{performances[i].InsertionStd}\t{performances[i].QuickMean}\t{performances[i].QuickStd}");
            }
        }

        public static List<SortData> PerformancesTest(List<int> sizes, int count)
        {
            List<SortData> performances = new List<SortData>();

            foreach (int size in sizes)
            {
                SortData data = PerformanceTest(size, count);
                performances.Add(data);
            }

            return performances;
        }

        public static SortData PerformanceTest(int size, int count)
        {
            List<long> insertionTimes = new List<long>();
            List<long> quickTimes = new List<long>();

            for (int i = 0; i < count; i++)
            {
                int[] array = ArraysGenerator(size).First(); // Taking the first generated array

                long insertionTime = UseInsertionSort(array);
                long quickTime = UseQuickSort(array);

                insertionTimes.Add(insertionTime);
                quickTimes.Add(quickTime);
            }

            SortData data = new SortData
            {
                InsertionMean = insertionTimes.Sum() / count,
                //InsertionStd = CalculateStandardDeviation(insertionTimes),
                QuickMean = quickTimes.Sum() / count,
                //QuickStd = CalculateStandardDeviation(quickTimes)
            };

            return data;
        }

        private static List<int[]> ArraysGenerator(int size)
        {
            List<int[]> arrays = new List<int[]>();

            Random random = new Random();
            for (int i = 0; i < 2; i++) // Generating a pair of identical arrays
            {
                int[] array = new int[size];
                for (int j = 0; j < size; j++)
                {
                    array[j] = random.Next(-1000, 1001);
                }
                arrays.Add(array);
            }

            return arrays;
        }


        public static long UseInsertionSort(int[] array)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            InsertionSort(array);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long UseQuickSort(int[] array)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSort(array, 0, array.Length - 1);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static void InsertionSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        int tmp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = tmp;
                    }
                }
            };
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                }
            }
            int tmp = array[i];
            array[i] = array[right];
            array[right] = tmp;
            return i;
        }
    }
}
