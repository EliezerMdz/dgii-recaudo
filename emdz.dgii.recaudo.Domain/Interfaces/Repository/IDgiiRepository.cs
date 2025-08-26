using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Interfaces.Repository;

public interface IDgiiRepository
{
    public Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request);

    public Task<TaxReceiptSummaryResponse> GetTaxReceiptsSummaryAsync(TaxReceiptSummaryRequest request);

    public Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request);
    
    public Task<TaxPayer> GetTaxPayerByIdAsync(int id);
    
    public Task<TaxPayerType> GetTaxPayerTypeByIdAsync(int id);

    public Task<DocumentType> GetDocumentTypeByIdAsync(int id);

    public Task<NaturalPerson> GetNaturalPersonByDocumentAsync(int documentTypeId, string documentNumber);

    public Task<LegalEntity> GetLegalEntityByRncAsync(string documentNumber);
}
