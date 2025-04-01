using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class ApplyMigrations
{
    public static void RunMigrations<TContext>(this IServiceProvider serviceProvider) where TContext : DbContext
    {
        Console.WriteLine("Applying migrations...");
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied.");
    }
}