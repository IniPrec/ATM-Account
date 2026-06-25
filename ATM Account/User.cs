using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ATM_Account
{
    class User
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; private set; }
        public string Password { get; set; }
        public int Pin { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }

        public void CreateAccount()
        {
            Console.WriteLine("Fill in the details below...");
            Console.WriteLine();

            Console.Write("First name: ");
            FirstName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Last name: ");
            LastName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Phone number: ");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine();

            do
            {
                Console.Write("password: ");
                Password = Console.ReadLine();
                Console.WriteLine();

                if (Password.Length < 8 || Password.Length > 16)
                {
                    Console.WriteLine("Password must be between 8 and 16!");
                }
            } while (Password.Length < 8 || Password.Length > 16);

            string confirmPassword;
            do
            {
                Console.Write("Confirm password: ");
                confirmPassword = Console.ReadLine();

                if (confirmPassword != Password)
                {
                    Console.WriteLine("Passwords do not match!");
                }
            } while (confirmPassword != Password);

            do
            {
                Console.Write("Enter a unique username: ");
                UserName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Console.WriteLine("Username cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(UserName));

            bool isValidPin;
            do
            {
                Console.Write("Create your transaction PIN (must be 4 digits): ");
                string pinInput = Console.ReadLine();
                isValidPin = int.TryParse(pinInput, out int parsedPin);

                if (!isValidPin)
                {
                    Console.WriteLine("PIN must be numbers only.");
                    continue;
                }

                Pin = parsedPin;

                if (Pin.ToString().Length != 4)
                {
                    Console.WriteLine("PIN must be 4 digits.");
                }
            } while (Pin.ToString().Length != 4 || !isValidPin);

            int confirmPin;
            bool isPinMatch;
            do
            {
                Console.Write("Confirm your transaction PIN: ");
                string confirmPinInput = Console.ReadLine();
                isPinMatch = int.TryParse(confirmPinInput, out confirmPin);
                if (!isPinMatch)
                {
                    Console.WriteLine("PIN must be numbers only.");
                    continue;
                }
                if (confirmPin != Pin)
                {
                    Console.WriteLine("PINs do not match!");
                }
            } while (confirmPin != Pin || !isPinMatch);
        }

        // Constructor to initialize the UserId and CreatedAt properties
        public User()
        {
            UserId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
