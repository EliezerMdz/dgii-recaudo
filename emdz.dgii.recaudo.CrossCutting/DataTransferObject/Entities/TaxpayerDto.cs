using emdz.dgii.recaudo.Domain.Entities;
using Newtonsoft.Json;

namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class TaxPayerDto
{
    public required int Id { get; set; }

    [JsonProperty("taxPayerType")]
    public required TaxPayerTypeDto TaxPayerTypeDto { get; set; }

    [JsonProperty("documentType")]
    public required DocumentTypeDto DocumentTypeDto { get; set; }

    [JsonProperty("naturalPerson")]
    public NaturalPersonDto? NaturalPersonDto { get; set; }

    [JsonProperty("legalEntity")]
    public LegalEntityDto? LegalEntityDto { get; set; }

    public required string DocumentNumber { get; set; }

    public required bool IsActive { get; set; }
}
