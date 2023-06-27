using demo.domain;

namespace demo.bl;

public interface ICategorieManager
{
    IEnumerable<Category> GetAllCategories();
    Category GetCategory(int id);
    Category AddCategory(string naam);
    Category UpdateCategory(int categoryId, string name);
    void RemoveCategory(int categoryId);
}