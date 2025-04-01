namespace Odoo.Persistence;

public partial class OdooDbContext
{
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidDataException("This context is read-only.");
    }
}
