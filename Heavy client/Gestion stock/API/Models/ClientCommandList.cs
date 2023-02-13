using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class ClientCommandList
{
    [JsonPropertyName("idArticle")]
    public int IDArticle { get; set; }

    [JsonPropertyName("idClientCommand")]
    public int IDClientCommand { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("idQuantityType")]
    public int IDQuantityType { get; set; }

    [JsonPropertyName("article")]
    public Article Article { get; set; } = new();

    [JsonPropertyName("quantityType")]
    public QuantityType QuantityType { get; set; } = new();
}
