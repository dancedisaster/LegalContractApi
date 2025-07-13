using LegalContractApi.DTOs;
using LegalContractApi.Models;

namespace LegalContractApi.Repositories
{
    /// <summary>
    /// Interface for contract services that handle legal contracts.
    /// </summary>
    public interface IContractRepository
    {
        /// <summary>
        /// Get all legal contracts.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LegalContract>> GetAllAsync();

        /// <summary>
        /// Paginated List of Contracts
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(IEnumerable<LegalContract> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);

        Task<LegalContract?> GetByIdAsync(Guid id);

        Task AddAsync(LegalContract contract);

        Task UpdateAsync(LegalContract existing);

        Task DeleteAsync(Guid id);
    }
}
