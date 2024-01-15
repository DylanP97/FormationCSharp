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
        public int DaysRangeMaxWithdrawalLimit { get; set; } // 7 for 7 days // One week
        public List<Transaction> TransactionsHistory { get; set; }
        public AccountManager Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TerminationDate { get; set; }

        // Constructor
        public BankAccount(int accountNumber, decimal balance, decimal maxWithdrawalLimit, DateTime creationDate, AccountManager owner)
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
            MaxWithdrawalLimit = 2000M;
            DaysRangeMaxWithdrawalLimit = 7;
            TransactionsHistory = new List<Transaction>();
            CreationDate = creationDate;
            Owner = owner;
        }

        public bool Withdraw(decimal amount, Transaction transac)
        {
            if (amount < 0)
            {
                Console.WriteLine("Un retrait doit être positif.");
                return false;
            }
            else if (amount > _balance)
            {
                Console.WriteLine($"Un retrait doit être inférieur ou égal au solde du compte. Balance : {_balance} // Montant à retirer : {amount}");
                return false;
            }
            else if (amount > MaxWithdrawalLimit)
            {
                Console.WriteLine($"Un retrait doit être inférieur à la limite de retrait. Limite : {MaxWithdrawalLimit} // Montant à retirer : {amount}");
                return false;
            }
            else if (isWithdrawalAuthorized(amount, transac.EffectiveDate) == false)
            {
                Console.WriteLine($"Vous avez retirer plus de {MaxWithdrawalLimit} euros ces {DaysRangeMaxWithdrawalLimit} derniers jours.");
                return false;
            }
            else
            {
                TransactionsHistory.Add(transac);
                _balance -= amount;
                Console.WriteLine($"Retrait de {amount} euros effectué depuis le compte n°{AccountNumber}.");
                return true;
            }
        }

        private bool isWithdrawalAuthorized(decimal amount, DateTime dateTrs)
        {
            DateTime closingDateLimit = dateTrs.AddDays(-DaysRangeMaxWithdrawalLimit);

            // Calcul du montant total des retraits dans la période spécifiée
            decimal totalWithdrawalsInPeriod = TransactionsHistory
                .Where(t => t.EffectiveDate > closingDateLimit && t.EffectiveDate <= dateTrs)
                .Where(t => t.TransactionType == "Withdrawal")
                .Sum(t => t.AmountDebited);

            // Vérification si le montant total des retraits dans la période dépasse la limite autorisée
            if (totalWithdrawalsInPeriod + amount > MaxWithdrawalLimit)
            {
                return false;
            }

            return true;
        }

        public bool Deposit(decimal amount, Transaction transac)
        {
            if (amount < 0)
            {
                Console.WriteLine("Un dépôt doit être positif.");
                return false;
            }
            else
            {
                _balance += amount;
                TransactionsHistory.Add(transac);
                Console.WriteLine($"Dépôt de {amount} euros effectué vers le compte n°{AccountNumber}.");
                return true;
            }
        }

        public bool Transfer(BankAccount recipient, decimal amount, decimal managementFees, int idTransaction, DateTime dateEff)
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
                Transaction trst = new Transaction(idTransaction, "Transfer", amount, managementFees, this.AccountNumber, recipient.AccountNumber, dateEff);
                TransactionsHistory.Add(trst);
                _balance -= amount;
                recipient._balance += (amount - managementFees);
                Console.WriteLine($"Transfert de {amount} euros, dont {managementFees} euros de frais de gestion, effectué " +
                    $"depuis le compte n°{AccountNumber} vers le compte n°{recipient.AccountNumber}");
                return true;
            }
        }

        //public void InitiateDirectDebitRequest(BankAccount requestedAccount, decimal amount)
        //{
        //    // Le compte expéditeur doit être en capacité d'effectuer le virement depuis leur compte
        //}

        //public void ApproveDirectDebitRequest(BankAccount requestingAccount, decimal amount, Transaction transac)
        //{
        //    // Transfer(requestingAccount, amount, 0M, transac, DateTime.Now());
        //}

        public string GetBalance()
        {
            return _balance.ToString() + " euros";
        }
    }
}
