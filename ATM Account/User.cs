using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ATM_Account
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; private set; }
        public string Password { get; set; }
        public int Pin { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; private set; }

        public void CreateAccount()
        {
            Console.WriteLine("Fill in the details below:");
            Console.WriteLine();

            Console.Write("First Name: ");
            FirstName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Last Name: ");
            LastName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Phone Number: ");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine();

            do
            {
                Console.Write("Password: ");
                Password = Console.ReadLine();
                Console.WriteLine();

                if (Password.Length < 8 || Password.Length > 16)
                {
                    Console.WriteLine("Password must be between 8 and 16");
                }
            } while (Password.Length < 8 || Password.Length > 16);

            string confirmPassword;
            do
            {
                Console.Write("Confirm Password: ");
                confirmPassword = Console.ReadLine();
                Console.WriteLine();

                if (confirmPassword != Password)
                {
                    Console.WriteLine("Password must match");
                }
            } while (confirmPassword != Password);

            // To-Do: make a condition to check if username is unique when file is created.
            UserServices userServices = new UserServices();

            do
            {
                Console.Write("Enter a unique username: ");
                UserName = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Console.WriteLine("Username cannot be empty.");
                }
                else if (userServices.UserNameExists(UserName))
                {
                    Console.WriteLine("Username exists. Use a different username");
                }
            } while (string.IsNullOrWhiteSpace(UserName) || userServices.UserNameExists(UserName));

            bool isValidPin = false;
            do
            {
                Console.Write("Create your transaction PIN (must be 4 digits): ");
                string pinInput = Console.ReadLine();
                isValidPin = int.TryParse(pinInput, out int parsedPin);
                Pin = parsedPin;

                if (!isValidPin)
                {
                    Console.WriteLine("PIN must be numbers only.");
                    Console.WriteLine();
                }

                if (Pin.ToString().Length != 4)
                {
                    Console.WriteLine("PIN must 4 digits!");
                    Console.WriteLine();
                }
            } while (Pin.ToString().Length != 4 || !isValidPin);

            int confirmPin;
            bool isValidConfirmPin;
            do
            {
                Console.Write("Confirm PIN: ");
                string confirmPininput = (Console.ReadLine());
                Console.WriteLine();

                isValidConfirmPin = int.TryParse(confirmPininput, out int parsedConfirmPin);
                confirmPin = parsedConfirmPin;

                if (!isValidConfirmPin)
                {
                    Console.WriteLine("PIN must be numbers only");
                    Console.WriteLine();
                    continue;
                }

                if (confirmPin != Pin)
                {
                    Console.WriteLine("PIN must match.");
                    Console.WriteLine();
                }
            } while (confirmPin != Pin || !isValidConfirmPin);
        }

        // Constructor
        public User()
        {
            UserId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public User(Guid userId, DateTime createdAt)
        {
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}