using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Account
{
    class Account
    {
        public long AccountNumber { get; }
        public double Balance { get; private set; }
        public Guid UserId { get; set; }

        public Account(long accountNumber, double balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public Account()
        {
            Random random = new Random();
            AccountNumber = random.NextInt64(1000000000, 9999999999);
            Balance = 0;
        }

        private double GetValidAmount(string prompt)
        {
            double amount;
            bool isValid;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                isValid = double.TryParse(input, out amount);

                if (!isValid)
                {
                    Console.WriteLine("Input must be numbers only.");
                }

                if (amount < 0)
                {
                    Console.WriteLine("Amount must be greater than 0.");
                }

            } while (!isValid || amount < 0);

            return amount;
        }

        private double BalanceChecker(string prompt)
        {
            double amount = GetValidAmount(prompt);

            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds!");
                return -1;
            }

            return amount;
        }

        AuthServices auth = new AuthServices();

        public bool Deposit(User user)
        {
            if (auth.VerifyUser(user))
            {
                double depositAmount;
                depositAmount = GetValidAmount("Enter amount to deposit: ");
                Balance += depositAmount;
                Console.WriteLine("Deposit Successful.");
                Console.WriteLine();
                return true;
            }

            return false;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public bool Withdraw(User user)
        {
            double withdrawAmount;

            if (auth.VerifyUser(user))
            {
                withdrawAmount = BalanceChecker("Enter amount to withdraw: ");

                if (withdrawAmount == -1)
                {
                    return false;
                }

                Balance -= withdrawAmount;
                Console.WriteLine("Withdrawal Successful!");
                Console.WriteLine();
                return true;
            }

            return false;
        }

        public bool Transfer(User user)
        {
            double transferAmount;
            long transferToAccount;
            bool isAccountValid;
            AccountServices accountServices = new AccountServices();
            Account recipient;

            if (auth.VerifyUser(user))
            {
                do
                {
                    Console.Write("Enter the recipient's account number: ");
                    string transferAccountInput = Console.ReadLine();
                    isAccountValid = long.TryParse(transferAccountInput, out transferToAccount);
                    recipient = accountServices.GetAccountByAccountNumber(transferToAccount);

                    if (!isAccountValid)
                    {
                        Console.WriteLine("Invalid input! Please enter a number.");
                        Console.WriteLine();
                    }

                    if (transferToAccount.ToString().Length != 10)
                    {
                        Console.WriteLine("Enter a valid account number!");
                        Console.WriteLine();
                    }

                    if (recipient == null)
                    {
                        Console.WriteLine("Recipient's Account not found");
                        Console.WriteLine();
                    }
                } while (!isAccountValid || transferToAccount.ToString().Length != 10 || recipient == null);

                transferAmount = BalanceChecker("Enter amount to transfer: ");

                if (transferAmount == -1)
                {
                    return false;
                }

                Balance -= transferAmount;
                recipient.Deposit(transferAmount);
                accountServices.UpdateAccount(recipient);
                Console.WriteLine("Transfer Successful!");
                Console.WriteLine();
                return true;
            }

            return false;
        }

        public bool CheckBalance(User user)
        {
            if (auth.VerifyUser(user))
            {
                Console.WriteLine($"Your balance is {Balance:C}");
                Console.WriteLine();
                return true;
            }

            return false;
        }
    }
}