using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ShoppingCart.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Entities.Concrete
{
    public class Product : MongoDbEntity
    { 
        public string ProductName { get; set; }
        public int Stock { get; set; }
    }
}
