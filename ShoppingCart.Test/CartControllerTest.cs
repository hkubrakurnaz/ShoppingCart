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
                IsSuccess = true,
                Message = "Product successfully added to cart.",
                Data = new Product
                {
                    Id = id,
                    ProductName = "test2",
                    Stock = 15
                }
            };

            _cartService.Setup(x => x.AddProduct(id)).ReturnsAsync(response);
            var controller = new CartController(_cartService.Object);

            var result = await controller.AddProduct(id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom <Response<Product>> (actionResult.Value);

            Assert.True(model.IsSuccess);
            Assert.Equal("Product successfully added to cart.", model.Message);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
        }

        [Fact]
        public async void AddProductToCart_GivenInvalidId_ReturnFail()
        {
            string id = "60b27814c8a1a343860387e7";

            var response = new Response<Product>
            {
                IsSuccess = false,
                Message = "Out of stock.",

            };

            _cartService.Setup(x => x.AddProduct(id)).ReturnsAsync(response);
            var controller = new CartController(_cartService.Object);

            var result = await controller.AddProduct(id);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<Product>>(actionResult.Value);

            Assert.False(model.IsSuccess);
            Assert.Equal("Out of stock.", model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);

        }
        [Fact]

        public async void AddProductToCart_GivenOutOfStock_ReturnMessage()
        {
            string id = "60b4e815c5af8af4341402ee";

            var response = new Response<Product>
            {
                IsSuccess = false,
                Message = "Out of stock.",
                Data = new Product
                {
                    Id = id,
                    ProductName = "test3",
                    Stock = 0
                }
            };

            _cartService.Setup(x => x.AddProduct(id)).ReturnsAsync(response);
            var controller = new CartController(_cartService.Object);

            var result = await controller.AddProduct(id);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<Product>>(actionResult.Value);

            Assert.False(model.IsSuccess);
            Assert.Equal("Out of stock.", model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
        }
    }
}
