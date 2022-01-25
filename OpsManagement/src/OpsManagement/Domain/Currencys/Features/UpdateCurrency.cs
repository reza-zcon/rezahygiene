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

public static class UpdateCurrency
{
    public class UpdateCurrencyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public CurrencyForUpdateDto CurrencyToUpdate { get; set; }

        public UpdateCurrencyCommand(Guid currency, CurrencyForUpdateDto newCurrencyData)
        {
            Id = currency;
            CurrencyToUpdate = newCurrencyData;
        }
    }

    public class Handler : IRequestHandler<UpdateCurrencyCommand, bool>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currencyToUpdate = await _db.Currencys
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (currencyToUpdate == null)
                throw new NotFoundException("Currency", request.Id);

            _mapper.Map(request.CurrencyToUpdate, currencyToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}