using Domain;

namespace ApiModels.Responses.Purchases;

public class PurchaseDTO
{
    public List<PurchaseDTOProductPurchase> Products { get; set; } = new();
    public string UserEmail { get; set; }
    public float TotalPrice { get; set; }
    public float FinalPrice { get; set; }
    public string? PromotionName { get; set; }
    public string PaymentMethod { get; set; }

    public static PurchaseDTO ToPurchaseDTO(Purchase purchase)
    {
        var purchaseDTO = new PurchaseDTO();
        purchaseDTO.UserEmail = purchase.User.Email;
        purchaseDTO.TotalPrice = purchase.TotalPrice;
        purchaseDTO.FinalPrice = purchase.FinalPrice;
        purchaseDTO.PromotionName = purchase.PromotionName;
        purchaseDTO.Products = purchase.Products.Select(p => 
            new PurchaseDTOProductPurchase(){ProductName = p.Product.Name, Quantity = p.Quantity})
            .ToList();
        purchaseDTO.PaymentMethod = purchase.PaymentMethod;
        return purchaseDTO;
    }
}

public class PurchaseDTOProductPurchase
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}