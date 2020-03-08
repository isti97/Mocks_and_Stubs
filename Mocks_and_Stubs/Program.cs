using System;
using System.Collections.Generic;

namespace Mocks_and_Stubs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Warehouse : IWarehouse
    {
        public Warehouse()
        {
            this.NameAndAmount = new Dictionary<string, int>();
        }

        public Dictionary<string, int> NameAndAmount
        {
            get;
            set;
        }

        public void Add(string productName, int amount)
        {
            this.NameAndAmount.Add(productName, amount);
        }

        public int GetAmount(string productName)
        {
            return NameAndAmount[productName];
        }

        public bool HasAmount(string productName, int amount)
        {
            if (this.NameAndAmount[productName] - amount >= 0)
            {
                return true;
            }

            return false;
        }

        public void Remove(Order order)
        {
            if (this.NameAndAmount.ContainsKey(order.OrderedProduct))
            {
                this.NameAndAmount[order.OrderedProduct] -= order.OrderedAmount;
                order.IsFilled = true;
            }
        }
    }

    public class Order
    {
        public Order(string productName, int amount)
        {
            this.OrderedProduct = productName;
            this.OrderedAmount = amount;
            this.IsFilled = false;
        }

        public string OrderedProduct
        {
            get;
            set;
        }

        public int OrderedAmount
        {
            get;
            set;
        }

        public bool IsFilled
        {
            get;
            set;
        }

        public void Fill(IWarehouse warehouse)
        {
            if (warehouse.HasAmount(this.OrderedProduct, this.OrderedAmount) == true)
            {
                warehouse.Remove(this);
            }
        }
    }

    public interface IWarehouse
    {
        Dictionary<string, int> NameAndAmount
        {
            get;
            set;
        }

        void Add(string productName, int amount);

        int GetAmount(string productName);

        void Remove(Order order);

        bool HasAmount(string productName, int amount);
    }
}

namespace Stubs
{
    public class Warehouse
    {
        public Warehouse()
        {
            this.NameAndAmount = new Dictionary<string, int>();
        }

        public Dictionary<string, int> NameAndAmount
        {
            get;
            set;
        }

        public void Add(string productName, int amount)
        {
            this.NameAndAmount.Add(productName, amount);
        }

        public int GetAmount(string productName)
        {
            return NameAndAmount[productName];
        }

        public bool HasAmount(string productName, int amount)
        {
            if (this.NameAndAmount[productName] - amount >= 0)
            {
                return true;
            }

            return false;
        }

        public void Remove(Order order)
        {
            if (this.NameAndAmount.ContainsKey(order.OrderedProduct))
            {
                this.NameAndAmount[order.OrderedProduct] -= order.OrderedAmount;
                order.IsFilled = true;
            }
        }
    }

    public class Order
    {
        public Order(string productName, int amount)
        {
            this.OrderedProduct = productName;
            this.OrderedAmount = amount;
            this.IsFilled = false;
        }

        public string OrderedProduct
        {
            get;
            set;
        }

        public int OrderedAmount
        {
            get;
            set;
        }

        public bool IsFilled
        {
            get;
            set;
        }

        public void Fill(Warehouse warehouse)
        {
            if (warehouse.HasAmount(this.OrderedProduct, this.OrderedAmount) == true)
            {
                warehouse.Remove(this);
            }
        }
    }
}