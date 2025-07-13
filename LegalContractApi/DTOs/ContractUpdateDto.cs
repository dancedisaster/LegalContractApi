namespace LegalContractApi.DTOs
{
    public class ContractUpdateDto
    {

        /// <summary>
        /// AuthorName
        /// </summary>
        public string AuthorName { get; set; } = string.Empty;
        public string LegalEntityName { get; set; }
        /// <summary>
        /// Description of the contract.
        /// </summary>
        public string Description { get; set; } = string.Empty;

    }
}
