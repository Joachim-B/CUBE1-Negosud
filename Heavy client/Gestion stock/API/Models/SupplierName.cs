using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class SupplierName
{
    [JsonPropertyName("idSupplier")]
    public int IDSupplier { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}