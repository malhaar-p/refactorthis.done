using RefactorThis.Models;
using RefactorThis.Repository;
using System;
using System.Collections.Generic;

namespace RefactorThis.Business
{
    public class ProductManager
    {
        private IProductRepository _productRepository;
        public ProductManager()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetProducts(string name)
        {
            return _productRepository.GetProducts(name);
        }

        public Product GetProductById(Guid id)
        {
            return _productRepository.GetProduct(id);
        }

        public void SaveProduct(Product product)
        {
            Product existingProduct = _productRepository.GetProduct(product.Id);
            if (existingProduct.Id == Guid.Empty)
            {   
                product.IsNew = true;
                _productRepository.Save(product);
            }  
        }

        public void UpdateProduct(Guid id, Product product)
        {
            Product existingProduct = _productRepository.GetProduct(id);
            if (existingProduct.Id != Guid.Empty)
            {
                product.IsNew = false;
                _productRepository.Save(product);
            }
        }

        public void DeleteProduct(Guid id)
        {
            Product existingProduct = _productRepository.GetProduct(id);
            if (existingProduct != null)
            { 
                _productRepository.Delete(id);
            }
        }
    }
}
