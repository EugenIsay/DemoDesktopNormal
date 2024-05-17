using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;

namespace DemoDecktopNormal;

public partial class Edit : Window
{
    public Edit()
    {
        InitializeComponent();
    }

    public Bitmap Image { get; set; }


    public string[] Categories { get => ProductList.Categories; }
    public string[] UnitType { get => ProductList.UnitType; }
    public void Add(object sender, RoutedEventArgs args)
    {
        ProductList.AddProduct(Name.Text, Convert.ToInt32(Amount.Text.ToString()), Manufacturer.Text, Convert.ToDecimal(Price.Text.ToString()), Description.Text, Unit.SelectedIndex, Category.SelectedIndex);
    }
}