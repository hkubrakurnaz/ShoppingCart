
using ShoppingCart.Data.Abstract;
using ShoppingCart.Entities.Concrete;
using ShoppingCart.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(string id, string type = "object")
        {
            var result = await _productRepository.GetByIdAsync(id, "guid");

            //if(result != null)
            //{

            //}
            return result;
        }
    }
}
