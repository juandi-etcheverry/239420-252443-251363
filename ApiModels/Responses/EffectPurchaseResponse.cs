using Domain;

namespace ApiModels.Responses;

public class EffectPurchaseResponse
{
    public PurchaseDTO Purchase { get; set; }
    public string Message { get; set; }
}