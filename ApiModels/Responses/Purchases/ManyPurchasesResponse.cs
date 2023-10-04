using Domain;

namespace ApiModels.Responses.Purchases;

public class ManyPurchasesResponse
{
    public List<PurchaseDTO> Purchases { get; set; }
}