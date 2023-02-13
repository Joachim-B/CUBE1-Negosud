using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;


public class ClientCommand
{
    [JsonPropertyName("idClientCommand")]
    public int IDClientCommand { get; set; }

    [JsonPropertyName("commandDate")]
    public DateTime CommandDate { get; set; }

    [JsonPropertyName("totalCostTTC")]
    public decimal TotalCostTTC { get; set; }

    [JsonPropertyName("totalCostHT")]
    public decimal TotalCostHT { get; set; }

    [JsonPropertyName("idClient")]
    public int IDClient { get; set; }

    [JsonPropertyName("idCommandStatus")]
    public int IDCommandStatus { get; set; }

    [JsonPropertyName("client")]
    public Client Client { get; set; } = new();

    [JsonPropertyName("clientCommandList")]
    public List<ClientCommandList> ClientCommandList { get; set; } = new();

    [JsonPropertyName("commandStatus")]
    public CommandStatus CommandStatus { get; set; } = new();
}