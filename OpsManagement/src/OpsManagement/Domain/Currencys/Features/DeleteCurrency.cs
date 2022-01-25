namespace OpsManagement.Domain.Currencys.Features;

using OpsManagement.Domain.Currencys;
using OpsManagement.Dtos.Currency;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteCurrencyCommand(Guid currency)
        {
            Id = currency;
        }
    }

    public class Handler : IRequestHandler<DeleteCurrencyCommand, bool>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Currencys
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Currency", request.Id);

            _db.Currencys.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}