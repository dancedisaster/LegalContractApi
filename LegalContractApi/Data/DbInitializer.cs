using LegalContractApi.Models;

namespace LegalContractApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            // Ensure the database is created
            context.Database.EnsureCreated();
            // Check if there are any contracts already in the database
            if (context.LegalContracts.Any())
            {
                return; // DB has been seeded
            }
            // Seed initial data
            var contracts = new List<LegalContract>
            {
                new LegalContract
                {
                    Id = Guid.NewGuid(),
                    AuthorName = "John Doe",
                    LegalEntityName = "Example Corp",
                    Description = "Initial contract description",
                    CreatedAt = DateTime.UtcNow

                }
            };
            context.LegalContracts.AddRange(contracts);
            context.SaveChanges();
        }

    }
}
