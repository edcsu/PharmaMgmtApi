using Microsoft.EntityFrameworkCore;

namespace PharmaMgmtApi.DbContexts;

public static class Seed
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}