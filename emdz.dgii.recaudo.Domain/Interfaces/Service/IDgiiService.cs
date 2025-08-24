using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Interfaces.Service;

public interface IDgiiService
{
    public Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request);

    public Task<TaxPayerResponse> GetTaxPayers(TaxPayerRequest request);

    public Task<DocumentType> GetDocumentTypeByIdAsync(int id);

    public Task<LegalEntity> GetLegalEntityByIdAsync(int id);

    public Task<NaturalPerson> GetNaturalPersonById(int id);

    public Task<TaxPayer> GetTaxPayerById(int id);

    public Task<TaxPayerType> GetTaxPayerTypeById(int id);
}
