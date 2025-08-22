namespace emdz.dgii.recaudo.Domain.Entities;

public class TaxpayerType
{
    public required int Id { get; set; }

    public required string Code { get; set; }

    public required string Description { get; set; }

    public required bool IsActive { get; set; }
}
