
using Microsoft.AspNetCore.Http;
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
        private readonly IProductService _productService;
        public CartService(IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        public async Task<Product> InsertItem(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            var sessionCart = SessionHelper.GetObject<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart");

            if (sessionCart == null)
            {
                sessionCart = new List<Item>();
                sessionCart.Add(new Item { Product = product, Quantity = 1 });
                SessionHelper.SetObject(_httpContextAccessor.HttpContext.Session, "cart", sessionCart  );
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

            return product;
        }
        public Task<List<Item>> GetItems()
        {
            return Task.FromResult(SessionHelper.GetObject<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart"));
        }



    }
}
