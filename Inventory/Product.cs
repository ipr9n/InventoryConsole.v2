using System;

namespace Inventory
{
    public class Product
    {
        public string _name { get; set; }
        public int _count { get; set; }
        public Guid _id { get; }
        public double _price { get; set; }

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

        protected bool Equals(Product other)
        {
            return _id.Equals(other._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }

        public static Product operator +(Product c1, Product c2)
        {
            return new Product(c1._name, c1._count + c2._count, c1._price);
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