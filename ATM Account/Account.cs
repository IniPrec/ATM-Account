using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Account
{
    class Account
    {
        public long AccountNumber { get; }
        public double Balance { get; set; }
        public Guid UserId { get; }

        public Account()
        {
            Random random = new Random();
            AccountNumber = random.NextInt64(1000000000, 9999999999);
        }

        private double GetValidAmount(string prompt)
        {
            double inputAmount;
            bool isValid;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                isValid = double.TryParse(input, out inputAmount);

                if (!isValid)
                {
                    Console.WriteLine("Input must be numbers only.");
                }

                if (inputAmount < 0)
                {
                    Console.WriteLine("Amount must be greater than 0.");
                }
            } while (!isValid || inputAmount < 0);

            return inputAmount;
        }

        private double BalanceChecker(string prompt)
        {
            double inputAmount;

            do
            {
                inputAmount = GetValidAmount(prompt);
                if (inputAmount > Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                }
            } while (inputAmount > Balance);

            return inputAmount;
        }

        public void Deposit()
        {
            double depositAmount;

            depositAmount = GetValidAmount("Enter amount to deposit: ");
            Balance += depositAmount;
            Console.WriteLine("Deposit Successful!");
        }

        public void Withdraw()
        {
            double withdrawAmount;

            withdrawAmount = BalanceChecker("Enter amount to withdraw: ");
            Balance -= withdrawAmount;
            Console.WriteLine("Withdrawal Successful!");
        }

        public void Transfer()
        {
            double transferAmount;
            long transferToAccount;
            bool isAccountValid;

            do
            {
                Console.Write("Enter the recipient's account number: ");
                string transferAccountInput = Console.ReadLine();
                isAccountValid = long.TryParse(transferAccountInput, out transferToAccount);

                if (!isAccountValid)
                {
                    Console.WriteLine("Invalid Input! Please enter a number.");
                }

                if (transferToAccount.ToString().Length != 10)
                {
                    Console.WriteLine("Enter a valid account number!");
                }
            } while (!isAccountValid || transferToAccount.ToString().Length != 10);

            transferAmount = BalanceChecker("Enter amount to transfer: ");
            Balance -= transferAmount;
            Console.WriteLine("Transaction Successful");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your balance is {Balance}");
        }
    }
}
