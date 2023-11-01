namespace ApiModels.Requests;

public class GetProductsRequest
{
    public string? Brand { get; set; } = "";
    public string? Category { get; set; } = "";
    public string? Text { get; set; } = "";
    public float? MinPrice { get; set; } = 0;
    public float? MaxPrice { get; set; } = float.MaxValue;
}