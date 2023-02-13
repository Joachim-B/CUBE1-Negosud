using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class CommandStatus
{
    [JsonPropertyName("idCommandStatus")]
    public int IDCommandStatus { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}