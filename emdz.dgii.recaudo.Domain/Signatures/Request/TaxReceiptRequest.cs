namespace emdz.dgii.recaudo.Domain.Signatures.Request;

public class TaxReceiptRequest
{
    public int? TaxPayerId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? PageNumber { get; set; }

    public int? Limit { get; set; }
}
