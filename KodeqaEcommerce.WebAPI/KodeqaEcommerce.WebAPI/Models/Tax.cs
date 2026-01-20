namespace KodeqaEcommerce.WebAPI.Models
{
    public class Tax
    {
        public string Country { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
