using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_OOP_BankSystem
{
    class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int SenderAccountNumber { get; set; }
        public int RecipientAccountNumber { get; set; }

        // Constructor
        public Transaction(int transactionId, decimal amount, int senderAccountNumber, int recipientAccountNumber)
        {
            TransactionId = transactionId;
            Amount = amount;
            SenderAccountNumber = senderAccountNumber;
            RecipientAccountNumber = recipientAccountNumber;
        }
    }
}
