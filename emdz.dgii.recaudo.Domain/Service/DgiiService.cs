using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Interfaces.Repository;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Service;

public class DgiiService(IDgiiRepository repository) : IDgiiService
{
    public async Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request) => await repository.GetTaxReceiptsAsync(request);

    public async Task<TaxPayerResponse> GetTaxPayers(TaxPayerRequest request) => await repository.GetTaxPayersAsync(request);

    public async Task<TaxPayer> GetTaxPayerById(int id) => await repository.GetTaxPayerByIdAsync(id);

    public async Task<TaxPayerType> GetTaxPayerTypeById(int id) => await repository.GetTaxPayerTypeByIdAsync(id);

    public async Task<DocumentType> GetDocumentTypeByIdAsync(int id) => await repository.GetDocumentTypeByIdAsync(id);    

    public async Task<LegalEntity> GetLegalEntityByIdAsync(int id) => await repository.GetLegalEntityByIdAsync(id);

    public async Task<NaturalPerson> GetNaturalPersonById(int id) => await repository.GetNaturalPersonByIdAsync(id);
}
