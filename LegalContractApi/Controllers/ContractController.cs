using LegalContractApi.Models;
using LegalContractApi.DTOs;
using LegalContractApi.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegalContractApi.Controllers
{
    /// <summary>
    /// Contract
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        #region [ Attributes ]
        private readonly ContractService _contractService;
        private readonly ILogger<ContractController> _logger;
        #endregion [ Attributes ]



        public ContractController(ContractService contractService, ILogger<ContractController> logger)
        {
            _contractService = contractService;
            _logger = logger;
        }

        #region [ Get's ]
 
        /// <summary>
        /// Retrieves all legal contracts with pagination support.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllContracts(int pageNumber = 1, int pageSize = 10)
        {
            var contracts = await _contractService.GetAll(pageNumber, pageSize);
            return Ok(contracts);
        }

        /// <summary>
        /// Retrieves a legal contract by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            var contract = await _contractService.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(contract);
        }


        /// <summary>
        /// Retrieves all legal contracts without pagination.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ContractResponseDto>>> GetAllWithoutPaging()
        {
            var contracts = await _contractService.GetAllAsync();
            return Ok(contracts);
        }


        #endregion[ Get's ]



        #region [ Post's ]
        /// <summary>
        /// Creates a new legal contract.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContract(ContractCreateDto contract)
        {
            if (contract == null)
            {
                return BadRequest("Invalid contract data.");
            }

            var result = await _contractService.CreateAsync(contract);

            if (result == null)
            {
                return BadRequest("Failed to create contract.");
            }

            return CreatedAtAction(nameof(GetContractById), new { id = result.Id }, result);
        }


        #endregion [ Post's ]


        #region [ Put's ]
        /// <summary>
        /// Updates an existing legal contract by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(Guid id, [FromBody] ContractUpdateDto contract)
        {
            if (contract == null)
            {
                return BadRequest("Invalid contract data.");
            }
            var existingContract = await _contractService.GetByIdAsync(id);
            if (existingContract == null)
            {
                return NotFound();
            }

            var updated = await _contractService.UpdateAsync(id, contract);
            if (!updated)
            {
                return BadRequest("Failed to update contract.");
            }
            return NoContent();
        }

        #endregion [ Put's ]


        #region [ Delete's ]
        /// <summary>
        /// Deletes a legal contract by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            await _contractService.DeleteAsync(id);
            return NoContent();
        }

        #endregion [ Delete's ]
    }
}
