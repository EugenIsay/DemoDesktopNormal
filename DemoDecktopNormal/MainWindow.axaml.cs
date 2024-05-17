using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Tmds.DBus.Protocol;

namespace DemoDecktopNormal
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Users.Adduser("user", "user", "user");
            Users.Adduser("manager", "manager", "manager");
            Users.Adduser("admin", "admin", "admin");
            ProductList.AddProduct("a", 2, "av", 12, "abc", 2, 2);
        }
        public void Reg(object sender, RoutedEventArgs args)
        {
            if (Users.Check(Login.Text, Password.Text))
            {
                new ProductPage().Show();
                this.Close();
            }
        }

    }
}