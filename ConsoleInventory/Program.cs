using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;

namespace ConsoleInventory
{
    class Program
    {

        private static MenuItems _chooseItem = 0;
        static Storage warehouse = new Storage();
        static Storage basket = new Storage();

        private enum MenuItems
        {
            AddToWarehouse,
            ShowWarehouse,
            ShowBasket,
            ClearBasket
        }

        private static readonly Dictionary<MenuItems, string> _mainMenuItems = new Dictionary<MenuItems, string>()
        {
            {MenuItems.AddToWarehouse, "Add to warehouse"},
            {MenuItems.ShowWarehouse, "Show warehouse"},
            {MenuItems.ShowBasket, "Show basket"},
            {MenuItems.ClearBasket, "Clear basket"}
        };

        private enum MenuId
        {
            MainMenu,
            WarehouseMenu,
            BasketMenu
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ShowMenu();
        }

        static void ShowMenu()
        {
            Console.Clear();

            foreach(MenuItems menuItem in _mainMenuItems.Keys.OrderBy(menuItem =>menuItem))
            {
                if (_chooseItem == menuItem)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"----->\t{_mainMenuItems[menuItem]}");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.WriteLine($"\t{_mainMenuItems[menuItem]}");
                }
            }

            CheckKey();
        }

        private static void CheckKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow,MenuId.MainMenu);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow,MenuId.MainMenu);
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    MenuAction(_chooseItem);
                    break;
            }

            CheckKey();
        }

        private static void MakeChoise(ConsoleKey key, MenuId id)
        {
            if ((int)_chooseItem < _mainMenuItems.Count - 1 && key == ConsoleKey.DownArrow && id == MenuId.MainMenu)
            {
                _chooseItem++;
                ShowMenu();
            }
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow && id == MenuId.MainMenu)
            {
                _chooseItem--;
                ShowMenu();
            }

            else if ((int)_chooseItem < warehouse.GetStorageItems().Count - 1 && key == ConsoleKey.DownArrow && id == MenuId.WarehouseMenu)
            {
                _chooseItem++;
                ShowWarehouseMenu();
            }
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow && id == MenuId.WarehouseMenu)
            {
                _chooseItem--;
                ShowWarehouseMenu();
            }

            else if ((int)_chooseItem < basket.GetStorageItems().Count - 1 && key == ConsoleKey.DownArrow && id == MenuId.BasketMenu)
            {
                _chooseItem++;
                ShowBasketMenu();
            }
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow && id == MenuId.BasketMenu)
            {
                _chooseItem--;
                ShowBasketMenu();
            }
            else if (id == MenuId.BasketMenu)
                ShowBasketMenu();
            else if ( id == MenuId.WarehouseMenu)
                ShowWarehouseMenu();
        }

        private static void MenuAction(MenuItems chooseItem)
        {
            switch (chooseItem)
            {
                case MenuItems.AddToWarehouse:
                    warehouse.AddItem(MakeSomeProduct());
                    break;
                case MenuItems.ShowWarehouse:
                    _chooseItem = 0;
                    ShowWarehouseMenu();
                    break;
                case MenuItems.ShowBasket:
                    _chooseItem = 0;
                    ShowBasketMenu();
                    break;
                case MenuItems.ClearBasket:
                    ClearBasket();
                    break;
            }
            ShowMenu();
        }

        public static void ShowBasketMenu()
        {
            List<Product> basketProducts = basket.GetStorageItems();
            Console.Clear();

            if (basketProducts.Count > 0)
            {
                for (var i = 0; i < basketProducts.Count; i++)
                {
                    var basketProduct = basketProducts[i];
                    if ((int)_chooseItem == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"--->\t{basketProduct}");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine($"\t{basketProduct}");

                    }
                }

                double cost = basketProducts.Sum(product => product._count * product._price);
                Console.Write($"The total cost of everything in the basket: {cost}\n" +
                              $"Press delete to remove item from basket\n" +
                              $"Press escape to return in main menu");
                CheckBasketKey();
            }
            else
            {
                Console.WriteLine("Empty basket.\nPress any key to return");
                Console.ReadKey();
            }

        }

        private static void CheckBasketKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow, MenuId.BasketMenu);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow,MenuId.BasketMenu);
                    break;
                case ConsoleKey.Escape:
                    _chooseItem = 0;
                    break;
                case ConsoleKey.Delete:
                    RemoveFromBasket((int)_chooseItem);
                    ShowBasketMenu();
                    break;
                default:
                    CheckBasketKey();
                    break;
            }
        }

        public static void ClearBasket()
        {
            List<Product> basketProducts = basket.GetStorageItems();

            if (basketProducts.Count > 1)
            {
                foreach (var product in basketProducts)
                {
                    warehouse.AddItem(product);
                }

                Console.WriteLine("Basket is clear.\n" +
                                  "All products return in inventory\n" +
                                  "Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Basket is already clear. Press any key to continue");
                Console.ReadKey();
            }

            basket.Clear();
        }

        private static void RemoveFromBasket(int itemIndex)
        {
            List<Product> _basketProducts = basket.GetStorageItems();

            Console.Clear();
            Console.WriteLine(_basketProducts[itemIndex]);
            Console.WriteLine("Write count to remove");

            int tempCount = GetCount();

            Product tempProduct = new Product(_basketProducts[itemIndex]._name,
                tempCount > _basketProducts[itemIndex]._count ? _basketProducts[itemIndex]._count : tempCount,
                _basketProducts[itemIndex]._price,
                _basketProducts[itemIndex]._id);

            warehouse.AddItem(tempProduct);
            basket.RemoveItemCount(itemIndex, tempCount);

            Console.WriteLine("Item return to inventory. Press any key to continue");
            Console.ReadKey();
        }

        private static void ShowWarehouseMenu()
        {
            Console.Clear();
            List<Product> warehouseProducts = warehouse.GetStorageItems();

                if (warehouseProducts.Count > 0)
                {
                    for (var i = 0; i < warehouseProducts.Count; i++)
                    {
                        var warehouseProduct = warehouseProducts[i];
                        if ((int)_chooseItem == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"--->\t{warehouseProduct}");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.WriteLine($"\t{warehouseProduct}");

                        }
                    }
                    Console.WriteLine("Press up and down arrow to navigate.\n" +
                                      "Press delete to delete item\n" +
                                      "Press 1 to add item count\n" +
                                      "Press 2 to add item in basket\n" +
                                      "Press 3 to change item price\n" +
                                      "Press escape to return in main menu");
                    CheckWarehouseKey();
                }
                else
                {
                    Console.WriteLine("Empty warehouse\nPress any key to return");
                    Console.ReadKey();
                }
        }

        private static void CheckWarehouseKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow,MenuId.WarehouseMenu);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow, MenuId.WarehouseMenu);
                    break;
                case ConsoleKey.Escape:
                    _chooseItem = 0;
                    break;
                case ConsoleKey.Delete:
                    Console.Clear();
                    Console.WriteLine("Write count number to remove");
                    warehouse.RemoveItemCount((int)_chooseItem,GetCount());
                    ShowWarehouseMenu();
                    break;
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Write count number to add");
                    warehouse.AddItemCount((int)_chooseItem,GetCount());
                    ShowWarehouseMenu();
                    break;
                case ConsoleKey.D2:
                    AddItemToBasket((int)_chooseItem);
                    ShowWarehouseMenu();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("Write new price");
                    warehouse.ChangeItemPrice((int)_chooseItem,GetPrice());
                    ShowWarehouseMenu();
                    break;
                default:
                    CheckWarehouseKey();
                    break;
            }
        }

        private static void AddItemToBasket(int itemIndex)
        {
            List<Product> warehouseProducts = warehouse.GetStorageItems();

            Console.Clear();
            Console.WriteLine(warehouseProducts[itemIndex]);
            Console.WriteLine("Write count to add");

            int tempCount = GetCount();
           
            Product tempProduct = new Product(warehouseProducts[itemIndex]._name,
                tempCount > warehouseProducts[itemIndex]._count ? warehouseProducts[itemIndex]._count : tempCount,
                warehouseProducts[itemIndex]._price,
                warehouseProducts[itemIndex]._id);

            basket.AddItem(tempProduct);
            warehouse.RemoveItemCount(itemIndex, tempCount);

            Console.WriteLine("Complete.\n Item in basket. Press any key to continue");
            Console.ReadKey();
        }

        private static double GetPrice()
        {
            double tempPrice;

            while (!Double.TryParse(Console.ReadLine(), out tempPrice))
            {
                Console.WriteLine("Write correct price");
            }

            return tempPrice;
        }

        private static int GetCount()
        {
            int tempPrice;

            while (!Int32.TryParse(Console.ReadLine(), out tempPrice))
            {
                Console.WriteLine("Write correct count");
            }

            return tempPrice;
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
    }
}
