using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Account
{
    class User
    {
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Guid userId { get; }
        public string password { get; set; }
        public string pin { get; set; }
        public string userName { get; set; }
        public long accountNumber { get; }

        public void Register()
        {
            Console.WriteLine("Fill in the details below...");
            Console.WriteLine();

            Console.Write("First name: ");
            firstName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Last name: ");
            lastName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Phone number: ");
            phoneNumber = Console.ReadLine();
            Console.WriteLine();

            do
            {
                Console.Write("password: ");
                password = Console.ReadLine();
                Console.WriteLine();

                if (password.Length < 8 || password.Length > 16)
                {
                    Console.WriteLine("Password must be between 8 and 16!");
                }
            } while (password.Length < 8 || password.Length > 16);

            string confirmPassword;
            do
            {
                Console.Write("Confirm password: ");
                confirmPassword = Console.ReadLine();

                if (confirmPassword != password)
                {
                    Console.WriteLine("Passwords do not match!");
                }
            } while (confirmPassword != password);
        }
    }
}
