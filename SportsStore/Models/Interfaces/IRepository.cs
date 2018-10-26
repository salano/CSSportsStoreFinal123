using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.Pages;

namespace SportsStore.Models.Interfaces
{
   public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProduct(long key);
        void AddProduct(Product product);
        //PagedList<Product> GetProducts(QueryOptions options);
        PagedList<Product> GetProducts(QueryOptions options, long category = 0);

        void UpdateProduct(Product product);
        void UpdateAll(Product[] products);
        void Delete(Product product);
    }
}
