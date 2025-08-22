using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Response.Base;
using Newtonsoft.Json;

namespace emdz.dgii.recaudo.Domain.Signatures.Response;

public class TaxpayerResponse
{
    [JsonProperty("data")]
    public IEnumerable<Taxpayer>? Taxpayers { get; set; } = [];

    [JsonProperty("pagination")]
    public required Pagination Pagination { get; set; }
}
