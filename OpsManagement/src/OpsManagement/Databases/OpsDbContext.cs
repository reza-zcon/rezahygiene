namespace OpsManagement.Databases;


using OpsManagement.Domain.Countrys;
using OpsManagement.Domain.Currencys;
using OpsManagement.Domain;
using OpsManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

public class OpsDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public OpsDbContext(
        DbContextOptions<OpsDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
        Database.EnsureCreated();
    }


    #region DbSet Region - Do Not Delete

    public DbSet<Country> Countrys { get; set; }
    public DbSet<Currency> Currencys { get; set; }
    #endregion DbSet Region - Do Not Delete

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
        
    private void UpdateAuditFields()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService?.UserId;
                    entry.Entity.CreatedOn = now;
                    entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                    entry.Entity.LastModifiedOn = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                    entry.Entity.LastModifiedOn = now;
                    break;
                
                case EntityState.Deleted:
                    // deleted_at
                    break;
            }
        }
    }
}