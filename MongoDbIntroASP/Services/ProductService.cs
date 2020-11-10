using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbIntroASP.Models;

namespace MongoDbIntroASP.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<ProductCatalog> _products;

        public ProductService(IProductCatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<ProductCatalog>(settings.ProductsCollectionName);
        }

        public List<ProductCatalog> Get() =>
            _products.Find(book => true).ToList();

        public ProductCatalog Get(string id) =>
            _products.Find<ProductCatalog>(product => product.Id == id).FirstOrDefault();

        public ProductCatalog Create(ProductCatalog product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(string id, ProductCatalog productIn) =>
            _products.ReplaceOne(product => product.Id == id, productIn);

        public void Remove(ProductCatalog productIn) =>
            _products.DeleteOne(product => product.Id == productIn.Id);

        public void Remove(string id) =>
            _products.DeleteOne(product => product.Id == id);
    }
}
