﻿using System;
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
                    List<string[]> operationsList = new List<string[]>();
                    List<BankAccount> bankAccountsList = new List<BankAccount>();
                    List<int> transactionIdList = new List<int>();
                    decimal totalAmountOKTransactions = 0M;

                    ReadInputFile(mngrPath);
                    Console.WriteLine();
                    ReadInputFile(oprtPath);
                    Console.WriteLine();
                    ReadInputFile(trxnPath);
                    Console.WriteLine();

                    // la ligne suivante convertie le string du champ date en DateTime et nous l'utilisons ensuite pour trier la liste
                    operationsList = operationsList.OrderBy(arr => DateTime.Parse(arr[1])).ToList();
                    TreatOperations(operationsList);

                    Console.WriteLine("----------- Résultats des comptes bancaires après traitement.");
                    Console.WriteLine();
                    DisplayAccountsList();

                    Console.ReadLine();

                    /***********************************************************************************************************************/

                    void TreatOperations(List<string[]> listOpe)
                    {
                        Console.WriteLine();
                        Console.WriteLine("-------------- Traitement des opérations : ");
                        Console.WriteLine();

                        foreach (string[] operation in listOpe)
                        {
                            switch (operation[5])
                            {
                                case "bankaccount":
                                    TreatBankAccountLine(operation);
                                    break;

                                case "transaction":
                                    TreatTransactionLine(operation);
                                    break;

                                default:
                                    Console.WriteLine($"Type d'opération inconnu : {operation[5]}");
                                    break;
                            }
                            Console.WriteLine();
                        }
                    }

                    void TreatBankAccountLine(string[] values)
                    {
                        //Console.WriteLine("values[0] : " + values[0]);
                        //Console.WriteLine("values[1] : " + values[1]);
                        //Console.WriteLine("values[2] : " + values[2]);
                        //Console.WriteLine("values[3] : " + values[3]);
                        //Console.WriteLine("values[4] : " + values[4]);
                        //Console.WriteLine("values[5] : " + values[5]);
                        //Console.WriteLine(values[0] + " " + values[1] + " " + values[2] + " " + values[3] + " " + values[4] + " " + values[5]);

                        int bankAccNbr = int.Parse(values[0]);
                        DateTime dateOpe = DateTime.Parse(values[1]);
                        decimal.TryParse(values[2], out decimal balance);
                        int.TryParse(values[3], out int entry);
                        int.TryParse(values[4], out int exit);

                        #region Création, Suppression ou Transfert de Compte Bancaire
                        if (entry > 0 && exit == 0) 
                        {
                            bool accountNumberAlreadyExists = bankAccountsList.Any(acc => acc.AccountNumber == bankAccNbr);

                            if (accountNumberAlreadyExists)
                            {
                                Console.WriteLine($"Le n°{bankAccNbr} de compte bancaire existe déjà chez la banque.");
                            }
                            else if (balance < 0)
                            {
                                Console.WriteLine($"La balance du compte ({balance}) ne peut pas être négative pour créer le compte bancaire.");
                            }
                            else
                            {
                                AccountManager entryAccManager = FindAccountManagerByNumber(entry);

                                if (entryAccManager != null)
                                {
                                    BankAccount newlyCreatedBankAcc = entryAccManager.CreateBankAccount(bankAccNbr, balance);
                                    if (newlyCreatedBankAcc != null)
                                    {
                                        bankAccountsList.Add(newlyCreatedBankAcc);
                                        Console.WriteLine($"Creation de compte bancaire {bankAccNbr} chez le gestionnaire n°{entry}.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Le gestionnaire de compte n°{entry} n'existe pas. Création de compte avortée.");
                                }
                            }
                        }
                        else if (entry == 0 && exit > 0)
                        {
                            AccountManager exitAccManager = FindAccountManagerByNumber(exit);
                            if (exitAccManager != null)
                            {
                                BankAccount bankAccToDelete = FindBankAccountByNumber(bankAccNbr);
                                if (bankAccToDelete != null)
                                {
                                    exitAccManager.TerminateBankAccount(bankAccNbr);
                                }
                                else
                                {
                                    Console.WriteLine($"Le compte bancaire n°{bankAccNbr} n'a pas été trouvé et ne peut donc être supprimé");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Le gestionnaire de comptes n°{exit} n'existe pas. Il nous est impossible de supprimé le compte bancaire n°{bankAccNbr}");
                            }
                        }
                        else if (entry > 0 && exit > 0)
                        {
                            AccountManager entryAccManager = FindAccountManagerByNumber(entry);
                            AccountManager exitAccManager = FindAccountManagerByNumber(exit);

                            if (entryAccManager != null || exitAccManager != null)
                            {
                                entryAccManager.InitiateTransferBankAccountOwnershipRequest(exitAccManager, bankAccNbr);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Erreur Entry Exit : {entry} / {exit}");
                        }
                        #endregion
                    }

                    void TreatTransactionLine(string[] values)
                    {
                        //Console.WriteLine(values[0] + " " + values[1] + " " + values[2] + " " + values[3] + " " + values[4] + " " + values[5]);

                        int transactionId = int.Parse(values[0]);
                        DateTime dateEff = DateTime.Parse(values[1]);
                        decimal.TryParse(values[2], out decimal amount);
                        int.TryParse(values[3], out int senderBkAccNumber);
                        int.TryParse(values[4], out int recipientBkAccNumber);

                        BankAccount senderAccount = FindBankAccountByNumber(senderBkAccNumber);
                        BankAccount recipientAccount = FindBankAccountByNumber(recipientBkAccNumber);

                        string status = "KO";

                        bool idAlreadyExists = transactionIdList.Any(id => id == transactionId);

                        if (senderBkAccNumber == 0 && recipientBkAccNumber == 0)
                        {
                            Console.WriteLine("Double Zéro, erreur. La nature donne à la nature de l'argent, les banquiers sont énervés!.");
                        }
                        else
                        {
                            if (!idAlreadyExists && !(senderBkAccNumber == 0 && recipientBkAccNumber == 0))
                            {
                                if ((senderAccount != null || senderBkAccNumber == 0) && (recipientAccount != null || recipientBkAccNumber == 0))
                                {
                                    if (amount > 0)
                                    {
                                        transactionIdList.Add(transactionId);
                                        bool res;
                                        if (recipientBkAccNumber == 0)
                                        {
                                            res = senderAccount.Withdraw(amount);
                                        }
                                        else if (senderBkAccNumber == 0)
                                        {
                                            res = recipientAccount.Deposit(amount);
                                        }
                                        else
                                        {
                                            new Transaction(transactionId, amount, senderBkAccNumber, recipientBkAccNumber);
                                            res = senderAccount.Transfer(recipientAccount, amount);
                                        }
                                        if (res)
                                        {
                                            ++totalAmountOKTransactions;
                                            status = "OK";
                                        }       
                                    }
                                    else
                                    {
                                        Console.WriteLine("Le montant de la transaction doit être un nombre positif. Transaction Avortée.");
                                    }
                                }
                                else
                                {
                                    StringBuilder messageBuilder = new StringBuilder();

                                    if (senderAccount == null) messageBuilder.Append($"Compte expéditeur n°{senderBkAccNumber} non-trouvé.");
                                    if (recipientAccount == null) messageBuilder.Append($"Compte destinataire n°{recipientBkAccNumber} non-trouvé.");

                                    string message = messageBuilder.ToString().Trim();
                                    Console.WriteLine(message + " Transaction Avortée.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Le transactionId {transactionId} a déjà été utilisé pour une précédente transaction.");
                            }
                        }
                        string statusLine = $"{transactionId};{status}";
                        File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Transactions_Status_1.csv"),
                            Environment.NewLine + statusLine);
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

                                        else if (filePath.Contains("Comptes"))
                                        {
                                            string[] newValues = new string[6];
                                            Array.Copy(values, newValues, values.Length);
                                            newValues[5] = "bankaccount";
                                            operationsList.Add(newValues);
                                        }
                                        else if (filePath.Contains("Transactions"))
                                        {
                                            string[] newValues = new string[6];
                                            Array.Copy(values, newValues, values.Length);
                                            newValues[5] = "transaction";
                                            operationsList.Add(newValues);
                                        }
                                        else
                                        {
                                            Console.WriteLine("No correct file");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Une erreur s'est produite lors de la lecture du fichier: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le fichier n'existe pas.");
                        }
                    }

                    BankAccount FindBankAccountByNumber(int accountNumber)
                    {
                        return bankAccountsList.Find(account => account.AccountNumber == accountNumber);
                    }

                    AccountManager FindAccountManagerByNumber(int accountNumber)
                    {
                        return accountManagersList.Find(account => account.AccManagerId == accountNumber);
                    }

                    void DisplayAccountsList()
                    {
                        foreach (var account in bankAccountsList)
                        {
                            Console.WriteLine($"Compte n°{account.AccountNumber}, Solde: {account.GetBalance()}");
                        }
                    }
                }
                #endregion
            }

        }

    }
}
