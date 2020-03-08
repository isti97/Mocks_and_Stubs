using Stubs;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class TestsWithStubs
    {
        //Test cases with stubs

        private const string productName = "Apple";
        private const int pieces = 50;
        private Warehouse warehouse = new Warehouse();

        [SetUp]
        public void Setup()
        {
            warehouse.Add(productName, pieces);
        }

        [Test]
        public void TestOrderIsFilledIfEnoughProductInWarehouse()
        {
            Order order = new Order(productName, pieces);
            order.Fill(warehouse);
            Assert.IsTrue(order.IsFilled);
            Assert.AreEqual(0, warehouse.GetAmount(productName));
        }

        [Test]
        public void TestOrderDoesNotRemoveIfNotEnoughProductInWarehouse()
        {
            Order order = new Order(productName, pieces + 1);
            order.Fill(warehouse);
            Assert.IsFalse(order.IsFilled);
            Assert.AreEqual(pieces, warehouse.GetAmount(productName));
        }  
    }
}
