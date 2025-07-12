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
        /// Get a paginated list of legal contracts.
        /// </summary>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        Task<IEnumerable<LegalContract>> GetAll(int pageNumber, int pageSize);

        Task<LegalContract?> GetByIdAsync(Guid id);

        Task AddAsync(LegalContract contract);

        Task UpdateAsync(LegalContract existing);

        Task DeleteAsync(Guid id);
    }
}
