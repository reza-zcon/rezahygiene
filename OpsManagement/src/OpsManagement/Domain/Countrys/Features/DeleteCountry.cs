namespace OpsManagement.Domain.Countrys.Features;

using OpsManagement.Domain.Countrys;
using OpsManagement.Dtos.Country;
using OpsManagement.Exceptions;
using OpsManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteCountry
{
    public class DeleteCountryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteCountryCommand(Guid country)
        {
            Id = country;
        }
    }

    public class Handler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly OpsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OpsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Countrys
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Country", request.Id);

            _db.Countrys.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}