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
            #region Entête
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Project OOP");
            Console.WriteLine("Banking System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();
            #endregion

            for (int i = 1; i < 7; i++)
            {
                #region Files
                // Input
                string mngrPath = Directory.GetCurrentDirectory() + $@"\Gestionnaires_{i}.txt";
                string oprtPath = Directory.GetCurrentDirectory() + $@"\Comptes_{i}.txt";
                string trxnPath = Directory.GetCurrentDirectory() + $@"\Transactions_{i}.txt";
                // Output
                string sttsOprtPath = Directory.GetCurrentDirectory() + $@"\StatutOpe_{i}.txt";
                string sttsTrxnPath = Directory.GetCurrentDirectory() + $@"\StatutTra_{i}.txt";
                string mtrlPath = Directory.GetCurrentDirectory() + $@"\Metrologie_{i}.txt";
                #endregion

                #region Main
                if (File.Exists(mngrPath) && File.Exists(oprtPath) && File.Exists(trxnPath))
                {
                    //TODO : votre code
                    List<AccountManager> accountManagersList = new List<AccountManager>();
                    List<string[]> operationsList = new List<string[]>;
                    List<BankAccount> bankAccountsList = new List<BankAccount>();
                    List<int> transactionIdList = new List<int>();

                    ReadInputFile(oprtPath);
                    Console.WriteLine();
                    ReadInputFile(trxnPath);
                    Console.WriteLine();
                    ReadInputFile(mngrPath);

                    // ici nous convertissons le string du champ date en DateTime et nous l'utilisons ensuite pour trier la liste
                    operationsList = operationsList.OrderBy(arr => DateTime.Parse(arr[1])).ToList();

                    TreatOperations(operationsList);

                    Console.ReadLine();

                    /***********************************************************************************************************************/

                    void TreatOperations(List<string[]> listOpe)
                    {
                        // lire la liste d'opérations puis exécuter TreatBankAccountLine() ou TreatTransactionLine() selon la ligne
                    }

                    void TreatBankAccountLine(string[] values)
                    {
                        int bankAccNbr = int.Parse(values[0]);
                        DateTime dateOpe = DateTime.Parse(values[1]);
                        decimal balance = decimal.Parse(values[2]);
                        int entry = int.Parse(values[3]);
                        int exit = int.Parse(values[4]);

                        bool accountNumberAlreadyExists = bankAccountsList.Any(acc => acc.AccountNumber == bankAccNbr);

                        if (accountNumberAlreadyExists)
                        {
                            Console.WriteLine("Le numéro de compte existe déjà.");
                        }
                        else
                        {
                            if (balance >= 0)
                            {
                                CreateBankAccount
                                BankAccount bankAccount = new BankAccount(accountNumber, decimal.Parse(formattedBalance), 1000);
                                if (bankAccount != null) bankAccountsList.Add(bankAccount);
                            }
                            else
                            {
                                Console.WriteLine("La balance du compte ne peut pas être négative.");
                            }
                        }

                    }

                    void TreatTransactionLine(string[] values)
                    {
                        if (int.TryParse(values[0], out int transactionId) &&
                            decimal.TryParse(values[1], out decimal amount) &&
                            int.TryParse(values[2], out int senderAccountNumber) &&
                            int.TryParse(values[3], out int recipientAccountNumber))
                        {
                            BankAccount senderAccount = FindAccountByNumber(senderAccountNumber);
                            BankAccount recipientAccount = FindAccountByNumber(recipientAccountNumber);

                            string status = "KO";

                            bool idAlreadyExists = transactionIdList.Any(id => id == transactionId);

                            if (senderAccountNumber == 0 && recipientAccountNumber == 0)
                            {
                                Console.WriteLine("Double Zéro, erreur.");
                            }
                            else
                            {
                                if (!idAlreadyExists && !(senderAccountNumber == 0 && recipientAccountNumber == 0))
                                {
                                    if ((senderAccount != null || senderAccountNumber == 0) && (recipientAccount != null || recipientAccountNumber == 0))
                                    {
                                        if (amount > 0)
                                        {
                                            transactionIdList.Add(transactionId);
                                            bool res;
                                            if (recipientAccountNumber == 0)
                                            {
                                                res = senderAccount.Withdraw(amount);
                                            }
                                            else if (senderAccountNumber == 0)
                                            {
                                                res = recipientAccount.Deposit(amount);
                                            }
                                            else
                                            {
                                                new Transaction(transactionId, amount, senderAccountNumber, recipientAccountNumber);
                                                res = senderAccount.Transfer(recipientAccount, amount);
                                            }
                                            if (res) status = "OK";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Le montant de la transaction doit être un nombre positif");
                                        }
                                    }
                                    else
                                    {
                                        if (senderAccount == null)
                                        {
                                            Console.WriteLine($"Compte expéditeur non-trouvé. Compte n°{senderAccountNumber}");
                                        }
                                        if (recipientAccount == null)
                                        {
                                            Console.WriteLine($"Compte destinataire non-trouvé. Compte n°{recipientAccountNumber}");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Le transactionId {transactionId} a déjà été utilisé.");
                                }
                            }
                            string statusLine = $"{transactionId};{status}";
                            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Transactions_Status_1.csv"),
                                Environment.NewLine + statusLine);
                        }
                        else
                        {
                            Console.WriteLine("Invalid data format in CSV values for transactions.");
                        }
                    }

                    void TreatAccountManagerLine(string[] values)
                    {
                        int accId = int.Parse(values[0]);
                        string cType = values[1];
                        int trCount = int.Parse(values[2]);

                        AccountManager accMng = new AccountManager(accId, cType, trCount);
                        accountManagersList.Add(accMng);
                    }

                    void ReadInputFile(string filePath)
                    {
                        if (File.Exists(filePath))
                        {
                            try
                            {
                                Console.WriteLine(filePath + " :");
                                Console.WriteLine();
                                using (StreamReader reader = new StreamReader(filePath))
                                {
                                    while (!reader.EndOfStream)
                                    {
                                        string line = reader.ReadLine();
                                        string[] values = line.Split(';');
                                        Console.WriteLine(line);

                                        if (filePath.Contains("Gestionnaires")) TreatAccountManagerLine(values);

                                        if (filePath.Contains("Comptes") || filePath.Contains("Transactions"))
                                        {
                                            operationsList.Add(values);
                                        }                                            
                                        //TreatBankAccountLine(values);
                                        //TreatTransactionLine(values);
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
                            Console.WriteLine("The file does not exist.");
                        }
                    }

                    BankAccount FindAccountByNumber(int accountNumber)
                    {
                        return bankAccountsList.Find(account => account.AccountNumber == accountNumber);
                    }

                    void DisplayAccountsList()
                    {
                        Console.WriteLine("Accounts List after treatment:");
                        foreach (var account in bankAccountsList)
                        {
                            Console.WriteLine($"Account N°: {account.AccountNumber}, Balance: {account.GetBalance()}");
                        }
                    }
                }
                #endregion
            }

        }

    }
}
