using Domain;

namespace ApiModels.Requests;

public class AddPurchaseRequest
{
    public List<Guid> ProductsIds { get; set; }
}