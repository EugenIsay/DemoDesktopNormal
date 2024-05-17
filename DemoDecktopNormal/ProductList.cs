using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoDecktopNormal
{
    static public class ProductList
    {
        public static List<Product> Products = new List<Product>();
        public static List<Product> ShownProducts = new List<Product>();
        public static List<string> Manufacturers = new List<string>() { "Все производители" };
        public static string[] Categories = { "Овощи", "Фрукты", "Мясо", "Бытовая техника", "Инструменты", "Другое" };
        public static string[] UnitType = { "Штук", "Грамм", "Килограмм", "Литров" };

        public static void Fill (List<Product> List)
        {
            ShownProducts.Clear();
            foreach (var item in List) 
            {
                ShownProducts.Add (item);
            }
        }
        public static void AddProduct(string Name, int Amount, string Manufacturer, decimal Price, string Description, int Unit, int Category)
        {
            Products.Add(new Product() { Name = Name, Amount = Amount, Manufacturer = Manufacturer, Price = Price, Description = Description, Unit = UnitType[Unit], Category = Categories[Category] });
            string tmp = Manufacturers.FirstOrDefault(Manufacturer);
            if (!Manufacturers.Contains(Manufacturer))
            {
                Manufacturers.Add(Manufacturer);
            }
            Fill(Products);
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }

        public string Manufacturer { get; set; }

        private decimal _price;
        public decimal Price { get { return Math.Round(_price, 2); } set { _price = Math.Round(value, 2); } }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
        //int FindMyInd
        //{
        //    get { return ProductsList.GetProducts.IndexOf(ProductsList.GetProducts.FirstOrDefault(po => po.Name == Name && po.Price == Price && po.Manufacturer == Manufacturer && po.Amount == Amount && po.Category == Category && po.Unit == Unit)); }
        //}
        //public async void Redact()
        //{
        //    var dialog = new EditView();
        //    dialog.DataContext = new EditingViewModel(FindMyInd);
        //    await dialog.ShowDialog((Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow).ConfigureAwait(true);

        //}
        //public void Delete()
        //{
        //    ProductsList.RemoveProduct(FindMyInd);
        //}
    }
}
