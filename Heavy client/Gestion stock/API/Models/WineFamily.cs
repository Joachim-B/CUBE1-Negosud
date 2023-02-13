using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class WineFamily
{
    [JsonPropertyName("idWineFamily")]
    public int IDWineFamily { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
