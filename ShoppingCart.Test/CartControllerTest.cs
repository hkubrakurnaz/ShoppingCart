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
    public class CartControllerTest
    {
        public Mock<ICartService> _cartService = new Mock<ICartService>();


        [Fact]
        public async void AddProductToCart_GivenValidId_ReturnSuccess()
        {
            string id = "60b27814c8a1a343860387e0";

            var response = new Response<Product>
            {
                Success = true,
                Message = "Product successfully added to cart.",
                Data = new Product
                {
                    Id = id,
                    ProductName = "test2",
                    Stock = 15
                }
            };

            _cartService.Setup(x => x.InsertItem(id)).ReturnsAsync(response);
            // arrange
            var controller = new CartController(_cartService.Object);

            //act 
            var result = await controller.InsertItem(id);

            //assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom <Response<Product>> (actionResult.Value);

            Assert.True(model.Success);
            Assert.Equal("Product successfully added to cart.", model.Message);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async void AddProductToCart_GivenInvalidId_ReturnFail()
        {
            string id = "60b27814c8a1a343860387e7";

            var controller = new CartController(_cartService.Object);

            var result = await controller.InsertItem(id);

            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]

        public async void AddProductToCart_GivenOutOfStock_ReturnMessage()
        {
            string id = "60b4e815c5af8af4341402ee";

            var response = new Response<Product>
            {
                Success = false,
                Message = "Out of stock."
            };

            _cartService.Setup(x => x.InsertItem(id)).ReturnsAsync(response);
            var controller = new CartController(_cartService.Object);

            var result = await controller.InsertItem(id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<Product>>(actionResult.Value);

            Assert.False(model.Success);
            Assert.Equal("Out of stock.", model.Message);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }
    }
}
