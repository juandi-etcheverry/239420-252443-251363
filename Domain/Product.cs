namespace Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public IList<Color> Colors { get; set; }
}