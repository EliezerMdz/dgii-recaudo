namespace emdz.dgii.recaudo.Domain.Entities;

public class Taxpayer
{
    public required int Id { get; set; }

    public required int TaxpayerTypeId { get; set; }

    public required int DocumentTypeId { get; set; }

    public required string DocumentNumber { get; set; }

    public required bool IsActive { get; set; }
}
