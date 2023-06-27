using demo.domain;

namespace demo.dal;

public interface ICategorieRepository
{
    IEnumerable<Category> ReadAllCategories();

    Category ReadCategory(int id);
    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    void DeleteCategory(Category category);
}