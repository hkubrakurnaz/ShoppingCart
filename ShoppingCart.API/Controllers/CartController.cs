using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Entities.Concrete;
using ShoppingCart.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Product id for test
        // 60b14c967ac7bedd4a83da2b
        // 60b27814c8a1a343860387e0
        // 60b4e815c5af8af4341402ee - stock = 0
        [HttpPost("add")]
        public async Task<ActionResult> AddProduct(string id)
        {
            var response =  await _cartService.AddProduct(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            var result = await _cartService.GetItems();
            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
