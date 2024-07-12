using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    internal class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        //private decimal _amount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        //private bool _executed;
        //private bool _reversed;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._amount = amount;

            this._withdraw = new WithdrawTransaction(this._fromAccount, this._amount);
            this._deposit = new DepositTransaction(this._toAccount, this._amount);
        }

        public override void Print()
        {
            base.Print();
            if (Success == true)
            {
                Console.WriteLine("Transfered " + this._amount);
                this._withdraw.Print();
                this._deposit.Print();
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

                    if (this._amount < 0)
                    {
                        throw new InvalidOperationException("Amount must be greater than 0");
                    }

                    this._withdraw.Execute();

                    if (this._withdraw.Success == true)
                    {
                        this._deposit.Execute();
                    }

                    if (Success == true)
                    {
                        this._executed = true;
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
                    if (Success == true)
                    {

                        if (this._deposit.Reversed == false)
                        {
                            this._deposit.Rollback();

                            if (this._deposit.Reversed == false)
                            {
                                throw new InvalidOperationException("Insufficient funds for transfer transaction");
                            }
                        }

                        if (this._withdraw.Reversed == false)
                        {
                            this._withdraw.Rollback();

                            if (this._withdraw.Reversed == false)
                            {
                                throw new InvalidOperationException("Insufficient funds for transfer transaction");
                            }
                        }

                       
                        /*if (this._withdraw.Reversed == false || this._deposit.Reversed == false)
                        {
                            throw new InvalidOperationException("Insufficient funds for transfer transaction");
                        }*/


                        this._reversed = true;

                    }

                    if (Success == false)
                    {
                        throw new InvalidOperationException("Original transaction unsuccessful");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Already reversed for transfer transaction");
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
            get
            {
                if (this._withdraw.Success == true && this._deposit.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        /*public bool Reversed
        {
            get { return this._reversed; }
        }*/
    }
}
