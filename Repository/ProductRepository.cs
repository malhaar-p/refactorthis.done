using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repository
{
    public class ProductRepository : IProductRepository
    {
        IProductOptionRepository _productOptionRepository { get; }
        public ProductRepository()
        {
            _productOptionRepository = new ProductOptionRepository();
        }

        public void Save(Product product)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = product.IsNew
                ? $"insert into Products (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})"
                : $"update Products set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}' collate nocase";

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            foreach (var option in _productOptionRepository.GetProductOptions(id))
            {
                _productOptionRepository.Delete(option.Id);
            }
                
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"delete from Products where id = '{id}' collate nocase";
            cmd.ExecuteNonQuery();
        }

        public List<Product> GetProducts(string name)
        {
            string where = null;
            List<Product> productsList = new List<Product>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            if (!string.IsNullOrEmpty(name))
                where = $"where lower(name) like '%{name.ToLower()}%'";

            cmd.CommandText = $"select id from Products {where}";

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                productsList.Add(GetProduct(id));
            }
            return productsList;
        }

        public Product GetProduct(Guid id)
        {
            Product product = new Product();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from Products where id = '{id}' collate nocase";

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return product;

            product.Id = Guid.Parse(rdr["Id"].ToString());
            product.Name = rdr["Name"].ToString();
            product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
            product.Price = decimal.Parse(rdr["Price"].ToString());
            product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());

            return product;
        }
    }
}
