using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Input;

namespace DemoDecktopNormal;

public partial class ProductPage : Window
{
    public string[] Srt = new string[] { "Без сортировки", "По уменьшению", "По увеличению" };

    public ProductPage()
    {
        InitializeComponent();
        if (ProductList.ShownProducts.Count != 0)
        {
            Man.ItemsSource = ProductList.Manufacturers.ToList();
            Man.SelectedIndex = 0;
            Sort.ItemsSource = Srt;
            Sort.SelectedIndex = 0;
            BoxList.ItemsSource = ProductList.ShownProducts.ToList();
            Nums.Text = ProductList.Nums;
        }

        UserName.Text = Users.AllUsers[Users.Current].Name;
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

    public async void Redact(object sender, RoutedEventArgs args)
    {
        await Task.Delay(100);
        this.Close();
    }

    public async void Redact()
    {
        await Task.Delay(100);
        this.Close();
    }

    public async void Delete(object sender, RoutedEventArgs args)
    {
        await Task.Delay(300);
        Man.ItemsSource = ProductList.Manufacturers.ToList();
        Man.SelectedIndex = 0;
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
        Nums.Text = ProductList.Nums;
    }

    private void SelectionBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        ProductList.Sort(Sort.SelectedIndex);
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
    }

    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        ProductList.ProductFiltration(Man.SelectedIndex);
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
        Nums.Text = ProductList.Nums;
    }

    private void TextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        ProductList.Search(Find.Text);
        BoxList.ItemsSource = ProductList.ShownProducts.ToList();
        Nums.Text = ProductList.Nums;
    }

    private void Buy(object? sender, RoutedEventArgs e)
    {
        new Buy().Show();
        Close();
    }

    private void BoxList_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        int i = BoxList.SelectedIndex;
        if (i != -1)
        {
            if (Users.IsAvalible)
            {
                ProductList.ShownProducts[i].Redact();
                Redact();
            }
            else
            {
                BuyProd tmp = new BuyProd
                    { BuyProduct = ProductList.Products[ProductList.ShownProducts[i].FindMyInd], User = Users.Current };
                if (Users.BuyList.Where(p => p.BuyProduct == tmp.BuyProduct).Count() == 0)
                {
                    Users.BuyList.Add(tmp);
                }
            }
        }
    }
}