using System.ComponentModel.DataAnnotations;

namespace LegalContractApi.DTOs
{
    public class ContractCreateDto
    {
        [Required]
        public string AuthorName { get; set; } = string.Empty;

        [Required]
        public string LegalEntityName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
