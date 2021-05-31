using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repository
{
    interface IProductRepository
    {
        void Save(Product product);
        void Delete(Guid id);
        List<Product> GetProducts(string name);
        Product GetProduct(Guid id);
    }
}
