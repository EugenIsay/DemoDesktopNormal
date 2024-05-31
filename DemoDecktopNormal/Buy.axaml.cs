using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DemoDecktopNormal;

public partial class Buy : Window
{
    public Buy()
    {
        InitializeComponent();
        if (Users.BuyList.Count > 0)
        {
            BoxList.ItemsSource = Users.BuyList.Where(p => p.User == Users.Current);
        }
        UserName.Text = Users.AllUsers[Users.Current].Name;
    }
    private async void Delete(object? sender, RoutedEventArgs e)
    {
        await Task.Delay(300);
        BoxList.ItemsSource = Users.BuyList.Where(p => p.User == Users.Current);
    }
    private void Exit(object? sender, RoutedEventArgs e)
    {
        new ProductPage().Show();
        this.Close();
    }
}