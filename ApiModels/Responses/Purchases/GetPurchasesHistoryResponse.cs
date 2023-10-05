namespace ApiModels.Responses.Purchases;

public class GetPurchasesHistoryResponse
{
    public List<PurchaseDTO> Purchases { get; set; } = new();
    public string Message { get; set; }
}