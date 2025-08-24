using AutoMapper;
using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Entities;
using emdz.dgii.recaudo.Domain.Entities;

namespace emdz.dgii.recaudo.CrossCutting.Mapper;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<TaxReceipt, TaxReceiptDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.TaxPayerDto, source => source.MapFrom<TaxPayerResolver>())
            .ForMember(dest => dest.Ncf, source => source.MapFrom(source => source.Ncf))
            .ForMember(dest => dest.Amount, source => source.MapFrom(source => source.Amount))
            .ForMember(dest => dest.ITBIS, source => source.MapFrom(source => source.ITBIS))
            .ForMember(dest => dest.GeneratedAt, source => source.MapFrom(source => source.GeneratedAt));

        CreateMap<TaxPayer, TaxPayerDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.TaxPayerTypeDto, source => source.MapFrom<TaxPayerTypeResolver>())
            .ForMember(dest => dest.DocumentTypeDto, source => source.MapFrom<DocumentTypeForTaxPayerResolver>())
            .ForMember(dest => dest.DocumentNumber, source => source.MapFrom(source => source.DocumentNumber))
            .ForMember(dest => dest.NaturalPersonDto, source => source.MapFrom<NaturalPersonResolver>())
            .ForMember(dest => dest.LegalEntityDto, source => source.MapFrom<LegalEntityResolver>())
            .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IsActive));

        CreateMap<TaxPayerType, TaxPayerTypeDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.Code, source => source.MapFrom(source => source.Code))
            .ForMember(dest => dest.Description, source => source.MapFrom(source => source.Description))
            .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IsActive));

        CreateMap<DocumentType, DocumentTypeDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.Code, source => source.MapFrom(source => source.Code))
            .ForMember(dest => dest.Description, source => source.MapFrom(source => source.Description))
            .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IsActive));

        CreateMap<NaturalPerson, NaturalPersonDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.DocumentTypeDto, source => source.MapFrom<DocumentTypeForNaturalPersonResolver>())
            .ForMember(dest => dest.DocumentNumber, source => source.MapFrom(source => source.DocumentNumber))
            .ForMember(dest => dest.FirstName, source => source.MapFrom(source => source.FirstName))
            .ForMember(dest => dest.MiddleName, source => source.MapFrom(source => source.MiddleName))
            .ForMember(dest => dest.FirstLastName, source => source.MapFrom(source => source.FirstLastName))
            .ForMember(dest => dest.SecondLastName, source => source.MapFrom(source => source.SecondLastName))
            .ForMember(dest => dest.Birthday, source => source.MapFrom(source => source.Birthday))
            .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IsActive));

        CreateMap<LegalEntity, LegalEntityDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.Rnc, source => source.MapFrom(source => source.Rnc))
            .ForMember(dest => dest.Name, source => source.MapFrom(source => source.Name))
            .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IsActive));
    }
}
