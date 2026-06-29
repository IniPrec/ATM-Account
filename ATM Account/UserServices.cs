using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Account
{
    class UserServices
    {
        private static readonly string filePath = "C:\\Users\\preciousoyebanjo\\source\\repos\\ATM Account\\ATM Account\\users.txt";

        public void SaveUser(User user)
        {
            string userData = $"{user.UserName},{user.UserId},{user.FirstName},{user.LastName},{user.Email},{user.PhoneNumber},{user.Password},{user.Pin},{user.CreatedAt}";
            File.AppendAllText(filePath, userData + Environment.NewLine);
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            if (!File.Exists(filePath))
            {
                return users;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] part = line.Split(',');
                User user = new User(Guid.Parse(part[1]), DateTime.Parse(part[8]));
                user.UserName = part[0];
                user.FirstName = part[2];
                user.LastName = part[3];
                user.Email = part[4];
                user.PhoneNumber = part[5];
                user.Password = part[6];
                user.Pin = int.Parse(part[7]);
                users.Add(user);
            }

            return users;
        }

        public bool UserNameExists(string userName)
        {
            List<User> users = GetAllUsers();

            foreach (User user in users)
            {
                if (user.UserName == userName)
                {
                    return true;
                }
            }

            return false;
        }

        public User GetUserByName(string userName)
        {
            List<User> users = GetAllUsers();

            foreach (User user in users)
            {
                if (user.UserName == userName)
                {
                    return user;
                }
            }

            return null; // user not found!
        }
    }
}