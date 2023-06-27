using demo.bl;
using KdG.DI.components;
using KdG.DI.components.attributes;

namespace demo.controllers;

public class CategorieController:Controller
{
    private readonly ICategorieManager _categorieManager;
    
    public CategorieController(ICategorieManager categorieManager)
    {
        _categorieManager = categorieManager;
    }
    
    [Init]
    public void Menu()
    {
        Console.WriteLine("Welkom bij de categorieController: Deze controller bied volgende opties");
        Console.WriteLine("7) GetAllCategories");
        Console.WriteLine("8) GetCategory");
        Console.WriteLine("9) AddCategory");
        Console.WriteLine("10) UpdateCategory");
        Console.WriteLine("11) RemoveCategory");
    }

    [KeyInputGetByNumber(7)]
    public void GetAllCategories()
    {
        foreach (var category in _categorieManager.GetAllCategories())
        {
            Console.WriteLine(category);
        }
    }

    [KeyInputGetByNumber(8)]
    public void GetCategory()
    {
        Console.Write("Geef het Id van het category: ");
        int input = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(_categorieManager.GetCategory(input));
    }

    [KeyInputGetByNumber(9)]
    public void AddCategory()
    {
        Console.WriteLine("Nieuw categorie toevoegen");
        Console.Write("Naam: ");
        string name = Console.ReadLine();

        _categorieManager.AddCategory(name);
    }

    [KeyInputGetByNumber(10)]
    public void UpdateCategory()
    {
        Console.WriteLine("Selecteer de category dat u wilt updaten: ");
        foreach (var category in _categorieManager.GetAllCategories())
        {
            Console.WriteLine($"{category.Id}) {category.Name}");
        }
        Console.Write("Category om up te daten: ");
        int categoryId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Nieuwe naam: ");
        string name = Console.ReadLine();

        _categorieManager.UpdateCategory(categoryId, name);
    }

    [KeyInputGetByNumber(11)]
    public void RemoveCategory()
    {
        Console.WriteLine("Selecteer de category dat u wilt verwijderen: ");
        foreach (var category in _categorieManager.GetAllCategories())
        {
            Console.WriteLine($"{category.Id}) {category.Name}");
        }
        Console.Write("Categorie om te verwijderen: ");
        int categoryId = Convert.ToInt32(Console.ReadLine());
        
        _categorieManager.RemoveCategory(categoryId);
    }
}