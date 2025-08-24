namespace emdz.dgii.recaudo.Domain.Signatures.Request;

public class TaxPayerRequest
{
    public int? TaxPayerTypeId { get; set; }

    public int? PageNumber { get; set; }

    public int? Limit { get; set; }
}
