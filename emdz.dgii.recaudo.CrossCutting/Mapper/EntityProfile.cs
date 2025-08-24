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
            .ForMember(dest => dest.TaxPayer, source => source.MapFrom<TaxPayerResolver>())
            .ForMember(dest => dest.Ncf, source => source.MapFrom(source => source.Ncf))
            .ForMember(dest => dest.Amount, source => source.MapFrom(source => source.Amount))
            .ForMember(dest => dest.ITBIS, source => source.MapFrom(source => source.ITBIS))
            .ForMember(dest => dest.GeneratedAt, source => source.MapFrom(source => source.GeneratedAt));

        CreateMap<TaxPayer, TaxPayerDto>()
            .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
            .ForMember(dest => dest.TaxPayerType, source => source.MapFrom<TaxPayerTypeResolver>())
            .ForMember(dest => dest.DocumentType, source => source.MapFrom<DocumentTypeResolver>())
            .ForMember(dest => dest.DocumentNumber, source => source.MapFrom(source => source.DocumentNumber))
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
    }
}
