using Newtonsoft.Json;

namespace emdz.dgii.recaudo.Domain.Signatures.Response;

public class TaxReceiptSummaryResponse
{
    [JsonProperty("taxPayerId")]
    public int TaxPayerId { get; set; }

    [JsonProperty("totalRecords")]
    public int TotalRecords { get; set; }

    [JsonProperty("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonProperty("totalITBIS")]
    public decimal TotalITBIS { get; set; }
}
