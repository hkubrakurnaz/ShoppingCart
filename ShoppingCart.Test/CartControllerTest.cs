using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.API.Controllers;
using ShoppingCart.Entities.Concrete;
using ShoppingCart.Services.Abstract;
using ShoppingCart.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCart.Test
{
    public class CartControllerTest
    {
        public Mock<ICartService> mock = new Mock<ICartService>();

    
        [Fact]
        public async void CreateActionResult_ReturnsNotFoundObjectResult_ForNonexistentProduct()
        {
            string id = "60b27814c8a1a343860387e7";
         
            // arrange
            //mock.Setup(p => p.InsertItem(id)).ReturnsAsync(productDTO);
            var controller = new CartController(mock.Object);

            //act 
            var result = await controller.InsertItem(id);

            //assert
            //Assert.NotNull()
            Assert.IsType<NotFoundObjectResult>(result);

        }
    }
}
