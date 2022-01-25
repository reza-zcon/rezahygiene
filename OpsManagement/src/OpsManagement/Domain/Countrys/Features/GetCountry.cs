namespace OpsManagement.Domain.Countrys.Features;

using OpsManagement.Dtos.Country;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetCountry
{
    public class CountryQuery : IRequest<CountryDto>
    {
        public Guid Id { get; set; }

        public CountryQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<CountryQuery, CountryDto>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CountryDto> Handle(CountryQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Countrys
                .AsNoTracking()
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Country", request.Id);

            return result;
        }
    }
}