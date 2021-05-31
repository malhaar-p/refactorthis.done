using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repository
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        public ProductOption GetProductOption(Guid id)
        {
            ProductOption productOption = new ProductOption();

            productOption.IsNew = true;
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"select * from productoptions where id = '{id}' collate nocase";

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return productOption;

            productOption.IsNew = false;
            productOption.Id = Guid.Parse(rdr["Id"].ToString());
            productOption.ProductId = Guid.Parse(rdr["ProductId"].ToString());
            productOption.Name = rdr["Name"].ToString();
            productOption.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();

            return productOption;
        }

        public void Save(ProductOption productOption)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = productOption.IsNew
                ? $"insert into productoptions (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')"
                : $"update productoptions set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}' collate nocase";

            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid optionId)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"delete from productoptions where id = '{optionId}' collate nocase";
            cmd.ExecuteReader();
        }

        public List<ProductOption> GetProductOptions(Guid? optionId)
        {
            string where = null;
            List<ProductOption> productOptionsList = new List<ProductOption>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            if (optionId.HasValue)
                where = $"where productid = '{optionId}' collate nocase";

            cmd.CommandText = $"select id from productoptions {where}";

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                productOptionsList.Add(GetProductOption(id));
            }
            return productOptionsList;
        }
    }
}
