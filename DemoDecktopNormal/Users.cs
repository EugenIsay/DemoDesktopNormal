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
        public static List<BuyProd> BuyList = new List<BuyProd>();

        public static int Current { get; set; }

        public static void Adduser(string Name, string Role, string Password)
        {
            AllUsers.Add(new User() { Name = Name, Role = Role, Password = Password });
        }

        public static void DelItem(int i)
        {
            BuyList.RemoveAt(i);
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
        public static bool IsAvalible
        {
            get { return AllUsers[Current].Role == "admin"; }

        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
    public class BuyProd
    {
        public int User { get; set; }
        public Product BuyProduct { get; set; }

        public int FindMyId
        {
            get => Users.BuyList.IndexOf(Users.BuyList.FirstOrDefault(po =>
                po.User == User && po.BuyProduct == BuyProduct));
        }
        public void Delete()
        {
            Users.DelItem(FindMyId);
        }
    }
}
