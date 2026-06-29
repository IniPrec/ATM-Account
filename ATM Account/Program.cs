using System;

namespace ATM_Account
{
    public class Program
    {
        static bool Continue()
        {
            Console.WriteLine("Do you want to perform another transaction? (yes/no)");
            string response = Console.ReadLine().ToLower();
            return response == "yes";
        }

        public static void Main()
        {
            User user = new User();
            UserServices userServices = new UserServices();
            Account account = new Account();
            AuthServices auth = new AuthServices();
            AccountServices accountServices = new AccountServices();

            Console.WriteLine(".....GSM ATM.....");
            Console.WriteLine(".................");
            Console.WriteLine();

            Console.WriteLine("Choose an option to start.");
            Console.WriteLine();

            bool validAtmChoice;
            int atmChoice;
            bool loggedIn = false;
            int startChoice;
            bool validStartChoice;

            bool running = true;
            while (running)
            {
                do
                {
                    Console.WriteLine("1. New User\n2. Existing User\n3. Exit. (Enter 1, 2 or 3)");
                    Console.WriteLine();

                    string choiceInput = Console.ReadLine();
                    validStartChoice = int.TryParse(choiceInput, out startChoice);
                    Console.WriteLine();

                    if (!validStartChoice)
                    {
                        Console.WriteLine("Please enter a valid choice (only numbers)");
                    }

                    if (startChoice < 1 || startChoice > 3)
                    {
                        Console.WriteLine("Choose a valid option.");
                    }
                } while (!validStartChoice || startChoice < 1 || startChoice > 3);

                switch (startChoice)
                {
                    case 1:
                        user = new User();
                        account = new Account();
                        user.CreateAccount();
                        userServices.SaveUser(user);
                        account.UserId = user.UserId;
                        accountServices.SaveAccount(account);
                        Console.WriteLine($"Account created successfully! Your account number is {account.AccountNumber}. Please log in");
                        Console.WriteLine();
                        break;

                    case 2:
                        user = auth.LoginUsers();
                        if (user != null)
                        {
                            account = accountServices.GetAccountByUserId(user.UserId);
                            loggedIn = true;
                        }
                        break;

                    case 3:
                        running = false;
                        break;

                }

                while (loggedIn == true)
                {
                    Console.WriteLine("Choose an option");
                    Console.WriteLine();

                    do
                    {
                        Console.WriteLine("1. Deposit\n2. Withdraw\n3. Transfer\n4. Check balance\n 5. Log out");
                        Console.WriteLine();

                        string atmInput = Console.ReadLine();
                        validAtmChoice = int.TryParse(atmInput, out atmChoice);
                        Console.WriteLine();

                        if (!validAtmChoice)
                        {
                            Console.WriteLine("Please enter a valid choice (only numbers)");
                        }

                        if (atmChoice < 1 || atmChoice > 5)
                        {
                            Console.WriteLine("Choose a valid option.");
                        }
                    } while (!validAtmChoice || atmChoice < 1 || atmChoice > 5);

                    switch (atmChoice)
                    {
                        case 1:
                            if (account.Deposit(user))
                            {
                                accountServices.UpdateAccount(account);
                                if (!Continue())
                                {
                                    loggedIn = false;
                                }
                            }
                            break;

                        case 2:
                            if (account.Withdraw(user))
                            {
                                accountServices.UpdateAccount(account);
                                if (!Continue())
                                {
                                    loggedIn = false;
                                }
                            }
                            break;

                        case 3:
                            if (account.Transfer(user))
                            {
                                accountServices.UpdateAccount(account);
                                if (!Continue())
                                {
                                    loggedIn = false;
                                }
                            }
                            break;

                        case 4:
                            if (account.CheckBalance(user))
                            {
                                if (!Continue())
                                {
                                    loggedIn = false;
                                }
                            }
                            break;

                        case 5:
                            loggedIn = false;
                            break;
                    }
                }
            }
        }
    }
}