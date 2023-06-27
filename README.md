# Gegevens student
naam: Van Leemput  
voornaam: Zeno

# Framework
DI Container .NET

# How to intall
## Builden van de code
```shell
cd .\DI_Framework\
dotnet build
```
## Runnen van de code
```shell
cd .\DI_Framework\demo\
dotnet run
```
## Testen van de code
```shell
cd .\DI_Framework\tests\
dotnet test 
```

# Code voorbeelden
## Program.cs
We starten bij het creëren van een DICollection.  
Zie onderstaand voorbeeld:
```cs
var services = new DICollection();
```
Om een service toe te voegen gebruiken we de **AddSingleton** methode. Deze kan je op verschillende manieren oproepen.  
Zie onderstaande voorbeelden:
### Zonder interface
```cs
services.AddSingleton<ProductManager>();
```
```cs
services.AddSingleton(typeof(ProductManager));
```
```cs
services.AddSingleton(new ProductManager());
```
### Met interface
```cs
services.AddSingleton<IProductManager, ProductManager>();
```

Om dan vervolgens het framework te starten, kunnen we gebruik maken van de **StartDI**.  
We geven hier als parameter ons Program mee zodat het framework hiermee verder aan de slag kan.
```cs
services.startDi(typeof(Program));
```

## Toevoegen van een Controller
Om een controller toe te voegen aan de applicatie laat je uw controller overerven van de **Controller** interface.
```cs
public class ProductController:Controller
{
}
```
Als je wilt dat bij initialisatie een bepaalde welkom tekst te voorschijn komt gebruik je het **[Init]** attribuut boven de methode waar je welkom bericht in staat
```cs
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
```
Om een bepaalde methode van de controller op te roepen zijn er 2 attributen beschikbaar:
- **[KeyInputGetByNumber(int number)]**
- **[KeyInputGetByMethodName(string route)]**  

Deze attributen kun je combineren. Je kan dus per methode de 2 attributen gebruiken, maar dit hoeft niet
```cs
    [KeyInputGetByNumber(1)]
    [KeyInputGetByMethodName("/Products/All")]
    public void GetAllProducts()
    {
        foreach (var product in _manager.GetAllProducts())
        {
            Console.WriteLine(product);
        }
    }
```

## Toevoegen van interception
Om gebruik te maken van interception bestaan er 4 attributen:
- **[EnableTimed]**
- **[EnableLogged]**
- **[Timed]**
- **[Logged]**

**[Logged]** en **[Timed]** worden boven de methode *(Let op deze methode moet virtual zijn om te werken)* gezet waar de interception op moet gebeuren. Ze kunnen gecombineerd worden, maar dit is niet noodzakelijk.  
Wel is het noodzakelijk om bij het bruik van een van de 2 of beide, boven de klasse de bijpassende **Enable** attribuut te plaatsen
bijvoorbeeld:
```cs
[EnableTimed]
[EnableLogged]
public class ProductManager:IProductManager
{
    [Timed]
    [Logged]
    public virtual IEnumerable<Product> GetAllProducts()
    {
        return _repo.ReadAllProducts();
    }
}
```
maar ook dit kan:
```cs
[EnableLogged]
public class ProductManager:IProductManager
{
    [Logged]
    public virtual IEnumerable<Product> GetAllProducts()
    {
        return _repo.ReadAllProducts();
    }
}
```
of andersom:
```cs
[EnableTimed]
public class ProductManager:IProductManager
{
    [Timed]
    public virtual IEnumerable<Product> GetAllProducts()
    {
        return _repo.ReadAllProducts();
    }
}
```