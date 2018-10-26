using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Pages;


namespace SportsStore.Models.Interfaces
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();
        private DataContext context;

        public DataRepository(DataContext ctx) => context = ctx;

        // public IEnumerable<Product> Products => context.Products;
        public IEnumerable<Product> Products => context.Products.Include(p => p.Category).ToArray();

        //public IEnumerable<Product> Products => data;
        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public Product GetProduct(long key) => context.Products.Include(p => p.Category).First(p => p.Id == key);

        public void UpdateProduct(Product product)
        {
            Product p = GetProduct(product.Id);
            p.Name = product.Name;
            //p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            // context.Products.Update(product);
            context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            //context.Products.UpdateRange(products);
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = context.Products.Where(p => data.Keys.Contains(p.Id));
            foreach (Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;

            }
            context.SaveChanges();
        }
        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        /*public PagedList<Product> GetProducts(QueryOptions options)
        {
            return new PagedList<Product>(context.Products
            .Include(p => p.Category), options);
        }*/

        public PagedList<Product> GetProducts(QueryOptions options, long category = 0)
        {
            IQueryable<Product> query = context.Products.Include(p => p.Category);
            if (category != -0)
            {
                query = query.Where(p => p.CategoryId == category);
            }
            return new PagedList<Product>(query, options);
        }
    }
}
