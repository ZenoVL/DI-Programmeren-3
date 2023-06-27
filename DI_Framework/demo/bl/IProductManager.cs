using demo.domain;

namespace demo.bl;

public interface IProductManager
{
    Product GetProduct(int id);
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsForCategory(int categoryId);
    void AddProduct(string name, double price, int categoryId);
    void UpdateProduct(int productId, string name, double price, int categoryId);
    void RemoveProduct(int id);
}