using Domain;

namespace ApiModels.Requests;

public class AddPurchaseRequest
{
    public List<PurchaseProductRequest> Cart { get; set; }
    public string PaymentMethod { get; set; }
}