using Mocks_and_Stubs;
using Moq;
using NUnit.Framework;

namespace TestWithMocks
{
    class Tests
    {
        private const string productName = "Apple";
        private const int pieces = 50;
        //private Warehouse warehouse = new Warehouse();

        [SetUp]
        public void Setup()
        {
            Order order = new Order(productName, pieces);
            var mockWarehouse = new Mock<IWarehouse>(MockBehavior.Strict);
            mockWarehouse.Setup(x => x.HasAmount(productName, pieces)).Returns(true);
            mockWarehouse.Setup(x => x.Remove(It.IsAny<Order>())).Callback((Order o) =>
            {
                o.IsFilled = true;
            });
        }

        [Test]
        public void TestFillingRemovesInventoryIfInStock()
        {
            order.Fill(mockWarehouse.Object);

            Assert.IsTrue(order.IsFilled);
            mockWarehouse.Verify(x => x.HasAmount(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            mockWarehouse.Verify(x => x.Remove(It.IsAny<Order>()), Times.Once());
        }

        [Test]
        public void TestFillingDoesNotRemoveIfNotEnoughInStock()
        {
            Order order = new Order(productName, pieces + 1);
            var mockWarehouse = new Mock<IWarehouse>(MockBehavior.Strict);
            mockWarehouse.Setup(x => x.HasAmount(productName, pieces + 1)).Returns(false);
            order.Fill(mockWarehouse.Object);
            Assert.IsFalse(order.IsFilled);
            mockWarehouse.Verify(x => x.HasAmount(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            mockWarehouse.Verify(x => x.Remove(It.IsAny<Order>()), Times.Never());
        }
    }
}