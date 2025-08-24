using AutoMapper;
using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;
using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Utils;

namespace emdz.dgii.recaudo.CrossCutting.Mapper;

public class TaxPayerResolver(IDgiiService service) : IValueResolver<TaxReceipt, TaxReceiptDto, TaxPayerDto>
{
    public TaxPayerDto Resolve(TaxReceipt source, TaxReceiptDto destination, TaxPayerDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayer = Task.Run(async () => await service.GetTaxPayerByIdAsync(source.TaxPayerId)).GetAwaiter().GetResult();

        // CreateMap<TaxPayer, TaxPayerDto>()
        return context.Mapper.Map<TaxPayerDto>(taxPayer);
    }
}

public class TaxPayerTypeResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, TaxPayerTypeDto>
{
    public TaxPayerTypeDto Resolve(TaxPayer source, TaxPayerDto destination, TaxPayerTypeDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayerType = Task.Run(async () => await service.GetTaxPayerTypeByIdAsync(source.TaxPayerTypeId)).GetAwaiter().GetResult();

        // Using CreateMap<TaxPayerType, TaxPayerTypeDto>()
        return context.Mapper.Map<TaxPayerTypeDto>(taxPayerType);
    }
}

public class DocumentTypeForTaxPayerResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, DocumentTypeDto>
{
    public DocumentTypeDto Resolve(TaxPayer source, TaxPayerDto destination, DocumentTypeDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var documentType = Task.Run(async () => await service.GetDocumentTypeByIdAsync(source.DocumentTypeId)).GetAwaiter().GetResult();

        // CreateMap<DocumentType, DocumentTypeDto>()
        return context.Mapper.Map<DocumentTypeDto>(documentType);
    }
}

public class DocumentTypeForNaturalPersonResolver(IDgiiService service) : IValueResolver<NaturalPerson, NaturalPersonDto, DocumentTypeDto>
{
    public DocumentTypeDto Resolve(NaturalPerson source, NaturalPersonDto destination, DocumentTypeDto destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var documentType = Task.Run(async () => await service.GetDocumentTypeByIdAsync(source.DocumentTypeId)).GetAwaiter().GetResult();

        // CreateMap<DocumentType, DocumentTypeDto>()
        return context.Mapper.Map<DocumentTypeDto>(documentType);
    }
}

public class NaturalPersonResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, NaturalPersonDto?>
{
    public NaturalPersonDto? Resolve(TaxPayer source, TaxPayerDto destination, NaturalPersonDto? destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayerType = Task.Run(async () => await service.GetTaxPayerTypeByIdAsync(source.TaxPayerTypeId)).GetAwaiter().GetResult();

        // If taxPayerType is not a [person] return null
        if (!string.Equals(taxPayerType.Code, EnumTaxPayerType.PER.ToString(), StringComparison.OrdinalIgnoreCase)) return null;

        // Executing service method synchronously
        var naturalPerson = Task.Run(async () => await service.GetNaturalPersonByDocumentAsync(source.DocumentTypeId, source.DocumentNumber)).GetAwaiter().GetResult();

        // CreateMap<NaturalPerson, NaturalPersonDto>()
        return context.Mapper.Map<NaturalPersonDto>(naturalPerson);
    }
}

public class LegalEntityResolver(IDgiiService service) : IValueResolver<TaxPayer, TaxPayerDto, LegalEntityDto?>
{
    public LegalEntityDto? Resolve(TaxPayer source, TaxPayerDto destination, LegalEntityDto? destMember, ResolutionContext context)
    {
        // Executing service method synchronously
        var taxPayerType = Task.Run(async () => await service.GetTaxPayerTypeByIdAsync(source.TaxPayerTypeId)).GetAwaiter().GetResult();

        // If taxPayerType is not a [company] return null
        if (!string.Equals(taxPayerType.Code, EnumTaxPayerType.EMP.ToString(), StringComparison.OrdinalIgnoreCase)) return null;

        // Executing service method synchronously
        var legalEntity = Task.Run(async () => await service.GetLegalEntityByRncAsync(source.DocumentNumber)).GetAwaiter().GetResult();

        // CreateMap<LegalEntity, LegalEntityDto>()
        return context.Mapper.Map<LegalEntityDto>(legalEntity);
    }
}