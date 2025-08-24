using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Response.Base;
using Newtonsoft.Json;

namespace emdz.dgii.recaudo.Domain.Signatures.Response;

public class TaxPayerResponse
{
    [JsonProperty("data")]
    public IEnumerable<TaxPayer>? TaxPayers { get; set; } = [];

    [JsonProperty("pagination")]
    public required Pagination Pagination { get; set; }
}
