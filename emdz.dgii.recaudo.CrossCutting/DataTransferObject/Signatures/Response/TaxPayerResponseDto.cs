using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Response.Base;
using Newtonsoft.Json;

namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Signatures.Response;

public class TaxPayerResponseDto
{
    [JsonProperty("data")]
    public IEnumerable<TaxPayerDto>? TaxPayersDto { get; set; } = [];

    [JsonProperty("pagination")]
    public required Pagination Pagination { get; set; }
}
