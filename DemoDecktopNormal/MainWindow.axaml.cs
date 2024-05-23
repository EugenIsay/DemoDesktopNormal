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