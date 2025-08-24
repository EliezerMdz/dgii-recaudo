namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class LegalEntityDto
{
    public required int Id { get; set; }

    public required string Rnc { get; set; }

    public required string Name { get; set; }

    public required bool IsActive { get; set; }
}
