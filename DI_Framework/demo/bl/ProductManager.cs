using demo.dal;
using demo.domain;
using KdG.DI.components.attributes;

namespace demo.bl;

[EnableTimed]
[EnableLogged]
public class ProductManager:IProductManager
{
    private readonly IProductRepository _repo;
    private readonly ICategorieManager _categoryManager;

    public ProductManager(IProductRepository repo, ICategorieManager categoryManager)
    {
        _repo = repo;
        _categoryManager = categoryManager;
    }

    public Product GetProduct(int id)
    {
        return _repo.ReadProduct(id);
    } 
    
    [Timed]
    [Logged]
    public virtual IEnumerable<Product> GetAllProducts()
    {
        return _repo.ReadAllProducts();
    }

    public IEnumerable<Product> GetProductsForCategory(int categoryId)
    {
        return _repo.ReadProductForCategory(categoryId);
    }

    public void AddProduct(string name, double price, int categoryId)
    {
        var category = _categoryManager.GetCategory(categoryId);
        var product = new Product(name, price, category);
        _repo.CreateProduct(product);
    }

    public void UpdateProduct(int productId, string name, double price, int categoryId)
    {
        var category = _categoryManager.GetCategory(categoryId);
        var product = GetProduct(productId);
        product.Name = name;
        product.Price = price;
        product.Category = category;
        _repo.UpdateProduct(product);
    }

    public void RemoveProduct(int id)
    {
        var product = GetProduct(id);
        _repo.DeleteProduct(product);
    }
}