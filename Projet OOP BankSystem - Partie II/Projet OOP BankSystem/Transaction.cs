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
        public string TransactionType { get; set; }
        public decimal AmountDebited { get; set; }
        public decimal Fees { get; set; }
        public int SenderAccountNumber { get; set; }
        public int RecipientAccountNumber { get; set; }
        public DateTime EffectiveDate { get; set; }

        // Constructor
        public Transaction(int transactionId, string transactionType, decimal amountDebited, decimal managementFees, int senderAccountNumber, int recipientAccountNumber, DateTime effectiveDate)
        {
            TransactionId = transactionId;
            TransactionType = transactionType;
            AmountDebited = amountDebited;
            Fees= managementFees ;
            SenderAccountNumber = senderAccountNumber;
            RecipientAccountNumber = recipientAccountNumber;
            EffectiveDate = effectiveDate;
        }
    }
}
