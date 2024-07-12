using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    abstract internal class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        protected bool _executed;
        protected bool _reversed;
        private DateTime _dateStamp;

        public Transaction(decimal amount)
        {
            this._amount = amount;
            this._dateStamp = DateTime.Now;
        }

        public virtual void Print()
        {
            Console.WriteLine(_dateStamp.ToString());
        }
           

        public virtual void Execute()
        {

        }

        public virtual void Rollback()
        {

        }

        abstract public bool Success
        {
            get;
        }
        public bool Executed
        {
            get { return this._executed; }
        }
        public bool Reversed
        {
            get { return this._reversed; }
        }
        public DateTime DateStamp
        {
            get { return this._dateStamp; }
        } 
    }
}
