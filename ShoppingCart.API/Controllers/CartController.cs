﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<ActionResult<Product>> InsertItem(string id)
        {
            var result =  await _cartService.InsertItem(id);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            var result = await _cartService.GetItems();
            return result;
        }
    }
}