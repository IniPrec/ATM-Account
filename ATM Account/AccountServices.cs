using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ATM_Account
{
    class AccountServices
    {
        private static readonly string filePath = "C:\\Users\\preciousoyebanjo\\source\\repos\\ATM Account\\ATM Account\\account.txt";
        public void SaveAccount(Account account)
        {
            string accountData = $"{account.UserId},{account.AccountNumber},{account.Balance}";
            File.AppendAllText(filePath, accountData + Environment.NewLine);
        }

        public Account GetAccountByUserId(Guid userId)
        {

            if (!File.Exists(filePath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] part = line.Split(',');
                if (Guid.Parse(part[0]) == userId)
                {
                    Account account = new Account(long.Parse(part[1]), double.Parse(part[2]));
                    account.UserId = Guid.Parse(part[0]);
                    return account;
                }
            }

            return null;
        }

        public Account GetAccountByAccountNumber(long accountNumber)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] part = line.Split(',');
                if (long.Parse(part[1]) == accountNumber)
                {
                    Account account = new Account(long.Parse(part[1]), double.Parse(part[2]));
                    account.UserId = Guid.Parse(part[0]);
                    return account;
                }
            }

            return null;
        }

        public void UpdateAccount(Account account)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] part = lines[i].Split(',');

                if (Guid.Parse(part[0]) == account.UserId)
                {
                    string updated = $"{account.UserId},{account.AccountNumber},{account.Balance}";
                    lines[i] = updated;
                }
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}