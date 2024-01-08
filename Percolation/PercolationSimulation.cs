using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public struct PclData
    {
        /// <summary>
        /// Moyenne 
        /// </summary>
        public double Mean { get; set; }
        /// <summary>
        /// Ecart-type
        /// </summary>
        public double StandardDeviation { get; set; }
        /// <summary>
        /// Fraction
        /// </summary>
        public double Fraction { get; set; }
    }

    public class PercolationSimulation
    {
        public PclData MeanPercolationValue(int size, int t)
        {
            double[] percolationValues = new double[t];

            // Perform t simulations
            for (int i = 0; i < t; i++)
            {
                Percolation perc = new Percolation(size);
                Random random = new Random();

                while (!perc._percolate)
                {
                    int randomRow = random.Next(0, size);
                    int randomCol = random.Next(0, size);

                    if (!perc.IsOpen(randomRow, randomCol))
                    {
                        perc.Open(randomRow, randomCol);
                    }
                }
                // Store the percolation value for this simulation
                percolationValues[i] = perc.PercolationValue();
                Console.WriteLine($"Percolation value for this simulation: {percolationValues[i]}");
            }

            // Calculate mean, standard deviation, and fraction
            double mean = CalculateMean(percolationValues);
            double standardDeviation = CalculateStandardDeviation(percolationValues, mean);
            double fraction = CalculateFraction(percolationValues, size);

            Console.WriteLine($"mean is : {mean}");
            Console.WriteLine($"standardDeviation is : {standardDeviation}");
            Console.WriteLine($"fraction is : {fraction}");

            return new PclData
            {
                Mean = mean,
                StandardDeviation = standardDeviation,
                Fraction = fraction
            };
        }

        private double CalculateMean(double[] values)
        {
            return values.Sum() / values.Length;
        }

        private double CalculateStandardDeviation(double[] values, double mean)
        {
            double sumSquaredDifferences = values.Select(val => Math.Pow(val - mean, 2)).Sum();
            return Math.Sqrt(sumSquaredDifferences / values.Length);
        }

        private double CalculateFraction(double[] values, int size)
        {
            return values.Count(val => val == 1) / (double)size;
        }
    }
}
