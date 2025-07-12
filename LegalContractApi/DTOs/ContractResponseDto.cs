namespace LegalContractApi.DTOs
{
    public class ContractResponseDto
    {
        /// <summary>
        /// Unique identifier for the contract.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// AuthorName
        /// </summary>
        public string AuthorName { get; set; } = string.Empty;


        public string LegalEntity { get; set; }

        /// <summary>
        /// Description of the contract.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Date when the contract was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Date when the contract was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
