using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class Article
{
    [JsonPropertyName("idArticle")]
    public int IDArticle { get; set; }

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("wineYear")]
    public int WineYear { get; set; }

    [JsonPropertyName("boxMinQuantity")]
    public int BoxMinQuantity { get; set; }

    [JsonPropertyName("boxBuyingPrice")]
    public decimal BoxBuyingPrice { get; set; }

    [JsonPropertyName("unitPriceTTC")]
    public decimal UnitPriceTTC { get; set; }

    [JsonPropertyName("boxPriceTTC")]
    public decimal BoxPriceTTC { get; set; }

    [JsonPropertyName("tva")]
    public decimal TVA { get; set; }

    [JsonPropertyName("boxOptimalQuantity")]
    public int BoxOptimalQuantity { get; set; }

    [JsonPropertyName("boxStockQuantity")]
    public int BoxStockQuantity { get; set; }

    [JsonPropertyName("unitStockQuantity")]
    public int UnitStockQuantity { get; set; }

    [JsonPropertyName("bottleQuantityPerBox")]
    public int BottleQuantityPerBox { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("imageLink")]
    public string? ImageLink { get; set; }

    [JsonPropertyName("idSupplier")]
    public int IDSupplier { get; set; }

    [JsonPropertyName("supplier")]
    public Supplier Supplier { get; set; } = new();

    [JsonPropertyName("idWineFamily")]
    public int IDWineFamily { get; set; }

    [JsonPropertyName("wineFamily")]
    public WineFamily WineFamily { get; set; } = new();
}