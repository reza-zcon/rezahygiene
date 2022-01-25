namespace OpsManagement.Domain.Currencys.Features;

using OpsManagement.Domain.Currencys;
using OpsManagement.Dtos.Currency;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using OpsManagement.Domain.Currencys.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddCurrency
{
    public class AddCurrencyCommand : IRequest<CurrencyDto>
    {
        public CurrencyForCreationDto CurrencyToAdd { get; set; }

        public AddCurrencyCommand(CurrencyForCreationDto currencyToAdd)
        {
            CurrencyToAdd = currencyToAdd;
        }
    }

    public class Handler : IRequestHandler<AddCurrencyCommand, CurrencyDto>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CurrencyDto> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = _mapper.Map<Currency> (request.CurrencyToAdd);
            _db.Currencys.Add(currency);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Currencys
                .AsNoTracking()
                .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == currency.Id, cancellationToken);
        }
    }
}