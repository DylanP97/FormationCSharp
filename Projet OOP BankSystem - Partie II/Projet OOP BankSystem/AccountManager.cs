using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_OOP_BankSystem
{
    class AccountManager
    {
        public int AccManagerId { get; set; }
        public string ClientType { get; set; }
        public int TransactionsCountLimit { get; set; }
        public decimal TotalTransactionManagementFees { get; set; }
        public List<BankAccount> AffiliatedBankAccounts { get; set; }

        // Constructor
        public AccountManager(int accId, string cType, int trCount)
        {
            AccManagerId = accId;
            ClientType = cType;
            TransactionsCountLimit = trCount;
            AffiliatedBankAccounts = new List<BankAccount>();
        }

        public BankAccount CreateBankAccount(int bankAccNbr, decimal initialBalance, DateTime dateOpe)
        {
            BankAccount accExisting = AffiliatedBankAccounts.Find(acc => acc.AccountNumber == bankAccNbr);
            if (accExisting == null)
            {
                BankAccount newAcc = new BankAccount(bankAccNbr, initialBalance, 1000, dateOpe, this);
                AffiliatedBankAccounts.Add(newAcc);
                return newAcc;
            }
            else
            {
                Console.WriteLine($"Le compte bancaire n°{bankAccNbr} existe déjà chez le gestionnaire {AccManagerId}! Création de compte bancaire avortée");
                return null;
            }
        }

        public bool TerminateBankAccount(int bankAccNbr, DateTime dateOpe)
        {
            bool exist = AffiliatedBankAccounts.Any(acc => acc.AccountNumber == bankAccNbr);

            if (exist)
            {
                BankAccount accToDelete = AffiliatedBankAccounts.Find(acc => acc.AccountNumber == bankAccNbr);
                AffiliatedBankAccounts.Remove(accToDelete);
                accToDelete.Owner = null;
                accToDelete.TerminationDate = dateOpe;
                Console.WriteLine($"Le compte bancaire n°{bankAccNbr} a été supprimé avec succès.");
                return true;
            }
            else
            {
                Console.WriteLine($"Le compte bancaire n°{bankAccNbr} n'appartient pas au gestionnaire de compte n°{AccManagerId}.");
                return false;
            }
        }

        public bool InitiateTransferBankAccountOwnershipRequest(AccountManager targetNewOwner, int targetBankAccNbr)
        {
            bool hasAccountOwnership = AffiliatedBankAccounts.Any(acc => acc.AccountNumber == targetBankAccNbr);
            if (hasAccountOwnership)
            {
                BankAccount targetedBankAcc = AffiliatedBankAccounts.Find(acc => acc.AccountNumber == targetBankAccNbr);
                targetNewOwner.ApproveTransferBankAccountOwnershipRequest(targetedBankAcc);
                return true;
            }
            else
            {
                Console.WriteLine($"Le gestionnaire {AccManagerId} n'est pas propriétaire du compte n°{targetBankAccNbr} et ne peut donc pas l'envoyer.");
                return false;
            }
        }

        public void ApproveTransferBankAccountOwnershipRequest(BankAccount targetedBankAcc)
        {
            AffiliatedBankAccounts.Add(targetedBankAcc);
            targetedBankAcc.Owner = this;
            Console.WriteLine($"Transfert de gestionnaire effectué! Le gestionnaire {AccManagerId} est le nouveau propriétaire " +
                $"du compte n°{targetedBankAcc.AccountNumber}");
        }
    }

}
