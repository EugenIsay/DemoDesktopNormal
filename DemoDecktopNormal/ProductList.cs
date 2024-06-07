using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Avalonia.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoDecktopNormal
{
    static public class ProductList
    {
        public static List<Product> Products = new List<Product>();
        public static List<Product> ShownProducts = new List<Product>();
        public static List<string> Manufacturers = new List<string>() { "Все производители" };
        public static string[] Categories = { "Овощи", "Фрукты", "Мясо", "Бытовая техника", "Инструменты", "Другое" };
        public static string[] UnitType = { "Штук", "Грамм", "Килограмм", "Литров" };
        private static int _filterID = 0;        
        private static int _sortID = 0;
        private static string _searchString = "";
        public static string Nums
        {
            get => $"{ShownProducts.Count()} из {Products.Count()}";
        }
        
        public static void Fill(List<Product> List)
        {
            ShownProducts.Clear();
            foreach (var item in List)
            {
                ShownProducts.Add(item);
            }
        }

        public static void AddProduct(Product product)
        {
            if (!Manufacturers.Contains(product.Manufacturer))
            {
                Manufacturers.Add(product.Manufacturer);
            }
            Products.Add(product);
            Fill(Products);
        }

        public static void RedactProduct(Product product, int i)
        {
            if (!Manufacturers.Contains(product.Manufacturer))
            {
                Manufacturers.Add(product.Manufacturer);
            }

            string tmp = Products[i].Manufacturer;
            if (File.Exists(Products[i].ImagePath) &&
                Products[i].ImagePath != $"{Environment.CurrentDirectory}\\default.png" &&
                Products[i].ImagePath != product.ImagePath)
            {
                File.Delete(Products[i].ImagePath);
            }

            Products[i] = product;
            if (Products.Where(p => p.Manufacturer == tmp).Count() == 0)
            {
                Manufacturers.Remove(tmp);
            }

            Fill(Products);
        }

        public static void Sort(int id)
        {
            _sortID = id;
            Fill(Sort(MainSearch(MainProductFiltration(Products))));
        }
        public static List<Product> Sort(List<Product> MyList)
        {
            switch (_sortID)
            {
                case 1:
                    MyList = MyList.OrderByDescending(p => p.Price).ToList();
                    break;
                case 2: 
                    MyList = MyList.OrderBy(p => p.Price).ToList();
                    break;
            }

            return MyList;
        }

        
        public static void ProductFiltration(int i)
        {
            _filterID = i;
            Fill(MainProductFiltration(MainSearch(Sort(Products))));
            
        }

        public static void RemoveProduct(int i)
        {
            string tmp = Products[i].Manufacturer;
            if (File.Exists(Products[i].ImagePath) &&
                Products[i].ImagePath != $"{Environment.CurrentDirectory}\\default.png")
            {
                File.Delete(Products[i].ImagePath);
            }

            Products.RemoveAt(i);
            if (Products.Where(p => p.Manufacturer == tmp).Count() == 0)
            {
                Manufacturers.Remove(tmp);
            }

            Fill(Products);
        }

        public static void Search(string MyString)
        {
            _searchString = MyString;
            Fill(MainSearch(MainProductFiltration(Sort(Products))));
        }

        public static List<Product> MainSearch(List<Product> MyList)
        {
            if (_searchString != string.Empty)
            {
                Char[] str = _searchString.ToCharArray();
                List<char> Find = new List<char>();
                List<Product> tmp = MyList.Where(p => p == p).ToList();
                List<string> strings = new List<string>();

                for (int cur = 0; cur < str.Length + 1; cur++)
                {
                    if (cur != str.Length && str[cur] != ' ')
                    {
                        Find.Add(str[cur]);
                    }
                    else if (Find.Count() != 0)
                    {
                        string f = new string(Find.ToArray());
                        f = f.ToLower();
                        strings.Add(f);
                        Find.Clear();

                    }
                }
                foreach (string f in strings) 
                {
                    tmp = tmp.Where(
                        p => p.Name.ToLower().Contains(f) || p.Description.ToLower().Contains(f)
                                                        || p.Category.ToLower().Contains(f)
                                                        || p.Price.ToString().ToLower().Contains(f)
                                                        || p.Amount.ToString().ToLower().Contains(f)
                                                        || p.Unit.ToLower().Contains(f)
                                                        || p.Manufacturer.ToLower().Contains(f)).ToList();
                }
                return tmp;
            }
            else
            {
                return MyList;
            }
        }
        public static List<Product> MainProductFiltration(List<Product> MyList)
        {
            if (_filterID > 0)
            {
                List<Product> temp = MyList.Where(p => p.Manufacturer == Manufacturers[_filterID]).ToList();
                return temp;
            }
            else
            {
                return MyList;
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

        public decimal Price
        {
            get { return Math.Round(_price, 2); }
            set { _price = Math.Round(value, 2); }
        }

        public string Description { get; set; }
        public string ImagePath { get; set; }

        public Bitmap Image
        {
            get => new Bitmap(ImagePath);
        }

        public bool IsAvailble
        {
            get
            {
                if (Amount <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string Color
        {
            get
            {
                if (IsAvailble)
                {
                    return "White";
                }
                else
                {
                    return "Gray";
                }
            }
        }

        public int FindMyInd
        {
            get
            {
                return ProductList.Products.IndexOf(ProductList.Products.FirstOrDefault(po =>
                    po.Name == Name && po.Price == Price && po.Manufacturer == Manufacturer && po.Amount == Amount &&
                    po.Category == Category && po.Unit == Unit));
            }
        }

        public void Redact()
        {
            if (Users.BuyList.Where(p => p.BuyProduct == ProductList.Products[FindMyInd]).Count() == 0)
                new Edit(FindMyInd).Show();
        }

        public void Delete()
        {
            if (Users.BuyList.Where(p => p.BuyProduct == ProductList.Products[FindMyInd]).Count() == 0)
                ProductList.RemoveProduct(FindMyInd);
        }
    }
}