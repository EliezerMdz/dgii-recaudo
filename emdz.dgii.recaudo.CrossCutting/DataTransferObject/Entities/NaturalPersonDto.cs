using Newtonsoft.Json;

namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class NaturalPersonDto
{
    public required int Id { get; set; }

    [JsonProperty("documentType")]
    public required DocumentTypeDto DocumentTypeDto { get; set; }

    public required string DocumentNumber { get; set; }

    public required string FirstName { get; set; }

    public required string MiddleName { get; set; }

    public required string FirstLastName { get; set; }

    public required string SecondLastName { get; set; }

    public required DateTime Birthday { get; set; }

    public required bool IsActive { get; set; }
}
