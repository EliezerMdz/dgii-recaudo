namespace emdz.dgii.recaudo.Domain.Entities;

public class TaxReceipt
{
    public int Id { get; set; }

    public required int TaxpayerId { get; set; }

    public required string Ncf { get; set; }

    public required decimal Amount { get; set; }

    public required decimal ITBIS { get; set; }

    public required DateTime GeneratedAt { get; set; }
}
