using RefactorThis.Models;
using RefactorThis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Business
{
    public class ProductOptionManager
    {
        private IProductOptionRepository _productOptionRepository { get; }
        private IProductRepository _productRepository { get; }

        public ProductOptionManager()
        {
            _productOptionRepository = new ProductOptionRepository();
            _productRepository = new ProductRepository();
        }

        public List<ProductOption> GetProductOptions(Guid id)
        {
            Product existingProduct = _productRepository.GetProduct(id);
            if (existingProduct.Id != Guid.Empty)
            {
                return _productOptionRepository.GetProductOptions(id);
            }
            else return null;
        }

        public ProductOption GetProductOption(Guid id, Guid optionId)
        {
            Product existingProduct = _productRepository.GetProduct(id);
            if (existingProduct != null)
            {
                return _productOptionRepository.GetProductOption(optionId);
            }
            else return null;
        }

        public void SaveProductOption(ProductOption productOption)
        {
            ProductOption existingProductOption = _productOptionRepository.GetProductOption(productOption.Id);
            if (existingProductOption.Id == Guid.Empty)
            {
                productOption.IsNew = true;
                _productOptionRepository.Save(productOption);
            }
        }

        public void UpdateProductOption(ProductOption productOption)
        {
            Product existingProduct = _productRepository.GetProduct(productOption.ProductId);
            if (existingProduct.Id != Guid.Empty)
            {
                productOption.IsNew = false;
                _productOptionRepository.Save(productOption);
            }
        }

        public void DeleteProductOption(Guid optionId)
        {
            ProductOption existingProductOption = _productOptionRepository.GetProductOption(optionId);
            if (existingProductOption.Id != Guid.Empty)
            {
                _productOptionRepository.Delete(optionId);
            }
        }
    }
}
