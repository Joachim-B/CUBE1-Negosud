using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class SupplierCommandList
{
    [JsonPropertyName("idArticle")]
    public int IDArticle { get; set; }
    [JsonPropertyName("idSupplierCommand")]
    public int IDSupplierCommand { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("article")]
    public Article Article { get; set; } = new();
    [JsonPropertyName("supplierCommand")]
    public SupplierCommand SupplierCommand { get; set; } = new();
}