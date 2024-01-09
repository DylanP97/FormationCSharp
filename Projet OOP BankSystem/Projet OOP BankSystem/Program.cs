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
        static List<BankAccount> accountsList = new List<BankAccount>();

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Projet OOP");
            Console.WriteLine("Banking System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("input files");
            Console.WriteLine();
            string accountsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bank_accounts.csv");
            string transactionsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.csv");
            Console.WriteLine("bank_accounts.csv");
            ReadAndDisplayCsvFile(accountsFile);
            Console.WriteLine();
            Console.WriteLine("transactions.csv");
            ReadAndDisplayCsvFile(transactionsFile);

            Console.ReadLine();
        }

        static void ReadAndDisplayCsvFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        bool isHeader = true;

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');
                            Console.WriteLine(line);

                            if (isHeader)
                            {
                                isHeader = false;
                                continue;
                            }

                            if (filePath.Contains("bank_accounts.csv")) InstanciateAccounts(values);
                            else if (filePath.Contains("transactions.csv")) InstanciateTransactions(values);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("The CSV file does not exist.");
            }
        }

        static void InstanciateAccounts(string[] values)
        {
            if (int.TryParse(values[0], out int accountNumber) && decimal.TryParse(values[1], out decimal balance))
            {
                BankAccount bankAccount = new BankAccount(accountNumber, balance, 1000);
                // Now you have a BankAccount instance, and you can use it as needed.
                accountsList.Add(bankAccount);
            }
            else
            {
                Console.WriteLine("Invalid data format in CSV values for bank_accounts.");
            }
        }

        static void InstanciateTransactions(string[] values)
        {
            if (int.TryParse(values[0], out int transactionId) &&
                decimal.TryParse(values[1], out decimal amount) &&
                int.TryParse(values[2], out int senderAccountNumber) &&
                int.TryParse(values[3], out int recipientAccountNumber))
            {
                BankAccount senderAccount = FindAccountByNumber(senderAccountNumber);
                BankAccount recipientAccount = FindAccountByNumber(recipientAccountNumber);

                if (senderAccount != null && recipientAccount != null)
                {
                    // Create a transaction
                    Transaction transaction = new Transaction(transactionId, amount, senderAccountNumber, recipientAccountNumber);

                    // Perform the transfer
                    senderAccount.Transfer(recipientAccount, amount);

                    // Now you have a Transaction instance, and the transfer has been initiated.
                    // You can store the transaction in a list or perform other processing if needed.
                    List<Transaction> transactionsList = new List<Transaction>();
                    transactionsList.Add(transaction);
                }
                else
                {
                    Console.WriteLine("Sender or recipient account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid data format in CSV values for transactions.");
            }
        }

        static BankAccount FindAccountByNumber(int accountNumber)
        {
            // Implement a method to find the account by account number
            // You can search in the list of BankAccounts or use a different data structure.
            // Return null if the account is not found.
            // Example: 
            return accountsList.Find(account => account.AccountNumber == accountNumber);
        }

    }
}
