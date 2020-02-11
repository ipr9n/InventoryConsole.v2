using System;

namespace Inventory
{
    public class Product
    {
        public string _name;
        public int _count;
        public Guid _id { get; private set; }
        public double _price;

        public Product(string name, int count, double price)
        {
            _name = name;
            _count = count;
            _price = price;
            _id = Guid.NewGuid();
        }

        public Product(string name, int count, double price, Guid id)
        {
            _name = name;
            _count = count;
            _price = price;
            _id = id;
        }

        public static bool operator ==(Product c1, Product c2)
        {
            if (c1._id == c2._id) return true;
            else return false;
        }

        public static bool operator !=(Product c1, Product c2)
        {
            if (c1._id != c2._id) return true;
            else return false;
        }

        public static Product operator +(Product c1, Product c2)
        {
            return new Product(c1._name,c1._count+c2._count,c1._price);
        }

        public override string ToString()
        {
            return $"\tProduct name is {_name}\t" +
                   $"Product count is {_count}\t" +
                   $"Product price is {_price}\t" +
                   $"Product id is {_id}";
        }
    }
}