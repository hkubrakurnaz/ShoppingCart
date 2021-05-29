
using ShoppingCart.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Abstract
{
    public interface IProductService 
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id, string type = "object");
    }
}
