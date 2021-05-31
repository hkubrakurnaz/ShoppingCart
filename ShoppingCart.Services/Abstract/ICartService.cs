using ShoppingCart.Core.Models;
using ShoppingCart.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Abstract
{
    public interface ICartService
    {
        public Task<Response<Product>> AddProduct(string id);
        Task<List<Item>> GetItems();
    }
}
