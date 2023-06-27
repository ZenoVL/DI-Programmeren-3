using demo.domain;
using KdG.DI.components.attributes;

namespace demo.dal.inMemory;

[EnableTimed]
public class ProductRepositoryInMemory : IProductRepository
{
    private IList<Product> _products;
    private static int _nextid = 1;

    public ProductRepositoryInMemory()
    {
        _products = new List<Product>();
        _products.Add(new Product("Carapils",0.3,new Category("Alcoholische dranken"){Id = 1}){Id = _nextid++});
    }

    public Product ReadProduct(int id)
    {
        return _products.Single(p => p.Id == id);
    }

    public IEnumerable<Product> ReadAllProducts()
    {
        return _products;
    }

    public IEnumerable<Product> ReadProductForCategory(int categoryId)
    {
        return _products.Where(p => p.Category.Id == categoryId);
    }

    public void CreateProduct(Product product)
    {
        product.Id = _nextid++;
        _products.Add(product);
    }

    [Timed]
    public virtual void UpdateProduct(Product product)
    {
        var oldproduct = _products.Single(p => p.Id == product.Id);
        oldproduct.Name = product.Name;
        oldproduct.Price = product.Price;
        oldproduct.Category = product.Category;
    }

    public void DeleteProduct(Product product)
    {
        _products.Remove(product);
    }
}