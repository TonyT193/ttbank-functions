using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    internal class DepositTransaction : Transaction
    {
        private Account _account;
        //private decimal _amount;
        //private bool _executed;
        //private bool _success;
        //private bool _reversed;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            this._account = account;
            this._amount = amount;
        }

        public override void Print()
        {
            base.Print();
            if (this._success == true)
            {
                Console.WriteLine("Transaction successful");
                Console.WriteLine("Account name: " + this._account.Name);
                Console.WriteLine("Amount deposited: " + this._amount);
            }
            else
            {
                Console.WriteLine("Transaction unsuccessful");
            }
        }

        public override void Execute()
        {
            try
            {
                if (this._executed == false)
                {
                    this._executed = true;

                    if (this._account.Deposit(this._amount) == false)
                    {
                        this._success = false;
                        throw new InvalidOperationException("Must be greater than 0");
                    }

                    else
                    {
                        this._success = true;
                    }
                }
                else if (this._executed == true)
                {
                    throw new InvalidOperationException("Already executed");
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public override void Rollback()
        {
            try
            {
                if (this._reversed == false)
                {
                    if (this._success == true)
                    {
                        if (!this._account.Withdraw(this._amount))
                        {
                            throw new InvalidOperationException("Insufficient funds deposit");
                        }

                        //this._account.Withdraw(this._amount);    
                       
                    }
                    else
                    {
                        throw new InvalidOperationException("Deposit Not finalised");
                    }
               
                    this._reversed = true;               
                }
                else 
                {
                    throw new InvalidOperationException("Already reversed for deposit transaction");
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }




        /*public bool Executed
        {
            get { return this._executed; }
        }*/
        public override bool Success
        {
            get { return this._success; }
        }
        /*public bool Reversed
        {
            get { return this._reversed; }
        }*/
    }
}
