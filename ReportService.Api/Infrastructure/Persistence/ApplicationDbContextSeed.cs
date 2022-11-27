
namespace ReportService.Api.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        //Seed, if necessary
        await Task.Delay(1000);
    }
}