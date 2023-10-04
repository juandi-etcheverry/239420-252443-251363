using Domain;

namespace ApiModels.Responses;

public class ManyPurchasesResponse 
{
    public List<PurchaseDTO> Purchases { get; set; }
}