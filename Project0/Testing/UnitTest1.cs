using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using StoreApp;
using StoreApp.BusinessLogic;

namespace Testing
{
    public class UnitTesting
    {
        /// <summary>
        /// Tests the product model by adding to it and
        /// verifying that the products show up in the database
        /// </summary>
        [Fact]
        public void AddsProductToDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreApp_DbContext>()
                .UseInMemoryDatabase(databaseName: "AddsProductToDb")
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

                var p1Name = context.Products.Where(p => p.StoreID == 7).FirstOrDefault();
                Assert.Equal(7, p1Name.StoreID);
                Assert.Equal(1, p1Name.ProductID);
            }
        }

        [Fact]
        public void CustomerInputValidationTest()
        {
            CustomerCreation validation = new CustomerCreation();

            string nameTest1 = "Mike";
            string nameTest2 = "4after";
            string nameTest3 = "after@dark";
            string usernameTest1 = "72838meah";
            string usernameTest2 = "pie";
            string usernameTest3 = "pie@2019withme";

            Assert.True(validation.IsValidName(nameTest1));
            Assert.False(validation.IsValidName(nameTest2));
            Assert.False(validation.IsValidName(nameTest3));
            Assert.True(validation.IsValidUserName(usernameTest1));
            Assert.True(validation.IsValidUserName(usernameTest2));
            Assert.False(validation.IsValidUserName(usernameTest3));
        }
    }
}
