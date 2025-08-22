namespace emdz.dgii.recaudo.Domain.Entities;

public class NaturalPerson
{
    public required int Id { get; set; }

    public required string FirstName { get; set; }

    public required string MiddleName { get; set; }

    public required string FirstLastName { get; set; }

    public required string SecondLastName { get; set; }

    public required DateTime Birthday { get; set; }

    public required bool IsActive { get; set; }
}
