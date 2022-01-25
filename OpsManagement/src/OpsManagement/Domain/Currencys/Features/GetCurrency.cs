namespace OpsManagement.Domain.Currencys.Features;

using OpsManagement.Dtos.Currency;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetCurrency
{
    public class CurrencyQuery : IRequest<CurrencyDto>
    {
        public Guid Id { get; set; }

        public CurrencyQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<CurrencyQuery, CurrencyDto>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CurrencyDto> Handle(CurrencyQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Currencys
                .AsNoTracking()
                .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Currency", request.Id);

            return result;
        }
    }
}