using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serie_IV
{
    public class PhoneBook
    {
        private Dictionary<string, string> phoneBook = new Dictionary<string, string>();

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10 && phoneNumber[0] == '0' && phoneNumber[1] != '0') return true;
            else return false;
        }

        public bool ContainsPhoneContact(string phoneNumber)
        {
            return phoneBook.ContainsKey(phoneNumber);
        }

        public void PhoneContact(string phoneNumber)
        {
            if (ContainsPhoneContact(phoneNumber))
            {
                string name = phoneBook[phoneNumber];
                Console.WriteLine($"Contact found: {name} - {phoneNumber}");
            }
            else
            {
                Console.WriteLine($"Contact with phone number {phoneNumber} not found.");
            }
        }

        public bool AddPhoneNumber(string phoneNumber, string name)
        {
            bool isValid = IsValidPhoneNumber(phoneNumber);

            if (name.Length == 0) {
                Console.WriteLine("Le nom ne contient aucun caractère.");
                return false;
            } else if (!isValid) {
                Console.WriteLine($"Le numéro de téléphone {phoneNumber} est incorrect.");
                return false;
            } else if (ContainsPhoneContact(phoneNumber))
            {
                Console.WriteLine($"Ce numéro {phoneNumber} est déjà associé à un contact.");
                return false;
            } else
            {
                phoneBook.Add(phoneNumber, name);
                Console.WriteLine($"Contact ajouté : {name} - {phoneNumber}");
                return true;
            }
        }

        public bool DeletePhoneNumber(string phoneNumber)
        {
            if (ContainsPhoneContact(phoneNumber))
            {
                phoneBook.Remove(phoneNumber);
                Console.WriteLine($"Contact with phone number {phoneNumber} deleted successfully.");
                return true;
            }
            else
            {
                Console.WriteLine($"Contact with phone number {phoneNumber} not found. Deletion failed.");
                return false;
            }
        }

        public void DisplayPhoneBook()
        {
            if (phoneBook.Count == 0)
            {
                Console.WriteLine("Phone book is empty.");
            }
            else
            {
                Console.WriteLine("Phone Book:");
                foreach (var entry in phoneBook)
                {
                    Console.WriteLine($"{entry.Value} - {entry.Key}");
                } 
            }
        }
    }
}
