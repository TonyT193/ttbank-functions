using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _7._2p
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account1 = new Account("a", 10);
            Account account2 = new Account("b", 10);
            //account1.Print();
            //account2.Print();

            Bank bank = new Bank();

            bank.AddAccount(account1);
            bank.AddAccount(account2);

            bool quit = false;
            while (!quit)
            {
                MenuOption option = ReadUserOption();

                switch (option)
                {
                    case MenuOption.CreateAccount:
                        CreateNewAccount(bank);
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(bank);
                        break;

                    case MenuOption.PrintTransactionHistory:
                        bank.PrintTransactionHistory();
                        DoRollback(bank);
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("Ending program");
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;

                }
            }

            Console.ReadLine();
        }
        public static MenuOption ReadUserOption()
        {
            decimal input;

            do
            {
                Console.WriteLine("Select from the following options: ");
                Console.WriteLine("1: Add new account");
                Console.WriteLine("2: Withdraw");
                Console.WriteLine("3: Deposit");
                Console.WriteLine("4: Transfer");
                Console.WriteLine("5: Print");
                Console.WriteLine("6: Print Transaction History");
                Console.WriteLine("7: Quit");

                input = ReadDecimal();

            } while (input < 1 || input > 7);

            return (MenuOption)input;
        }

        public static void DoDeposit(Bank bank)
        {
            try
            {
                Console.WriteLine("Enter amount to deposit: ");
                decimal deposit = ReadDecimal();

                Account account = FindAccount(bank);

                if (account == null)
                {
                    throw new InvalidOperationException("Account not found");
                }
                else
                {
                    DepositTransaction accountDeposit = new DepositTransaction(account, deposit);
                    bank.ExecuteTransaction(accountDeposit);
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }

        }

        public static void DoWithdraw(Bank bank)
        {
            try
            {
                Console.WriteLine("Enter amount to withdraw ");
                decimal withdraw = ReadDecimal();

                Account account = FindAccount(bank);

                if (account == null)
                {
                    throw new InvalidOperationException("Account not found");
                }
                else
                {
                    WithdrawTransaction accountWithdraw = new WithdrawTransaction(account, withdraw);
                    bank.ExecuteTransaction(accountWithdraw);
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public static void DoPrint(Bank bank)
        {
            try
            {
                Account account = FindAccount(bank);

                if (account == null)
                {
                    throw new InvalidOperationException("Account not found");
                }
                else
                {
                    account.Print();
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public static void DoTransfer(Bank bank)
        {
            try
            {
                Account account1 = FindAccount(bank);

                if (account1 == null)
                {
                    throw new InvalidOperationException("Account not found");
                }
                else
                {
                    Console.WriteLine("Enter amount to transfer from this account: ");
                    decimal transfer = ReadDecimal();

                    Console.WriteLine("To this account");
                    Account account2 = FindAccount(bank);

                    if (account2 == null)
                    {
                        throw new InvalidOperationException("Account not found");
                    }
                    else
                    {
                        TransferTransaction transfer1 = new TransferTransaction(account1, account2, transfer);

                        bank.ExecuteTransaction(transfer1);
                    }
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public static void DoRollback(Bank bank)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("1. Rollback");
                Console.WriteLine("2. No rollback");

                decimal rollback = ReadDecimal();

                if (rollback < 1 || rollback > 2)
                {
                    throw new InvalidOperationException("Invalid number. Please try again");
                }

                if (rollback == 1)
                {
                    Console.WriteLine("Select which index transaction to rollback: ");
                    decimal rollbackSelect = ReadDecimal();

                    bank.RollbackTransaction(rollbackSelect - 1);
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
            }
        }

        public static void CreateNewAccount(Bank bank)
        {
            Console.WriteLine("Enter an account name: ");
            String accountName = Console.ReadLine();

            Console.WriteLine("Please enter a balance");
            decimal accountBalance = ReadDecimal();
            Account addNewAccount = new Account(accountName, accountBalance);

            bank.AddAccount(addNewAccount);
        }

        private static Account FindAccount(Bank bank)
        {
            Console.WriteLine("Enter an account name: ");
            String searchAccountName = Console.ReadLine();

            Account account = bank.GetAccount(searchAccountName);

            if (account != null)
            {
                Console.WriteLine("Account found. ");
            }
            else
            {
                Console.WriteLine("Account not found. ");
            }

            return account;
        }

        public static decimal ReadDecimal()
        {
            Boolean validNumber = false;
            decimal number = 0;
            while (!validNumber)
            {
                try
                {
                    number = decimal.Parse(Console.ReadLine());
                    if (number < 0)
                    {
                        validNumber = false;
                        throw new FormatException("Must be greater than 0");
                    }
                    validNumber = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            return number;
        }

    }
    public enum MenuOption
    {
        CreateAccount = 1,
        Withdraw = 2,
        Deposit = 3,
        Transfer = 4,
        Print = 5,
        PrintTransactionHistory = 6,
        Quit = 7,
    }
}
