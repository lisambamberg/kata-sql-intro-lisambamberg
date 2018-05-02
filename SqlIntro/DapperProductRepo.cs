using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Dapper;


namespace SqlIntro
{
    public class DapperProductRepo : IProductRepository
    {
        private readonly string _connectionString;

        public DapperProductRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetProductsandReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string sql = "SELECT product.name, productreview.Comments from product" +
                                   "LEFT JOIN ProductReview on product.ProductId = ProductReview.ProductId" +
                                   "WHERE Comments is NOT NULL;";
                return conn.Query<Product>(sql);
            }
        }

        public IEnumerable<Product> GetProductswithReviews()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string sql = "SELECT product.name FROM product INNER JOIN productreview on product.ProductID = productreview.ProductID;";
                return conn.Query<Product>(sql);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT ProductId as Id, Name FROM product;");
            }
        }

        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("DELETE FROM product WHERE ProductID = @id", new { id });
            }
        }

        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("UPDATE product SET Name = @name WHERE ProductId = @id", new { id = prod.Id, name = prod.Name });
            }
        }

        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("INSERT INTO product (Name) values(@name)", new { name = prod.Name });
            }
        }
    }
}
