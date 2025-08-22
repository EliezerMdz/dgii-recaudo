namespace emdz.dgii.recaudo.Domain.Entities;

public class LegalEntity
{
    public required int Id { get; set; }

    public required string Rnc { get; set; }

    public required string Name { get; set; }

    public required bool IsActive { get; set; }
}
