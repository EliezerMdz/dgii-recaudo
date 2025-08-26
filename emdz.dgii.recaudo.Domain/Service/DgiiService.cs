using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Interfaces.Repository;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Service;

public class DgiiService(IDgiiRepository repository) : IDgiiService
{
    public async Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request) => await repository.GetTaxReceiptsAsync(request);

    public async Task<TaxReceiptSummaryResponse> GetTaxReceiptsSummaryAsync(TaxReceiptSummaryRequest request) => await repository.GetTaxReceiptsSummaryAsync(request);

    public async Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request) => await repository.GetTaxPayersAsync(request);

    public async Task<TaxPayer> GetTaxPayerByIdAsync(int id) => await repository.GetTaxPayerByIdAsync(id);

    public async Task<TaxPayerType> GetTaxPayerTypeByIdAsync(int id) => await repository.GetTaxPayerTypeByIdAsync(id);

    public async Task<DocumentType> GetDocumentTypeByIdAsync(int id) => await repository.GetDocumentTypeByIdAsync(id);    

    public async Task<NaturalPerson> GetNaturalPersonByDocumentAsync(int documentTypeId, string documentNumber)  => await repository.GetNaturalPersonByDocumentAsync(documentTypeId, documentNumber);

    public async Task<LegalEntity> GetLegalEntityByRncAsync(string documentNumber) => await repository.GetLegalEntityByRncAsync(documentNumber);
}
