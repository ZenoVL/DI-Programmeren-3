namespace demo.domain;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }

    public Category(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"id: {Id}, naam: {Name}";
    }
}