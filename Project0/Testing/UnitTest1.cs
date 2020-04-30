using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using StoreApp;

namespace Testing
{
    public class UnitTest1
    {
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
    }
}
