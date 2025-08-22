namespace emdz.dgii.recaudo.Domain.Signatures.Request;

public class TaxpayerRequest
{
    public int? TaxpayerTypeId { get; set; }

    public int? PageNumber { get; set; }

    public int? Limit { get; set; }
}
