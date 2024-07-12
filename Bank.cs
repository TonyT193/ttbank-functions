using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    internal class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public Bank()
        {
            this._accounts = new List<Account>();
            this._transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            this._accounts.Add(account);
        }
        public Account GetAccount(String name)
        {

            for (int i = 0; i < this._accounts.Count; i++)
            {
                if (name == this._accounts[i].Name)
                {
                    return this._accounts[i];
                }
            }

            return null;
        }
        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
            transaction.Print();
        }

        public void RollbackTransaction(decimal index)
        {
            try
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("Index must be greater than 0");
                }

                this._transactions[Convert.ToInt32(index)].Rollback();
                this._transactions[Convert.ToInt32(index)].Print();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine("Transaction doesn't exist");
                Console.WriteLine();
            }
        }

        public void PrintTransactionHistory()
        {
            if (this._transactions.Count == 0)
            {
                Console.WriteLine("No transactions");
            }
            else
            {
                for (int i = 0; i < this._transactions.Count; i++)
                {
                    Console.WriteLine("Index: " + (i + 1));
                    this._transactions[i].Print();
                    Console.WriteLine();
                }
            }       
        }


        /*public void ExecuteTransaction(WithdrawTransaction transaction)
        {
            transaction.Execute();
            transaction.Print();
        }
        public void ExecuteTransaction(TransferTransaction transaction)
        {
            transaction.Execute();
            transaction.Print();
        }*/



    }
}
