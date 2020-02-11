using System;
using System.Collections.Generic;

namespace Inventory
{ 
    public class Warehouse
    {
        private static int _warehouseCursorPosition = 0;
        private static List<Product> _warehouseProducts = new List<Product>();

        public static void AddItem(Product item)
        {

            if(IsProductExists(item, out int i))
            {
                _warehouseProducts[i] += item;
            }
            else
                _warehouseProducts.Add(item);
        }

        private static bool IsProductExists(Product item, out int i)
        {

            for (i = 0; i < _warehouseProducts.Count; i++)
            {
                if (item == _warehouseProducts[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static void ShowWarehouse()
        {
            Console.Clear();
            if (_warehouseProducts.Count > 0)
            {
                for (var i = 0; i < _warehouseProducts.Count; i++)
                {
                    var warehouseProduct = _warehouseProducts[i];
                    if (_warehouseCursorPosition == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"--->\t{warehouseProduct.ToString()}");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine($"\t{warehouseProduct.ToString()}");

                    }
                }
                Console.WriteLine("Press up and down arrow to navigate.\n" +
                                  "Press delete to delete item\n" +
                                  "Press 1 to add item count\n" +
                                  "Press 2 to add item in basket\n" +
                                  "Press 3 to change item price\n" +
                                  "Press escape to return in main menu");
                CheckKey();
            }
            else
            {
                Console.WriteLine("Empty warehouse\nPress any key to return");
                Console.ReadKey();
            }

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
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Delete:
                    RemoveItemCount(_warehouseCursorPosition);
                    ShowWarehouse();
                    break;
                case ConsoleKey.D1:
                    AddItemCount(_warehouseCursorPosition);
                    ShowWarehouse();
                    break;
                case ConsoleKey.D2:
                    AddItemToBasket(_warehouseCursorPosition);
                    ShowWarehouse();
                    break;
                case ConsoleKey.D3:
                    ChangeItemPrice(_warehouseCursorPosition);
                    ShowWarehouse();
                    break;
                default:
                    CheckKey();
                    break;
            }
        }

        private static void ChangeItemPrice(int itemIndex)
        {
            int tempPrice;
            Console.Clear();
            Console.WriteLine(_warehouseProducts[itemIndex].ToString());
            Console.WriteLine("Write new price");

            while (!Int32.TryParse(Console.ReadLine(), out tempPrice))
            {
                Console.WriteLine("Write correct price");
            }

            _warehouseProducts[itemIndex]._price = tempPrice;
        }

        private static void AddItemToBasket(int itemIndex)
        {
            int tempCount;
            Console.Clear();
            Console.WriteLine(_warehouseProducts[itemIndex].ToString());
            Console.WriteLine("Write count to add");

            while (!Int32.TryParse(Console.ReadLine(), out tempCount))
            {
                Console.WriteLine("Write correct count");
            }

            Product tempProduct = new Product(_warehouseProducts[itemIndex]._name,
                tempCount>_warehouseProducts[itemIndex]._count?_warehouseProducts[itemIndex]._count:tempCount,
                _warehouseProducts[itemIndex]._price,
                _warehouseProducts[itemIndex]._id);

            Basket.AddItem(tempProduct);
            RemoveItemCount(itemIndex,tempCount);

            Console.WriteLine("Complete.\n Item in basket. Press any key to continue");
            Console.ReadKey();
        }

        private static void RemoveItemCount(int itemIndex)
        {
            int tempCount;
            Console.Clear();
            Console.WriteLine(_warehouseProducts[itemIndex].ToString());
            Console.WriteLine("Write count to remove");

            while (!Int32.TryParse(Console.ReadLine(), out tempCount))
            {
                Console.WriteLine("Write correct count");
            }

            RemoveItemCount(itemIndex,tempCount);
        }

        private static void RemoveItemCount(int itemIndex, int count)
        {

            if (count >= _warehouseProducts[itemIndex]._count)
                _warehouseProducts.RemoveAt(itemIndex);
            else
            {
                _warehouseProducts[itemIndex]._count -= count;
            }
        }

        private static void AddItemCount(int itemIndex)
        {
            int tempCount;
            Console.Clear();
            Console.WriteLine(_warehouseProducts[itemIndex].ToString());
            Console.WriteLine("Write count to add");

            while (!Int32.TryParse(Console.ReadLine(), out tempCount))
            {
                Console.WriteLine("Write correct count");
            }

            _warehouseProducts[itemIndex]._count += tempCount;
        }

        private static void MakeChoise(ConsoleKey key)
        {
            if (_warehouseCursorPosition < _warehouseProducts.Count-1 && key == ConsoleKey.DownArrow)
            {
                _warehouseCursorPosition++;
                ShowWarehouse();
            }

            else if (_warehouseCursorPosition > 0 && key == ConsoleKey.UpArrow)
            {
                _warehouseCursorPosition--;
                ShowWarehouse();
            }
            CheckKey();
        }
    }
}
