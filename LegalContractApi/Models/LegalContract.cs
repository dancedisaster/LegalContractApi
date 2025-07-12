using System.ComponentModel.DataAnnotations;

namespace LegalContractApi.Models
{
    /// <summary>
    /// Represents a legal contract entity.
    /// </summary>
    public class LegalContract
    {
        /// <summary>
        /// A unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the author
        /// </summary>
        [Required]
        public required string AuthorName { get; set; }

        /// <summary>
        /// A legal entity name
        /// </summary>
        [Required]
        public required string LegalEntityName { get; set; }

        /// <summary>
        /// A free-text field which is used to describe the legal entity
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// A free-text field which is used to describe the legal entity
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date when the legal entity was updated (if any)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
