namespace OpsManagement.Domain.Countrys.Features;

using OpsManagement.Domain.Countrys;
using OpsManagement.Dtos.Country;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using OpsManagement.Domain.Countrys.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddCountry
{
    public class AddCountryCommand : IRequest<CountryDto>
    {
        public CountryForCreationDto CountryToAdd { get; set; }

        public AddCountryCommand(CountryForCreationDto countryToAdd)
        {
            CountryToAdd = countryToAdd;
        }
    }

    public class Handler : IRequestHandler<AddCountryCommand, CountryDto>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CountryDto> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<Country> (request.CountryToAdd);
            _db.Countrys.Add(country);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Countrys
                .AsNoTracking()
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == country.Id, cancellationToken);
        }
    }
}