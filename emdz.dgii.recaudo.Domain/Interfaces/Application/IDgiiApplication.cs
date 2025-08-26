using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Interfaces.Application;

public interface IDgiiApplication
{
    public Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request);

    public Task<TaxReceiptSummaryResponse> GetTaxReceiptsSummaryAsync(TaxReceiptSummaryRequest request);

    public Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request);
}
