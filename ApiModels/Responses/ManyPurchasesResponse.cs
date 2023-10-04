using Domain;

namespace ApiModels.Responses;

public class ManyPurchasesResponse 
{
    public List<Purchase> Purchases { get; set; }
}