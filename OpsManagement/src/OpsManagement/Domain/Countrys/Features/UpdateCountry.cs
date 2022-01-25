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

public static class UpdateCountry
{
    public class UpdateCountryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public CountryForUpdateDto CountryToUpdate { get; set; }

        public UpdateCountryCommand(Guid country, CountryForUpdateDto newCountryData)
        {
            Id = country;
            CountryToUpdate = newCountryData;
        }
    }

    public class Handler : IRequestHandler<UpdateCountryCommand, bool>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToUpdate = await _db.Countrys
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (countryToUpdate == null)
                throw new NotFoundException("Country", request.Id);

            _mapper.Map(request.CountryToUpdate, countryToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}