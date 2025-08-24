namespace emdz.dgii.recaudo.Domain.Entities;

public class TaxPayer
{
    public required int Id { get; set; }

    public required int TaxPayerTypeId { get; set; }

    public required int DocumentTypeId { get; set; }

    public required string DocumentNumber { get; set; }

    public required bool IsActive { get; set; }
}
