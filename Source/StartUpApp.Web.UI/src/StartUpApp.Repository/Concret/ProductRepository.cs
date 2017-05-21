using StartUpApp.Core.Domain;
using StartUpApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartUpApp.DataAccess;

namespace StartUpApp.Repository.Concret
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(StartUpAppDbContext DbContext) : base(DbContext)
        {
        }
    }
}
