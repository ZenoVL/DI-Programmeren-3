namespace demo.domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }

    public Product(string name, double price, Category category)
    {
        Name = name;
        Price = price;
        Category = category;
    }

    public override string ToString()
    {
        return String.Format("id: {0}, name: {1}, price: {2}, categorie: {3}",Id,Name,Price,Category.Name);
    }
}