using demo.dal;
using demo.domain;
using KdG.DI.components.attributes;

namespace demo.bl;

[EnableLogged]
public class CategorieManager : ICategorieManager
{
    private readonly ICategorieRepository _repo;

    public CategorieManager(ICategorieRepository repo)
    {
        _repo = repo;
    }
    
    [Logged]
    public virtual IEnumerable<Category> GetAllCategories()
    {
        return _repo.ReadAllCategories();
    }

    public Category GetCategory(int id)
    {
        return _repo.ReadCategory(id);
    }

    public Category AddCategory(string name)
    {
        var category = new Category(name);

        return _repo.CreateCategory(category);
    }

    public Category UpdateCategory(int categoryId, string name)
    {
        var category = GetCategory(categoryId);
        category.Name = name;
        _repo.UpdateCategory(category);
        return category;
    }

    public void RemoveCategory(int categoryId)
    {
        var category = GetCategory(categoryId);
        _repo.DeleteCategory(category);
    }
}