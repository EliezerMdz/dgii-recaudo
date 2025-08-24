using AutoMapper;
using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Signatures.Response;
using emdz.dgii.recaudo.Domain.Signatures.Response;

namespace emdz.dgii.recaudo.CrossCutting.Mapper;

public class SignatureProfile : Profile
{
    public SignatureProfile()
    {
        CreateMap<TaxReceiptResponse, TaxReceiptResponseDto>()
            .ForMember(dest => dest.TaxReceiptsDto, opt => opt.MapFrom(source => source.TaxReceipts))
            .ForMember(dest => dest.Pagination, opt => opt.MapFrom(source => source.Pagination));
    }
}
