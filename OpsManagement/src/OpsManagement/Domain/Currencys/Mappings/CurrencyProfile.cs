namespace OpsManagement.Domain.Currencys.Mappings;

using OpsManagement.Dtos.Currency;
using AutoMapper;
using OpsManagement.Domain.Currencys;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        //createmap<to this, from this>
        CreateMap<Currency, CurrencyDto>()
            .ReverseMap();
        CreateMap<CurrencyForCreationDto, Currency>();
        CreateMap<CurrencyForUpdateDto, Currency>()
            .ReverseMap();
    }
}