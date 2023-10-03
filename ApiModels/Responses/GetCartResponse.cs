using Domain;
namespace ApiModels.Responses;

public class GetCartResponse
{
    public List<Product> Products { get; set; } = new List<Product>();
    public string Message { get; set; }
}