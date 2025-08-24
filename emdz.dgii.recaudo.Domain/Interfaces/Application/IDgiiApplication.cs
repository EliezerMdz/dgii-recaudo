using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Interfaces.Application;

public interface IDgiiApplication
{
    public Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request);

    public Task<TaxpayerResponse> GetTaxpayers(TaxpayerRequest request);
}
