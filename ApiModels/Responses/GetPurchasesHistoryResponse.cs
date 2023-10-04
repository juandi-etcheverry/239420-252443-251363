using Domain;
namespace ApiModels.Responses;

public class GetPurchasesHistoryResponse
{
    public List<PurchaseDTO> Purchases { get; set; } = new List<PurchaseDTO>();
    public string Message { get; set; }
}