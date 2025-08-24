using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Interfaces.Repository;

public interface IDgiiRepository
{
    public Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request);

    public Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request);
    
    public Task<TaxPayerType> GetTaxPayerTypeByIdAsync(int id);

    public Task<DocumentType> GetDocumentTypeByIdAsync(int id);

    public Task<LegalEntity> GetLegalEntityByIdAsync(int id);

    public Task<NaturalPerson> GetNaturalPersonByIdAsync(int id);

    public Task<TaxPayer> GetTaxPayerByIdAsync(int id);

}
