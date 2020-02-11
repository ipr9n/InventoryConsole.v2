using System;
using Inventory;

namespace ConsoleInventory
{
    class Program
    {
        private static int _chooseItem = 0;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ShowMenu();
        }

        static void ShowMenu()
        {
            Console.Clear();

            for (int i = 0; i < _mainMenuItems.Length; i++)
            {
                if (_chooseItem == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"----->\t{_mainMenuItems[i]}");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.WriteLine($"\t{_mainMenuItems[i]}");
                }
            }

            CheckKey();
        }

        private static void CheckKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow);
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    MenuAction(_chooseItem);
                    break;
            }

            CheckKey();
        }

        private static void MakeChoise(ConsoleKey key)
        {
            if (_chooseItem < _mainMenuItems.Length - 1 && key == ConsoleKey.DownArrow)
            {
                _chooseItem++;
                ShowMenu();
            }
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow)
            {
                _chooseItem--;
                ShowMenu();
            }
        }

        private static void MenuAction(int chooseItem)
        {
            switch ((MenuItems) chooseItem)
            {
                case MenuItems.AddToWarehouse:
                    Warehouse.AddItem(MakeSomeProduct());
                    break;
                case MenuItems.ShowWarehouse:
                    Warehouse.ShowWarehouse();
                    break;
                case MenuItems.ShowBasket:
                    Basket.ShowBasket();
                    break;
                case MenuItems.ClearBasket:
                    Basket.ClearBasket();
                    break;
            }

            ShowMenu();
        }

        private static Product MakeSomeProduct()
        {
            Console.WriteLine("Write product name");
            string productName = Console.ReadLine();

            Console.WriteLine("Write count of product");
            int productCount;

            while (!int.TryParse(Console.ReadLine(), out productCount))
            {
                Console.WriteLine("Write correct count");
            }

            Console.WriteLine("Write product price");
            double productPrice;

            while (!double.TryParse(Console.ReadLine(), out productPrice))
            {
                Console.WriteLine("Write correct price");
            }

            Product product = new Product(productName, productCount, productPrice);

            return product;
        }

        private enum MenuItems
        {
            AddToWarehouse,
            ShowWarehouse,
            ShowBasket,
            ClearBasket
        }

        private static readonly string[] _mainMenuItems = new string[]
            {"Add to inventory", "Show Inventory", "Show Basket", "Clear Basket"};

        public static readonly string[] _productMenuItems = new string[]
        {
            $"Delete some count of products",
            "Delete this product full",
            "Add some count product",
            "Change price",
            "Add to basket"
        };
    }
}
