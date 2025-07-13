using LegalContractApi.Data;
using LegalContractApi.DTOs;
using LegalContractApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalContractApi.Repositories
{
    public class EfContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EfContractRepository> _logger;

        public EfContractRepository(AppDbContext context, ILogger<EfContractRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<LegalContract>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all contracts");
            return await _context.LegalContracts.AsNoTracking().ToListAsync();
        }

        public async Task<(IEnumerable<LegalContract> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation("Fetching contracts with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);

            var query = _context.LegalContracts.AsQueryable();

            var totalCount = await query.CountAsync();

            var items =  await query
                .OrderBy(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<LegalContract?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching contract with ID: {Id}", id);
            return await _context.LegalContracts.FindAsync(id);
        }

        public async Task AddAsync(LegalContract contract)
        {
            _logger.LogInformation("Adding new contract");
            await _context.LegalContracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LegalContract contract)
        {
            _logger.LogInformation("Updating contract with ID: {Id}", contract.Id);
            _context.LegalContracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting contract with ID: {Id}", id);
            var contract = await _context.LegalContracts.FindAsync(id);
            if (contract != null)
            {
                _context.LegalContracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }
    }

}
