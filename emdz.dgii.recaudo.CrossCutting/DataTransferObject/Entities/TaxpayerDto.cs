namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class TaxPayerDto
{
    public required int Id { get; set; }

    public required TaxPayerTypeDto TaxPayerType { get; set; }

    public required DocumentTypeDto DocumentType { get; set; }

    public required string DocumentNumber { get; set; }

    public required bool IsActive { get; set; }
}
