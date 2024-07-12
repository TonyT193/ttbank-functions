using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    internal class Account
    {
        private decimal _balance;
        private string _name;

        public String Name
        {
            get { return this._name; }
        }

        public Account(String name, decimal balance)
        {
            this._balance = balance;
            this._name = name;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a positive number greater than 0 ");
                Console.WriteLine();
                return false;
            }

            this._balance += amount;

            return true;

        }
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a positive number greater than 0 ");
                Console.WriteLine();
                return false;
            }
            else if (amount > this._balance)
            {
                Console.WriteLine();
                Console.WriteLine("Insufficient funds ");
                Console.WriteLine();
                return false;
            }

            this._balance -= amount;

            return true;
        }
        public void Print()
        {
            Console.WriteLine("Name of account: " + _name);
            Console.WriteLine("Balance of account: " + _balance);
        }
    }
}
