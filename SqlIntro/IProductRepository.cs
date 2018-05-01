using System;
using System.Collections.Generic;
using System.Text;

namespace SqlIntro
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductswithReviews();
        IEnumerable<Product> GetProductsandReview();
        void DeleteProduct(int id);
        void UpdateProduct(Product prod);
        void InsertProduct(Product prod);
    }
}
