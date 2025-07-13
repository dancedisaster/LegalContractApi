namespace LegalContractApi.DTOs
{
    /// <summary>
    /// Paginated Contract Response DTO
    /// </summary>
    public class ContractPaginatedResponseDto
    {
        public long Total { get; set; }

        public IEnumerable<ContractResponseDto> Items { get; set; }
    }

}
