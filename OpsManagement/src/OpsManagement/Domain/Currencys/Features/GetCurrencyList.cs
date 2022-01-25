namespace OpsManagement.Domain.Currencys.Features;

using OpsManagement.Domain.Currencys;
using OpsManagement.Dtos.Currency;
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

public static class GetCurrencyList
{
    public class CurrencyListQuery : IRequest<PagedList<CurrencyDto>>
    {
        public CurrencyParametersDto QueryParameters { get; set; }

        public CurrencyListQuery(CurrencyParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<CurrencyListQuery, PagedList<CurrencyDto>>
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

        public async Task<PagedList<CurrencyDto>> Handle(CurrencyListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Currencys
                as IQueryable<Currency>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider);

            return await PagedList<CurrencyDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}