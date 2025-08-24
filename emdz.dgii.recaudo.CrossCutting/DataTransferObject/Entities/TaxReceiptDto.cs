using Newtonsoft.Json;

namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class TaxReceiptDto
{
    public int Id { get; set; }

    [JsonProperty("taxPayer")]
    public required TaxPayerDto TaxPayerDto { get; set; }

    public required string Ncf { get; set; }

    public required decimal Amount { get; set; }

    public required decimal ITBIS { get; set; }

    public required DateTime GeneratedAt { get; set; }
}
