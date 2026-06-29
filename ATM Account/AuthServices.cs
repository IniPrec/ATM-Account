using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Account
{
    class AuthServices
    {
        public User LoginUsers()
        {
            UserServices userServices = new UserServices();
            string checkUserName;
            string checkPassword;
            User foundUser;

            do
            {
                Console.Write("Username: ");
                checkUserName = Console.ReadLine();
                Console.WriteLine();
                foundUser = userServices.GetUserByName(checkUserName);

                if (foundUser == null)
                {
                    Console.WriteLine("Username does not exist. Try again!");
                    Console.WriteLine();
                }
            } while (foundUser == null);

            if (foundUser != null)
            {
                do
                {
                    Console.Write("Password: ");
                    checkPassword = Console.ReadLine();
                    Console.WriteLine();

                    if (foundUser.Password == checkPassword)
                    {
                        Console.WriteLine("Login Successful");
                        Console.WriteLine();
                        return foundUser;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Password. Try again!");
                        Console.WriteLine();
                    }
                } while (foundUser.Password != checkPassword);
            }

            return null;
        }

        public bool VerifyUser(User user)
        {
            int attempts = 3;

            while (attempts > 0)
            {
                Console.Write("Enter your pin: ");
                string pinInput = Console.ReadLine();
                bool isValidPin = int.TryParse(pinInput, out int verifyPin);

                if (!isValidPin)
                {
                    Console.WriteLine("Pin must be numbers only");
                    continue;
                }

                if (verifyPin == user.Pin)
                {
                    return true;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Incorrect Pin. {attempts} attempts remaining.");
                }
            }

            Console.WriteLine("Too many incorrect attempts.");
            return false;
        }
    }
}