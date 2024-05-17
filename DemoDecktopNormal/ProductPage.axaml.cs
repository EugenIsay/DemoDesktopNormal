using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace DemoDecktopNormal;

public partial class ProductPage : Window
{
    public ProductPage()
    {
        InitializeComponent();
    }
    public List<Product> Products { get => ProductList.ShownProducts; }
    public void Add(object sender, RoutedEventArgs args)
    {
        new Edit().ShowDialog(this);
    }

    public void Exit(object sender, RoutedEventArgs args)
    {
        Users.Current = 0;
        new MainWindow().Show();
        
        this.Close();
    }
}