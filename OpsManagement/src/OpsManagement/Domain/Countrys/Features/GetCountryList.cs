namespace OpsManagement.Domain.Countrys.Features;

using OpsManagement.Domain.Countrys;
using OpsManagement.Dtos.Country;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using OpsManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetCountryList
{
    public class CountryListQuery : IRequest<PagedList<CountryDto>>
    {
        public CountryParametersDto QueryParameters { get; set; }

        public CountryListQuery(CountryParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<CountryListQuery, PagedList<CountryDto>>
    {
        private readonly OpsDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<CountryDto>> Handle(CountryListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Countrys
                as IQueryable<Country>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider);

            return await PagedList<CountryDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}