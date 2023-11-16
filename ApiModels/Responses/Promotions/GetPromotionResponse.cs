namespace ApiModels.Responses.Promotions;

public class GetPromotionResponse
{
    public String PromotionName  { get; set; }
    public float Discount { get; set; }
    public float FinalPrice { get; set; }
}