using Domain;

namespace ApiModels.Requests;

public class AddPurchaseRequest
{
    public List<PurchaseProduct> Cart { get; set; }
}