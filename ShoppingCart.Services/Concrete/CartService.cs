
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

        public async Task<Response<Product>> InsertItem(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            else if (product.Stock == 0)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = "Out of stock."
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
                    int index = sessionCart.FindIndex(x => x.Product.Id.Contains(id));

                    if (index == -1)
                        sessionCart.Add(new Item { Product = product, Quantity = 1 });
                    else
                        sessionCart[index].Quantity += 1;

                    SessionHelper.SetObject(_httpContextAccessor.HttpContext.Session, "cart", sessionCart);

                }
                return new Response<Product>
                {
                    Success = true,
                    Message = "Product successfully added to cart.",
                    Data = product
                };

            }
            catch (Exception ex)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
          
        }
        public Task<List<Item>> GetItems()
        {
            return Task.FromResult(SessionHelper.GetObject<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart"));
        }


    }
}
