using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace DemoDecktopNormal;

public partial class ProductPage : Window
{
    public ProductPage()
    {

        InitializeComponent();
        if (ProductList.ShownProducts.Count != 0)
        {
            Man.ItemsSource = ProductList.Manufacturers.ToList();
            Man.SelectedIndex = 0;
            BoxList.ItemsSource = ProductList.ShownProducts.ToList();
        }
    }
    public void Add(object sender, RoutedEventArgs args)
    {
        new Edit().Show();
        this.Close();
    }

    public void Exit(object sender, RoutedEventArgs args)
    {
        Users.Current = 0;
        new MainWindow().Show();
        this.Close();
    }
    public void Descending(object sender, RoutedEventArgs args)
    {
        ProductList.Descending();
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
    }
    public void Ascending(object sender, RoutedEventArgs args)
    {
        ProductList.Ascending();
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
    }

    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        ProductList.ProductFiltration(Man.SelectedIndex);
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
    }
}