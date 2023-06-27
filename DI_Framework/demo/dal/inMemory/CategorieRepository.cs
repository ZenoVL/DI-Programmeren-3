using demo.domain;

namespace demo.dal;

public class CategorieRepository : ICategorieRepository
{
    private readonly ICollection<Category> _categories;
    private static int _nextid = 1;

    public CategorieRepository()
    {
        _categories = new List<Category>();
        _categories.Add(new Category("Alcoholische drank"){Id = _nextid++});
    }
    
    public IEnumerable<Category> ReadAllCategories()
    {
        return _categories;
    }

    public Category ReadCategory(int id)
    {
        return _categories.SingleOrDefault(c => c.Id == id);
    }

    public Category CreateCategory(Category category)
    {
        category.Id = _nextid++;
        _categories.Add(category);
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        var oldCategory = _categories.Single(c => c.Id == category.Id);
        oldCategory.Name = category.Name;
        return oldCategory;
    }

    public void DeleteCategory(Category category)
    {
        _categories.Remove(category);
    }
}