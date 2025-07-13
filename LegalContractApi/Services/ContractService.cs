
using LegalContractApi.DTOs;
using LegalContractApi.Models;
using LegalContractApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LegalContractApi.Services
{
    public class ContractService
    {
        private readonly IContractRepository _repository;
        private readonly ILogger<ContractService> _logger;

        public ContractService(IContractRepository repository, ILogger<ContractService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ContractResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("ContractService ### Retrieving all contracts");

            var contracts = await _repository.GetAllAsync();
            return contracts.Select(MapToDto);
        }

        public async Task<ContractResponseDto?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving contract with ID: {Id}", id);


            var contract = await _repository.GetByIdAsync(id);
            return contract == null ? null : MapToDto(contract);
        }

        public async Task<ContractPaginatedResponseDto> GetAllAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation("Retrieving all contracts with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
            var data = await _repository.GetAllAsync(pageNumber, pageSize);

            return new ContractPaginatedResponseDto
            {
                Total = data.TotalCount,
                Items = data.Items.Select(MapToDto)
            };
        }

        //Crate a new contract
        public async Task<ContractResponseDto> CreateAsync(ContractCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Attempted to create a contract with null data.");
                throw new ArgumentNullException(nameof(dto), "Contract cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(dto.LegalEntityName))
            {
                _logger.LogError("Legal entity name is required for contract creation.");
                throw new ArgumentException("Legal entity name cannot be empty.", nameof(dto.LegalEntityName));
            }

            _logger.LogInformation("Creating a new contract for legal entity: {LegalEntity}", dto.LegalEntityName);

            var contract = new LegalContract
            {
                Id = Guid.NewGuid(),
                AuthorName = dto.AuthorName,
                LegalEntityName = dto.LegalEntityName,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };


            await _repository.AddAsync(contract);
            return MapToDto(contract);
        }

        // Update an existing contract
        [HttpPut("{id}")]
        public async Task<bool> UpdateAsync(Guid id, ContractUpdateDto dto)
        {
            _logger.LogInformation("Updating contract with ID: {Id}", id);
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Contract with ID {Id} not found for update", id);
                return false;
            }

            existing.AuthorName = dto.AuthorName;
            existing.LegalEntityName = dto.LegalEntityName;
            existing.Description = dto.Description;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting contract with ID: {Id}", id);
            var contract = await _repository.GetByIdAsync(id);
            if (contract == null)
            {
                _logger.LogWarning("Contract with ID {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }



        #region Private Methods

        private static ContractResponseDto MapToDto(LegalContract contract) => new()
        {
            Id = contract.Id,
            AuthorName = contract.AuthorName,
            LegalEntityName = contract.LegalEntityName,
            Description = contract.Description,
            CreatedAt = contract.CreatedAt,
            UpdatedAt = contract.UpdatedAt
        };

        #endregion Private Methods

    }
}
