using HappyCakeStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCakeStore.Domain.Entities;

namespace HappyCakeStore.Domain.Business
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);

            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.ProductName = product.ProductName;
                    dbEntry.UnitPrice = product.UnitPrice;
                    dbEntry.UnitInStock = product.UnitInStock;
                }
            }
            context.SaveChanges();
        }
    }
}
