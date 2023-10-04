using Domain;
namespace ApiModels.Responses;

public class GetPurchasesHistoryResponse
{
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
    public string Message { get; set; }
}