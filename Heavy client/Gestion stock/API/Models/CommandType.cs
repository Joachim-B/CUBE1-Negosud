using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class CommandType
{
    [JsonPropertyName("idCommandType")]
    public int IDCommandType { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}