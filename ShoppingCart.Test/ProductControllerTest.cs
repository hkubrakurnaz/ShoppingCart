using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.API.Controllers;
using ShoppingCart.Core.Models;
using ShoppingCart.Entities.Concrete;
using ShoppingCart.Services.Abstract;
using ShoppingCart.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCart.Test
{
    public class ProductControllerTest
    {
        public Mock<IProductService> mock = new Mock<IProductService>();

    
        [Fact]
        public async void Test1()
        {
            string id = "60b27814c8a1a343860387e0";

            var productDTO = new Product
            {
                Id = id,
                ProductName = "test2",
                Stock = 15
            };
              
            
            // arrange
            mock.Setup(p => p.GetByIdAsync(id)).ReturnsAsync(productDTO);
            var controller = new ProductController(mock.Object);

            //act 
            var result = await controller.GetProductById(id);

            //assert
            //Assert.NotNull()
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(productDTO,returnValue);

        }
    }
}
