using Domain;

namespace ApiModels.Requests;

public class AddPurchaseRequest
{
    public List<Product> Products { get; set; }
    public User User { get; set; }
    public float TotalPrice { get; set; }
    public float FinalPrice { get; set; }
    public string? PromotionName { get; set; }
    
    public Purchase ToEntity()
    {
        return new Purchase
        {
            Products = Products,
            User = User,
            TotalPrice = TotalPrice,
            FinalPrice = FinalPrice,
            PromotionName = PromotionName
        };
    }
}