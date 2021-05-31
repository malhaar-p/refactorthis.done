using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repository
{
    interface IProductOptionRepository
    {
        void Delete(Guid optionId);
        void Save(ProductOption productOption);
        ProductOption GetProductOption(Guid id);
        List<ProductOption> GetProductOptions(Guid? optionId);
    }
}
