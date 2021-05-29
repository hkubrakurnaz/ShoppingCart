using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingCart.Core.Settings;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Context;
using ShoppingCart.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Concrete
{
    public class ProductRepository : MongoRepositoryBase<Product>, IProductRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Product>();
        }
    }
}
