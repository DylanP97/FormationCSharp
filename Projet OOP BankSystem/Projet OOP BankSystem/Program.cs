using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_OOP_BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Projet OOP");
            Console.WriteLine("Banking System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Reading Input Files");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            string path = Directory.GetCurrentDirectory();
            string bankAccountsFile = path + @"\transactions.csv";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.csv");

            if (File.Exists(filePath))
            {
                ReadAndDisplayCsvFile(filePath);
            }
            else
            {
                Console.WriteLine("The CSV file does not exist.");
            }

            Console.ReadLine();
        }

        static void ReadAndDisplayCsvFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
