using Domain;
namespace ApiModels.Responses;

public class GetPurchasesHistoryResponse
{
    public List<PurchaseDTO> Purchases { get; set; } = new List<PurchaseDTO>();
    public string Message { get; set; }

    public static PurchaseDTO ToPurchaseDTO(Purchase purchase)
    {
        var purchaseDTO = new PurchaseDTO();
        purchaseDTO.UserEmail = purchase.User.Email;
        purchaseDTO.TotalPrice = purchase.TotalPrice;
        purchaseDTO.FinalPrice = purchase.FinalPrice;
        purchaseDTO.PromotionName = purchase.PromotionName;
        purchaseDTO.ProductsNames = purchase.Products.Select(p => p.Name).ToList();
        return purchaseDTO;
    }
}

public class PurchaseDTO
{
    public List<string> ProductsNames { get; set; } = new List<string>();
    public string UserEmail { get; set; }
    public float TotalPrice { get; set; }
    public float FinalPrice { get; set; }
    public string? PromotionName { get; set; }
}