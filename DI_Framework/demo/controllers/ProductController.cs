using demo.bl;
using KdG.DI.components;
using KdG.DI.components.attributes;

namespace demo.controllers;

public class ProductController:Controller
{
    private readonly IProductManager _manager;
    private readonly ICategorieManager _categorieManager;
    
    public ProductController(IProductManager manager, ICategorieManager categorieManager)
    {
        _manager = manager;
        _categorieManager = categorieManager;
    }
    
    [Init]
    public void Menu()
    {
        Console.WriteLine("Welkom bij de productController: Deze controller bied volgende opties");
        Console.WriteLine("1) GetAllProducts");
        Console.WriteLine("2) GetProduct");
        Console.WriteLine("3) GetProductsForCategorie");
        Console.WriteLine("4) AddProduct");
        Console.WriteLine("5) UpdateProduct");
        Console.WriteLine("6) RemoveProduct");
    }

    [KeyInputGetByNumber(1)]
    [KeyInputGetByMethodName("/Products/All")]
    public void GetAllProducts()
    {
        foreach (var product in _manager.GetAllProducts())
        {
            Console.WriteLine(product);
        }
    }

    [KeyInputGetByNumber(2)]
    public void GetProduct()
    {
        Console.Write("Geef het Id van het product: ");
        int input = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(_manager.GetProduct(input));
    }

    [KeyInputGetByNumber(3)]
    public void GetProductsForCategorie()
    {
        Console.Write("Geef het id van de categorie: ");
        var input = Convert.ToInt32(Console.ReadLine());

        foreach (var product in _manager.GetProductsForCategory(input))
        {
            Console.WriteLine(product);
        }
    }

    [KeyInputGetByNumber(4)]
    public void AddProduct()
    {
        Console.WriteLine("Nieuw product toevoegen");
        Console.Write("Productnaam: ");
        string name = Console.ReadLine();
        Console.Write("Price: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("categorie: ");
        foreach (var category in _categorieManager.GetAllCategories())
        {
            Console.WriteLine($"{category.Id}) {category.Name}");
        }
        Console.Write("geef het id van de categorie: ");
        int categoryId = Convert.ToInt32(Console.ReadLine());
        
        _manager.AddProduct(name, price,categoryId);
    }

    [KeyInputGetByNumber(5)]
    public void UpdateProduct()
    {
        Console.WriteLine("Selecteer het product dat u wilt updaten: ");
        foreach (var product in _manager.GetAllProducts())
        {
            Console.WriteLine($"{product.Id}) {product.Name}");
        }
        Console.Write("Product om up te daten: ");
        int productId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Nieuwe naam: ");
        string name = Console.ReadLine();
        Console.Write("Nieuwe prijs: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Nieuwe categorie: ");
        foreach (var category in _categorieManager.GetAllCategories())
        {
            Console.WriteLine($"{category.Id}) {category.Name}");
        }
        Console.Write("geef het id van de categorie: ");
        int categoryId = Convert.ToInt32(Console.ReadLine());
        
        _manager.UpdateProduct(productId, name, price, categoryId);
    }

    [KeyInputGetByNumber(6)]
    public void RemoveProduct()
    {
        Console.WriteLine("Selecteer het product dat u wilt verwijderen: ");
        foreach (var product in _manager.GetAllProducts())
        {
            Console.WriteLine($"{product.Id}) {product.Name}");
        }
        Console.Write("Product om te verwijderen: ");
        int productId = Convert.ToInt32(Console.ReadLine());
        
        _manager.RemoveProduct(productId);
    }
}