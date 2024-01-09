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
        public List<Transaction> TransactionsHistory { get; set; }

        // Constructor
        public BankAccount(int accountNumber, decimal balance, decimal maxWithdrawalLimit)
        {
            if (accountNumber <= 0) throw new ArgumentOutOfRangeException("Account number should be a unique positive integer.");

            AccountNumber = accountNumber;
            _balance = balance;
            MaxWithdrawalLimit = maxWithdrawalLimit;
            TransactionsHistory = new List<Transaction>();
        }

        // pour chaque méthodes mettre à jour le TransactionsHistory

        public void Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Un dépôt doit être positif.");
            else _balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Un retrait doit être positif.");
            else if (amount > MaxWithdrawalLimit) throw new ArgumentOutOfRangeException("Un retrait doit être inférieur à la limite de retrait.");
            else if (amount > _balance) throw new ArgumentOutOfRangeException("Un retrait doit être inférieur ou égal au solde du compte.");
            else _balance -= amount;
        }

        public void Transfer(BankAccount recipient, decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Un virement doit être positif.");
            else if (amount > MaxWithdrawalLimit) throw new ArgumentOutOfRangeException("Un virement doit être inférieur à la limite de retrait.");
            else if (amount > _balance) throw new ArgumentOutOfRangeException("Un virement doit être inférieur ou égal au solde du compte.");
            else
            {
                recipient._balance += amount;
                _balance -= amount;
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
    }
}
