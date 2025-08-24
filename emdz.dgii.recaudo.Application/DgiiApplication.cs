using emdz.dgii.recaudo.Domain.Interfaces.Application;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Application;

public class DgiiApplication(IDgiiService service) : IDgiiApplication
{
    public async Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request) => await service.GetTaxReceipts(request);

    public async Task<TaxpayerResponse> GetTaxpayers(TaxpayerRequest request) => await service.GetTaxpayers(request);
}
