using System.ComponentModel.DataAnnotations;

namespace KodeqaEcommerce.WebAPI.Models
{
    public class OrderRequest
    {
        [Required]
        public string ProductId { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
        [Required]
        [RegularExpression("MK|DE|FR|USA", ErrorMessage = "Unsupported country.")]
        public string Country { get; set; } = string.Empty; // "MK", "DE", "FR", "USA"
    }
}
