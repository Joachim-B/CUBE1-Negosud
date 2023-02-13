using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class QuantityType
{
    [JsonPropertyName("idQuantityType")]
    public int IDQuantityType { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
