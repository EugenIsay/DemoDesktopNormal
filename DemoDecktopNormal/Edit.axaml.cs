using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Reflection;

namespace DemoDecktopNormal;

public partial class Edit : Window
{
    public Edit()
    {
        InitializeComponent();
        Unit.ItemsSource = ProductList.UnitType;
        Category.ItemsSource = ProductList.Categories;
    }
    public Edit(int i)
    {
        InitializeComponent();
        Unit.ItemsSource = ProductList.UnitType;
        Category.ItemsSource = ProductList.Categories;
        index = i;
        Product product = ProductList.Products[i];
        Name.Text = product.Name;
        Amount.Text = product.Amount.ToString();
        Manufacturer.Text = product.Manufacturer;
        Price.Text = product.Price.ToString();
        Description.Text = product.Description;
        Unit.SelectedIndex = Array.IndexOf(ProductList.UnitType, product.Unit);
        Category.SelectedIndex = Array.IndexOf(ProductList.Categories, product.Category);

    }
    int index;
    public void Add(object sender, RoutedEventArgs args)
    {
        if (index == null)
        {
            ProductList.AddProduct(Name.Text, Convert.ToInt32(Amount.Text.ToString()), Manufacturer.Text, Convert.ToDecimal(Price.Text.ToString()), Description.Text, Unit.SelectedIndex, Category.SelectedIndex);
        }
        else
        {

        }
        new ProductPage().Show();
        this.Close();
    }
}