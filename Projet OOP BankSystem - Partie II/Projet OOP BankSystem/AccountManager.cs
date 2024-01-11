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
        public decimal ManagementFees { get; set; }
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
            if (clientType == "Entreprise") return 40.54M;
            else if (clientType == "Particulier") return 86.55M;
            else return 0.00M;
        }
    }

}
