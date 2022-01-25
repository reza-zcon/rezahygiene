namespace OpsManagement.Domain.Countrys.Mappings;

using OpsManagement.Dtos.Country;
using AutoMapper;
using OpsManagement.Domain.Countrys;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        //createmap<to this, from this>
        CreateMap<Country, CountryDto>()
            .ReverseMap();
        CreateMap<CountryForCreationDto, Country>();
        CreateMap<CountryForUpdateDto, Country>()
            .ReverseMap();
    }
}