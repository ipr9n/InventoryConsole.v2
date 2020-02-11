using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory
{
    public class Basket
    {
        private static int _basketCursorPosition = 0;
        private static List<Product> _basketProducts = new List<Product>();

        public static void AddItem(Product item)
        {

            if (IsProductExists(item, out int i))
            {
                _basketProducts[i] += item;
            }
            else
                _basketProducts.Add(item);
        }

        private static bool IsProductExists(Product item, out int i)
        {

            for (i = 0; i < _basketProducts.Count; i++)
            {
                if (item == _basketProducts[i])
                {
                    return true;
                }
            }
            return false;
        }

        private static double GetCost() => _basketProducts.Select(product => product._count * product._price).Sum();

        public static void ShowBasket()
        {
            Console.Clear();
            if (_basketProducts.Count > 0)
            { 
                for (var i = 0; i < _basketProducts.Count; i++)
                {
                    var basketProduct = _basketProducts[i];
                    if (_basketCursorPosition == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"--->\t{basketProduct.ToString()}");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine($"\t{basketProduct.ToString()}");

                    }
                }
                Console.Write($"The total cost of everything in the basket: {GetCost()}\n" +
                              $"Press delete to remove item from basket\n" +
                              $"Press escape to return in main menu");
                CheckKey();
            }
            else
            {
                Console.WriteLine("Empty basket.\nPress any key to return");
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
                    RemoveFromBasket(_basketCursorPosition);
                    ShowBasket();
                    break;
                default:
                    CheckKey();
                    break;
            }
        }

        private static void RemoveItemCount(int itemIndex)
        {
            int tempCount;
            Console.Clear();
            Console.WriteLine(_basketProducts[itemIndex].ToString());
            Console.WriteLine("Write count to remove");

            while (!Int32.TryParse(Console.ReadLine(), out tempCount))
            {
                Console.WriteLine("Write correct count");
            }

            RemoveItemCount(itemIndex, tempCount);
        }

        private static void RemoveFromBasket(int itemIndex)
        {
            int tempCount;
            Console.Clear();
            Console.WriteLine(_basketProducts[itemIndex].ToString());
            Console.WriteLine("Write count to remove");

            while (!Int32.TryParse(Console.ReadLine(), out tempCount))
            {
                Console.WriteLine("Write correct count");
            }

            Product tempProduct = new Product(_basketProducts[itemIndex]._name,
                tempCount > _basketProducts[itemIndex]._count ? _basketProducts[itemIndex]._count : tempCount,
                _basketProducts[itemIndex]._price,
                _basketProducts[itemIndex]._id);

            Warehouse.AddItem(tempProduct);
            RemoveItemCount(itemIndex, tempCount);

            Console.WriteLine("Item return to inventory. Press any key to continue");
            Console.ReadKey();
        }

        private static void RemoveItemCount(int itemIndex, int count)
        {

            if (count >= _basketProducts[itemIndex]._count)
                _basketProducts.RemoveAt(itemIndex);
            else
            {
                _basketProducts[itemIndex]._count -= count;
            }
        }

        public static void ClearBasket()
        {
            if (_basketProducts.Count > 1)
            {
                foreach (var product in _basketProducts)
                {
                    Warehouse.AddItem(product);
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
            _basketProducts.Clear();
        }

        private static void MakeChoise(ConsoleKey key)
        {
            if (_basketCursorPosition < _basketProducts.Count - 1 && key == ConsoleKey.DownArrow)
            {
                _basketCursorPosition++;
                ShowBasket();
            }

            else if (_basketCursorPosition > 0 && key == ConsoleKey.UpArrow)
            {
                _basketCursorPosition--;
                ShowBasket();
            }
            CheckKey();
        }
    }
}
