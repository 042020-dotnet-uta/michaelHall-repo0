using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using RPS_Game;
using System.Linq;

namespace XUnit_Test_RPS
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RPS_DbContext>()
                .UseInMemoryDatabase(databaseName: "Test1")
                .Options;

            // Act
            using (var db = new RPS_DbContext(options))
            {
                //Player p = new Player
                //{
                //    Losses = 5,
                //    Name = "Geralt",
                //    Wins = 11
                //};
                //db.Add(p);
                //db.SaveChanges();
            }

            // Assert
            using (var context = new RPS_DbContext(options))
            {
                //Assert.Equals(1, context.Players.Count());

                //var p1Name = context.Players.Where(p => p.Wins == 11).FirstOrDefault();
                //Assert.Equal("Geralt", p1Name.Name);
            }
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RPS_DbContext>()
                .UseInMemoryDatabase(databaseName: "Test1")
                .Options;
        }
        // more tests below
    }
}
