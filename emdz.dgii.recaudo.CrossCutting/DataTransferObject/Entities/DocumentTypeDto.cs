namespace emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;

public class DocumentTypeDto
{
    public required int Id { get; set; }

    public required string Code { get; set; }

    public required string Description { get; set; }

    public required bool IsActive { get; set; }
}
