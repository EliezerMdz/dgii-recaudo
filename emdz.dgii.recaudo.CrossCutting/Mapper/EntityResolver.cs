using AutoMapper;
using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;
using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Interfaces.Service;

namespace emdz.dgii.recaudo.CrossCutting.Mapper;

public class TaxPayerResolver(IDgiiService service) : IValueResolver<TaxReceipt, TaxReceiptDto, TaxPayerDto>
{
    public TaxPayerDto Resolve(TaxReceipt source, TaxReceiptDto destination, TaxPayerDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayer = Task.Run(async () => await service.GetTaxPayerById(source.TaxPayerId)).GetAwaiter().GetResult();

        // CreateMap<TaxPayer, TaxPayerDto>()
        return context.Mapper.Map<TaxPayerDto>(taxPayer);
    }
}

public class TaxPayerTypeResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, TaxPayerTypeDto>
{
    public TaxPayerTypeDto Resolve(TaxPayer source, TaxPayerDto destination, TaxPayerTypeDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayerType = Task.Run(async () => await service.GetTaxPayerTypeById(source.TaxPayerTypeId)).GetAwaiter().GetResult();

        // Using CreateMap<TaxPayerType, TaxPayerTypeDto>()
        return context.Mapper.Map<TaxPayerTypeDto>(taxPayerType);
    }
}

public class DocumentTypeResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, DocumentTypeDto>
{
    public DocumentTypeDto Resolve(TaxPayer source, TaxPayerDto destination, DocumentTypeDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var documentType = Task.Run(async () => await service.GetDocumentTypeByIdAsync(source.TaxPayerTypeId)).GetAwaiter().GetResult();

        // CreateMap<DocumentType, DocumentTypeDto>()
        return context.Mapper.Map<DocumentTypeDto>(documentType);
    }
}