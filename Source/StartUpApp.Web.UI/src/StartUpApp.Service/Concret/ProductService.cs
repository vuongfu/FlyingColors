using StartUpApp.Core.Domain;
using StartUpApp.Repository.Abstract;
using StartUpApp.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpApp.Service.Concret
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(IProductRepository repository)
        {
            Repository = repository;
        }
    }
}
