using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingCart.Core.Repository.Abstract;
using ShoppingCart.Core.Settings;
using ShoppingCart.Data.Context;
using ShoppingCart.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class MongoRepositoryBase<TEntity> : IRepository<TEntity> where TEntity  : MongoDbEntity, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepositoryBase(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }

        public async  Task<List<TEntity>> GetAllAsync()
        {
            return  await _collection.AsQueryable().ToListAsync();
            //if (data != null)
            //    result.Result = data;
        }

        public async Task<TEntity> GetByIdAsync(string id, string type = "object")
        {
            return  await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}