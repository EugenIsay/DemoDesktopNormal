using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDecktopNormal
{
    public static class Users
    {
        public static List<User> AllUsers = new List<User>();

        public static int Current { get ; set ; }

        public static void Adduser(string Name, string Role, string Password)
        {
            AllUsers.Add(new User() { Name = Name, Role = Role, Password = Password });
        }
        public static bool Check(string Name, string Password)
        {
            if (AllUsers.Contains(AllUsers.FirstOrDefault(u => u.Name == Name && u.Password == Password)))
            {
                Current = AllUsers.IndexOf(AllUsers.FirstOrDefault(u => u.Name == Name && u.Password == Password));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
