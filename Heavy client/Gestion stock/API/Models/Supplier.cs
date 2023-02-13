using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class Supplier
{
    [JsonPropertyName("idSupplier")]
    public int IDSupplier { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;
    [JsonPropertyName("town")]
    public string Town { get; set; } = string.Empty;
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}