namespace emdz.dgii.recaudo.Domain.Signatures.Request;

public class TaxReceiptSummaryRequest
{
    public int? TaxPayerId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
