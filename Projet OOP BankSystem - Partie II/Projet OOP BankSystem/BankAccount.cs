using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_OOP_BankSystem
{
    class BankAccount
    {
        public int AccountNumber { get; set; } // Should be a unique positive integer
        private decimal? _balance { get; set; } // Null by default
        public decimal MaxWithdrawalLimit { get; set; }
        public int PeriodRangeMaxWithdrawal { get; set; } // 7 for 7 days // One week
        public List<Transaction> TransactionsHistory { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TerminationDate { get; set; }

        // Constructor
        public BankAccount(int accountNumber, decimal balance, decimal maxWithdrawalLimit)
        {
            if (accountNumber <= 0)
            {
               Console.WriteLine("Le numéro du compte bancaire doit être un nombre positif.");
               return;
            }
            if (balance < 0)
            {
                Console.WriteLine("Le solde doit être égal ou supérieur à 0.");
                return;
            }

            AccountNumber = accountNumber;
            _balance = balance;
            MaxWithdrawalLimit = maxWithdrawalLimit;
            TransactionsHistory = new List<Transaction>();
            //CreationDate = creationDate;
        }

        // pour chaque méthodes mettre à jour le TransactionsHistory

        public bool Deposit(decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Un dépôt doit être positif.");
                return false;
            }
            else
            {
                _balance += amount;
                Console.WriteLine($"Dépôt de {amount} euros effectué vers le compte n°{AccountNumber}.");
                return true;
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Un retrait doit être positif.");
                return false;
            }
            else if (amount > MaxWithdrawalLimit)
            {
                Console.WriteLine($"Un retrait doit être inférieur à la limite de retrait. Limite : {MaxWithdrawalLimit} // Montant à retirer : {amount}");
                return false;
            }
            else if (amount > _balance)
            {
                Console.WriteLine($"Un retrait doit être inférieur ou égal au solde du compte. Balance : {_balance} // Montant à retirer : {amount}");
                return false;
            }
            else
            {
                _balance -= amount;
                Console.WriteLine($"Retrait de {amount} euros effectué depuis le compte n°{AccountNumber}.");
                return true;
            }
        }

        public bool Transfer(BankAccount recipient, decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Un virement doit être positif.");
                return false;
            }
            else if (amount > MaxWithdrawalLimit)
            {
                Console.WriteLine($"Un virement doit être inférieur à la limite de retrait. Limite : {MaxWithdrawalLimit} // Montant à retirer : {amount}");
                return false;
            }                
            else if (amount > _balance)
            {
                Console.WriteLine($"Un virement doit être inférieur ou égal au solde du compte. Balance : {_balance} // Montant à retirer : {amount}");
                return false;
            }
            else
            {
                recipient._balance += amount;
                _balance -= amount;
                Console.WriteLine($"Transfert de {amount} euros effectué depuis le compte n°{AccountNumber} vers le compte n°{recipient.AccountNumber}");
                return true;
            }
        }

        public void InitiateDirectDebitRequest(BankAccount requestedAccount, decimal amount)
        {
            // Le compte expéditeur doit être en capacité d'effectuer le virement depuis leur compte
        }

        public void ApproveDirectDebitRequest(BankAccount requestingAccount, decimal amount)
        {
            Transfer(requestingAccount, amount);
        }

        public string GetBalance()
        {
            return _balance.ToString() + " euros";
        }
    }
}
