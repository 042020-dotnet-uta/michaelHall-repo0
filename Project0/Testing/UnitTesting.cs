using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using StoreApp;
using StoreApp.BusinessLogic;
using StoreApp.Data_Access;

namespace Testing
{
    public class UnitTesting
    {
        /// <summary>
        /// Tests the product model by adding to it and
        /// verifying that the products show up in the database
        /// </summary>
        [Fact]
        public void AddsProductToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "AddsProductToDbTest")
                .Options;

            //Act
            using (var db = new StoreApp_DbContext(options))
            {
                Product bar = new Product
                {
                    StoreID = 7,
                    ProductName = "bar",
                    Inventory = 5,
                    Price = 10
                };

                db.Add(bar);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreApp_DbContext(options))
            {
                Assert.Equal(1, context.Products.Count());

                var product1 = context.Products.Where(p => p.StoreID == 7).FirstOrDefault();
                Assert.Equal(7, product1.StoreID);
                Assert.Equal(1, product1.ProductID);
            }
        }

        [Fact]
        public void AddsStoreToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "AddsStoreToDbTest")
                .Options;

            //Act
            using (var db = new StoreApp_DbContext(options))
            {
                Store location = new Store
                {
                    Location = "Maryland"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreApp_DbContext(options))
            {
                Assert.Equal(1, context.Stores.Count());

                var store1 = context.Stores.Where(s => s.StoreID == 1).FirstOrDefault();
                Assert.Equal(1, store1.StoreID);
                Assert.Equal("Maryland", store1.Location);
            }
        }

        [Fact]
        public void AddsCustomerToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "AddsCustomerToDbTest")
                .Options;

            //Act
            using (var db = new StoreApp_DbContext(options))
            {
                Customer location = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall"
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreApp_DbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());

                var customer1 = context.Customers.Where(c => c.CustomerID == 1).FirstOrDefault();
                Assert.Equal(1, customer1.CustomerID);
                Assert.Equal("Michael", customer1.FirstName);
                Assert.Equal("Hall", customer1.LastName);
                Assert.Equal("mbhall", customer1.UserName);
            }
        }

        [Fact]
        public void AddsOrderToDbTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "AddsOrderToDbTest")
                .Options;

            //Act
            using (var db = new StoreApp_DbContext(options))
            {
                Order location = new Order
                {
                    CustomerID = 5,
                    ProductID = 10,
                    Quantity = 3,
                    Timestamp = DateTime.Today
                };

                db.Add(location);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreApp_DbContext(options))
            {
                Assert.Equal(1, context.Orders.Count());

                var order1 = context.Orders.Where(o => o.OrderID == 1).FirstOrDefault();
                Assert.Equal(1, order1.OrderID);
                Assert.Equal(5, order1.CustomerID);
                Assert.Equal(10, order1.ProductID);
                Assert.Equal(3, order1.Quantity);
                Assert.Equal(DateTime.Today, order1.Timestamp);
            }
        }

        [Fact]
        public void ValidNameTest()
        {
            CustomerCreation validation = new CustomerCreation();

            string nameTest1 = "Mike";
            string nameTest2 = "4after";
            string nameTest3 = "after@dark";

            Assert.True(validation.IsValidName(nameTest1));
            Assert.False(validation.IsValidName(nameTest2));
            Assert.False(validation.IsValidName(nameTest3));
        }

        [Fact]
        public void ValidUserNameTest()
        {
            CustomerCreation validation = new CustomerCreation();

            string usernameTest1 = "72838meah";
            string usernameTest2 = "pie";
            string usernameTest3 = "pie@2019withme";

            Assert.True(validation.IsValidUserName(usernameTest1));
            Assert.True(validation.IsValidUserName(usernameTest2));
            Assert.False(validation.IsValidUserName(usernameTest3));
        }

        [Fact]
        public void ValidNumTests()
        {
            OrderCreation test = new OrderCreation();

            string validNum1 = "Mike";
            string validNum2 = "32";

            Assert.False(test.IsValidNum(validNum1));
            Assert.True(test.IsValidNum(validNum2));
        }

        [Fact]
        public void StringToIntTests()
        {
            OrderCreation test = new OrderCreation();

            string stringToInt1 = "45";
            string stringToInt2 = "word";
            string stringToInt3 = "273";

            Assert.Equal(45, test.StringToInt(stringToInt1));
            Assert.Equal(0, test.StringToInt(stringToInt2));
            Assert.Equal(273, test.StringToInt(stringToInt3));
        }

        [Fact]
        public void UnreasonableOrderTest()
        {
            OrderCreation test = new OrderCreation();

            int unreasonable1 = 25;
            int unreasonable2 = 5;

            Assert.True(test.IsUnreasonableQuantity(unreasonable1));
            Assert.False(test.IsUnreasonableQuantity(unreasonable2));
        }

        [Fact]
        public void CustomerQueryExceptionTest()
        {
            CustomerQueries check = new CustomerQueries();
            

            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerQueryExceptionTest")
                .Options;

            check.CustomerSearch("yes", "no");
        }
        
        [Fact]
        public void ValidCustomerIDQueryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerQueriesTests")
                .Options;

            //Act
            using (var db = new StoreApp_DbContext(options))
            {
                Customer customer = new Customer
                {
                    FirstName = "Michael",
                    LastName = "Hall",
                    UserName = "mbhall"
                };

                db.Add(customer);
                db.SaveChanges();
            }

            //Assert
            using (var context = new StoreApp_DbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                CustomerQueries check = new CustomerQueries();

                Assert.False(check.IsValidCustomerID(1));
                Assert.False(check.IsValidCustomerID(2));
                Assert.False(check.IsValidCustomerID(-5));
            }
        }
    }
}
