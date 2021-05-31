
using Microsoft.AspNetCore.Http;
using ShoppingCart.Core.Models;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Entities.Concrete;
using ShoppingCart.Helpers;
using ShoppingCart.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        public CartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public async Task<Response<Product>> AddProduct(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            // Check if the product id exists
            if (product == null)
            {
                return new Response<Product>
                {
                    IsSuccess = false,
                    Message = "Invalid product."
                };
            }

            // Check if the product is in stock
            else if (product.Stock == 0)
            {
                return new Response<Product>
                {
                    IsSuccess = false,
                    Message = "Out of stock.",
                    Data = product
                };
            }
            try
            {
                var sessionCart = SessionHelper.GetObject<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart");

                if (sessionCart == null)
                {
                    sessionCart = new List<Item>();
                    sessionCart.Add(new Item { Product = product, Quantity = 1 });
                    SessionHelper.SetObject(_httpContextAccessor.HttpContext.Session, "cart", sessionCart);
                }
                else
                {
                    // If the session has the same product, the quantity is increased by 1,
                    // otherwise the quantity value is assigned as 1.

                    int index = sessionCart.FindIndex(x => x.Product.Id.Contains(id));

                    if (index == -1)
                        sessionCart.Add(new Item { Product = product, Quantity = 1 });
                    else
                        sessionCart[index].Quantity += 1;

                    SessionHelper.SetObject(_httpContextAccessor.HttpContext.Session, "cart", sessionCart);

                }
                return new Response<Product>
                {
                    IsSuccess = true,
                    Message = "Product successfully added to cart.",
                    Data = product
                };

            }
            catch (Exception ex)
            {
                return new Response<Product>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
          
        }
        public async Task<List<Item>> GetItems()
        {
            return await Task.FromResult(SessionHelper.GetObject<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart"));
        }


    }
}
