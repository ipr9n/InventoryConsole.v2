using System.Collections.Generic;

namespace Inventory
{
    public class Storage
    {
        private List<Product> _storageProducts = new List<Product>();

        public void AddItem(Product item)
        {

            if (IsProductExists(item, out int i))
            {
                _storageProducts[i] += item;
            }
            else
                _storageProducts.Add(item);
        }

        private bool IsProductExists(Product item, out int i)
        {
            for (i = 0; i < _storageProducts.Count; i++)
            {
                if (item == _storageProducts[i])
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeItemPrice(int itemIndex, double price) =>_storageProducts[itemIndex]._price = price;

        public void RemoveItemCount(int itemIndex, int count)
        {

            if (count >= _storageProducts[itemIndex]._count)
                _storageProducts.RemoveAt(itemIndex);
            else
                _storageProducts[itemIndex]._count -= count;
        }

        public void AddItemCount(int itemIndex, int count) => _storageProducts[itemIndex]._count += count;

        public List<Product> GetStorageItems()
        {
            return _storageProducts;
        }

        public void Clear()
        {
            _storageProducts.Clear();
        }
    }
}
