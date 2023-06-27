using demo.domain;

namespace demo.dal;

public interface IProductRepository
{
    Product ReadProduct(int id);
    IEnumerable<Product> ReadAllProducts();
    IEnumerable<Product> ReadProductForCategory(int categoryId);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}