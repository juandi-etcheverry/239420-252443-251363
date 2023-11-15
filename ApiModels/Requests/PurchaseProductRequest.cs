
namespace ApiModels.Requests
{
    public class PurchaseProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
