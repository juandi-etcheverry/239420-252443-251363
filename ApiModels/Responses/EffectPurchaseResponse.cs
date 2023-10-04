using Domain;

namespace ApiModels.Responses;

public class EffectPurchaseResponse
{
    public Purchase Purchase { get; set; }
    public string Message { get; set; }
}