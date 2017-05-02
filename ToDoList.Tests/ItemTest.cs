using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using Xunit;

namespace ToDoList.Tests
{
    public class ItemTest
    {
        [Fact]
        public void GetDescriptionTest()
        {
            //Arrange
            var item = new Item();

            //Act
            item.Description = "Wash the dog";
            var result = item.Description;

            //Assert
            Assert.Equal("Wash the dog", result);
        }
    }
}
