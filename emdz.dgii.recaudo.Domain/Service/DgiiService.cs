using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Interfaces.Repository;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.Domain.Service;

public class DgiiService(IDgiiRepository repository) : IDgiiService
{
    public async Task<DocumentType> GetDocumentTypeByIdAsync(int id) => await repository.GetDocumentTypeByIdAsync(id);    

    public async Task<LegalEntity> GetLegalEntityByIdAsync(int id) => await repository.GetLegalEntityByIdAsync(id);

    public async Task<NaturalPerson> GetNaturalPersonById(int id) => await repository.GetNaturalPersonById(id);

    public async Task<Taxpayer> GetTaxpayerById(int id) => await repository.GetTaxpayerById(id);

    public async Task<TaxReceiptResponse> GetTaxpayers(TaxpayerRequest request) => await repository.GetTaxpayers(request);

    public async Task<TaxpayerType> GetTaxpayerTypeById(int id) => await repository.GetTaxpayerTypeById(id);

    public async Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request) => await repository.GetTaxReceipts(request);
}
