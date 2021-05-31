using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;
using RefactorThis.Business;
using System.Collections.Generic;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductManager ProductManager { get; }
        ProductOptionManager ProductOptionManager { get; }
        public ProductsController()
        {
            ProductManager = new ProductManager();
            ProductOptionManager = new ProductOptionManager();
        }

        [HttpGet]
        public List<Product> Get(string name)
        {
            return ProductManager.GetProducts(name);
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return ProductManager.GetProductById(id);
        }

        [HttpPost]
        public void Post(Product product)
        {
            ProductManager.SaveProduct(product);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, Product product)
        {
            ProductManager.UpdateProduct(id, product);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            ProductManager.DeleteProduct(id);
        }

        [HttpGet("{id}/options")]
        public List<ProductOption> GetOptions(Guid id)
        {
            return ProductOptionManager.GetProductOptions(id);
        }

        [HttpGet("{id}/options/{optionId}")]
        public ProductOption GetOption(Guid id, Guid optionId)
        {
            return ProductOptionManager.GetProductOption(id, optionId);
        }

        [HttpPost("{id}/options")]
        public void CreateOption(Guid id, ProductOption productOption)
        {
            productOption.ProductId = id;
            ProductOptionManager.SaveProductOption(productOption);
        }

        [HttpPut("{id}/options/{optionId}")]
        public void UpdateOption(Guid id, ProductOption productOption)
        {
            productOption.ProductId = id;
            ProductOptionManager.UpdateProductOption(productOption);
        }

        [HttpDelete("{id}/options/{optionId}")]
        public void DeleteOption(Guid optionId)
        {
            ProductOptionManager.DeleteProductOption(optionId);
        }
    }
}