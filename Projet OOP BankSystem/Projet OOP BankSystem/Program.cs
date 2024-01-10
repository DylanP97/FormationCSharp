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
            string path = Directory.GetCurrentDirectory();
            #region Files
            // Input
            string acctPath = path + @"\bank_accounts.csv";
            string trxnPath = path + @"\transactions.csv";
            // Output
            string sttsPath = path + @"\transactions_status.csv";
            #endregion

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Projet OOP");
            Console.WriteLine("Banking System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();

            //string accountsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bank_accounts.csv");
            //string transactionsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.csv");
            //string transactionsStatusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions_status.csv");
            ReadAndDisplayCsvFile(acctPath);
            Console.WriteLine();
            ReadAndDisplayCsvFile(trxnPath);
            Console.WriteLine();
            ReadAndDisplayCsvFile(sttsPath);

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            DisplayAccountsList();
            Console.WriteLine();
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
            if (int.TryParse(values[0], out int accountNumber))
            {
                decimal.TryParse(values[1], out decimal balance); // if field is empty, the balance will go to 0
                BankAccount bankAccount = new BankAccount(accountNumber, balance, 1000);
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

                string status;

                if ((senderAccount != null || senderAccountNumber == 0) && (recipientAccount != null || recipientAccountNumber == 0))
                {
                    if (amount > 0)
                    {
                        status = "ok";
                        if (recipientAccountNumber == 0) senderAccount.Withdraw(amount);
                        else if (senderAccountNumber == 0) recipientAccount.Deposit(amount);
                        else
                        {
                            new Transaction(transactionId, amount, senderAccountNumber, recipientAccountNumber);
                            senderAccount.Transfer(recipientAccount, amount);
                        }
                    }
                    else
                    {
                        status = "ko";
                        Console.WriteLine("Le montant de la transaction doit être un nombre positif");
                    }
                }
                else
                {
                    status = "ko";

                    if (senderAccount == null)
                    {
                        Console.WriteLine($"Compte expéditeur non-trouvé. Compte n°{senderAccountNumber}");
                    }
                    if (recipientAccount == null)
                    {
                        Console.WriteLine($"Compte destinataire non-trouvé. Compte n°{recipientAccountNumber}");
                    }
                }

                // Create a line for the status CSV
                string statusLine = $"{transactionId},{status}";
                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions_status.csv"), Environment.NewLine + statusLine);
            }
            else
            {
                Console.WriteLine("Invalid data format in CSV values for transactions.");
            }
        }

        static BankAccount FindAccountByNumber(int accountNumber)
        {
            return accountsList.Find(account => account.AccountNumber == accountNumber);
        }

        static void DisplayAccountsList()
        {
            Console.WriteLine("Accounts List after treatment:");
            foreach (var account in accountsList)
            {
                Console.WriteLine($"Account N°: {account.AccountNumber}, Balance: {account.GetBalance()}");
            }
        }
    }
}
