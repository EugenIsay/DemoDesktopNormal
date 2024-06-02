using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;

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
    public async void SomthingsWrong()
    {
        Wrong.Text = "Что-то пошло не так";
        await Task.Delay(1000);
        Wrong.Text = "";
    }
    int index = -1;
    string FilePath;
    static int im = 0;

    public void Add(object sender, RoutedEventArgs args)
    {
        bool AllGood = true;
        Product tmp = new Product();
        try
        {
            tmp.Name = Name.Text;
            tmp.Amount = Convert.ToInt32(Amount.Text);
            tmp.Price = Convert.ToDecimal(Price.Text);
            tmp.ImagePath = FilePath;
            tmp.Unit = ProductList.UnitType[Unit.SelectedIndex];
            tmp.Category = ProductList.Categories[Category.SelectedIndex];
            tmp.Description = Description.Text;
            tmp.Manufacturer = Manufacturer.Text;
        }
        catch
        {
            AllGood = false;
            SomthingsWrong();
        }

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
            string name = "picture" + im.ToString();
            im++;
            File.Copy(fileToCopy, $"{Environment.CurrentDirectory}\\Assets\\{name}");
            FilePath = $"{Environment.CurrentDirectory}\\Assets\\{name}";
            Image.Source = new Bitmap(FilePath);
        }
        else
        {
            FilePath = $"{Environment.CurrentDirectory}\\default.png";
            Bitmap btm = new Bitmap(FilePath);
            Bitmap.DecodeToWidth(btm.LoadCoverBitmapAsync(), 400);
            Image.Source = new Bitmap(FilePath);
        }
    }
}