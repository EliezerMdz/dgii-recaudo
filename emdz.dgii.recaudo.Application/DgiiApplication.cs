using emdz.dgii.recaudo.Domain.Interfaces.Application;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Application;

public class DgiiApplication(IDgiiService service) : IDgiiApplication
{
    public async Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request) => await service.GetTaxReceiptsAsync(request);

    public async Task<TaxReceiptSummaryResponse> GetTaxReceiptsSummaryAsync(TaxReceiptSummaryRequest request) => await service.GetTaxReceiptsSummaryAsync(request);

    public async Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request) => await service.GetTaxPayersAsync(request);
}
