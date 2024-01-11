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
        public int TransactionsCount { get; set; }
        public decimal ManagementFees { get; set; }  // calculate as a % 
        public List<BankAccount> AffiliatedBankAccounts { get; set; }

        // Constructor
        public AccountManager(int accId, string cType, int trCount)
        {
            AccManagerId = accId;
            ClientType = cType;
            TransactionsCount = trCount;
            ManagementFees = CalculateManagementFees(cType);
        }

        public decimal CalculateManagementFees(string clientType)
        {
            if (clientType == "Entreprise") return 10.00M;
            else if (clientType == "Particulier") return 0.01M; // calculate as a % 
            else return 0.00M;
        }

        public void CreateBankAccount(int bankAccNbr, decimal initialBalance)
        {
            bool exist = AffiliatedBankAccounts.Any(acc => acc.AccountNumber == bankAccNbr);

            if (!exist)
            {
                BankAccount newAcc = new BankAccount(bankAccNbr, decimal.Parse(initialBalance), 1000);
                AffiliatedBankAccounts.Add(newAcc);
            }
            else
            {
                Console.WriteLine($"Ce compte n°{bankAccNbr} existe déjà !");
            }
        }

        public void TerminateBankAccount(int bankAccNbr)
        {
            bool exist;
            // find if account already existe 

            if (exist)
            {
                // delete bank account
            }
            else
            {
                Console.WriteLine($"Le compte {bankAccNbr} n'existe pas.");
            }
        }

        public void InitiateTransferBankAccountOwnershipRequest(AccountManager targetNewOwner, int targetBankAccNbr)
        {
            List<BankAccount> newOwnerBankAccounts = targetNewOwner.AffiliatedBankAccounts;
            bool exist = newOwnerBankAccounts.Any(acc => acc.AccountNumber == targetBankAccNbr);
            BankAccount targetedBankAcc = newOwnerBankAccounts.Find(acc => acc.AccountNumber == targetBankAccNbr);

            if (exist) targetNewOwner.ApproveTransferBankAccountOwnershipRequest(targetedBankAcc);
            else Console.WriteLine($"Le client {targetNewOwner.AccManagerId} n'a pas de compte bancaire avec le n°{targetBankAccNbr}.");
        }

        public void ApproveTransferBankAccountOwnershipRequest(BankAccount targetedBankAcc)
        {
            targetedBankAcc.AccountNumber = AccManagerId;
        }
    }

}
