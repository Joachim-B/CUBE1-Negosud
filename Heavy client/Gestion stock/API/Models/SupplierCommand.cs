using System.Text.Json.Serialization;

namespace Gestion_stock.API.Models;

public class SupplierCommand
{
    [JsonPropertyName("idSupplierCommand")]
    public int IDSupplierCommand { get; set; }

    [JsonPropertyName("transportCost")]
    public decimal TransportCost { get; set; }

    [JsonPropertyName("commandDate")]
    public DateTime CommandDate { get; set; }

    [JsonPropertyName("totalCost")]
    public decimal TotalCost { get; set; }

    [JsonPropertyName("idSupplier")]
    public int IDSupplier { get; set; }

    [JsonPropertyName("idCommandStatus")]
    public int IDCommandStatus { get; set; }

    [JsonPropertyName("idCommandType")]
    public int IDCommandType { get; set; }

    [JsonPropertyName("supplier")]
    public Supplier Supplier { get; set; } = new();

    [JsonPropertyName("commandStatus")]
    public CommandStatus CommandStatus { get; set; } = new();

    [JsonPropertyName("commandType")]
    public CommandType CommandType { get; set; } = new();

    [JsonPropertyName("supplierCommandList")]
    public List<SupplierCommandList> SupplierCommandList { get; set; } = new();
}