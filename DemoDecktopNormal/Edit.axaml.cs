using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace DemoDecktopNormal;

public partial class Edit : Window
{
    public Edit()
    {
        InitializeComponent();
        Unit.ItemsSource = ProductList.UnitType;
        Category.ItemsSource = ProductList.Categories;
        FilePath = $"{Environment.CurrentDirectory}\\default.png";
        Image.Source = new Bitmap(FilePath);
    }
    public Edit(int i)
    {
        InitializeComponent();
        Unit.ItemsSource = ProductList.UnitType;
        Category.ItemsSource = ProductList.Categories;
        index = i;
        Product product = ProductList.Products[i];
        FilePath = product.ImagePath;
        Image.Source = new Bitmap(FilePath);
        Name.Text = product.Name;
        Amount.Text = product.Amount.ToString();
        Manufacturer.Text = product.Manufacturer;
        Price.Text = product.Price.ToString();
        Description.Text = product.Description;
        Unit.SelectedIndex = Array.IndexOf(ProductList.UnitType, product.Unit);
        Category.SelectedIndex = Array.IndexOf(ProductList.Categories, product.Category);

    }
    int index = -1;
    string FilePath;

    public void Add(object sender, RoutedEventArgs args)
    {
        bool AllGood = true;
        Product tmp = new Product();
        tmp.Name = Name.Text;
        try
        {
            tmp.Amount = Convert.ToInt32(Amount.Text);
            tmp.Price = Convert.ToDecimal(Price.Text);
        }
        catch
        {
            AllGood = false;
        }
        tmp.ImagePath = FilePath;
        tmp.Unit = ProductList.UnitType[Unit.SelectedIndex];
        tmp.Category = ProductList.Categories[Category.SelectedIndex];
        tmp.Description = Description.Text;
        tmp.Manufacturer = Manufacturer.Text;
        if (index == -1 && AllGood)
        {
            ProductList.AddProduct(tmp);
        }
        else if (AllGood)
        {
            ProductList.RedactProduct(tmp, index);
        }
        if (AllGood)
        {
            new ProductPage().Show();
            this.Close();
        }
    }
    public async void Pict(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string fileToCopy;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var topLevel = await openFileDialog.ShowAsync(this);
        fileToCopy = String.Join("", topLevel);
        if (fileToCopy != "")
        {
            string name = Path.GetFileName(fileToCopy);
            File.Copy(fileToCopy, $"{Environment.CurrentDirectory}\\Assets\\{name}");
            FilePath = $"{Environment.CurrentDirectory}\\Assets\\{name}";
            Image.Source = new Bitmap(FilePath);
        }
        else
        {
            FilePath = $"{Environment.CurrentDirectory}\\default.png";
            Image.Source = new Bitmap(FilePath);
        }
    }
}