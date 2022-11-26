using ContactService.Api.Core.Entities;

namespace ContactService.Api.Infrastrcuture.Persistence;

public static class ApplicationDbContextSeed
{
    public async static Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        
        //Seed, if necessary
        if (!context.ContactItemTypes.Any())
        {
            context.ContactItemTypes.AddRange(new List<ContactItemType>
            {
                new ContactItemType()
                {
                    Type = "Telephone",
                    Created = DateTime.UtcNow,
                    CreatedBy = 111,
                    IsValid = 1
                },
                new ContactItemType()
                {
                    Type = "E-mail",
                    Created = DateTime.UtcNow,
                    CreatedBy = 111,
                    IsValid = 1
                },
                new ContactItemType()
                {
                    Type = "Location",
                    Created = DateTime.UtcNow,
                    CreatedBy = 111,
                    IsValid = 1
                }
            });
        
            await context.SaveChangesAsync();
        }
    }
}