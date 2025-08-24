using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Response.Base;
using Newtonsoft.Json;

namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Signatures.Response;

public class TaxReceiptResponseDto
{
    [JsonProperty("data")]
    public IEnumerable<TaxReceiptDto>? TaxReceiptsDto { get; set; } = [];

    [JsonProperty("pagination")]
    public required Pagination Pagination { get; set; }
}
