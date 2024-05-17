using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tmds.DBus.SourceGenerator;
using System.Reflection;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia;

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
        public static void Descending()
        {
            List<Product> temp = ShownProducts.OrderByDescending(p => p.Price).ToList();
            Fill(temp);
        }
        public static void Ascending()
        {
            List<Product> temp = ShownProducts.OrderBy(p => p.Price).ToList();
            Fill(temp);
        }
        public static void ProductFiltration(int i)
        {
            if ( i != 0)
            {
                List<Product> temp = Products.Where(p => p.Manufacturer == Manufacturers[i]).ToList();
                Fill(temp);
            }
            else
            {
                Fill(Products);
            }

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
        int FindMyInd
        {
            get { return ProductList.Products.IndexOf(ProductList.Products.FirstOrDefault(po => po.Name == Name && po.Price == Price && po.Manufacturer == Manufacturer && po.Amount == Amount && po.Category == Category && po.Unit == Unit)); }
        }
        public async void Redact()
        {
            (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
            new Edit(FindMyInd).Show();

        }
        //public void Delete()
        //{
        //    ProductsList.RemoveProduct(FindMyInd);
        //}
    }
}
