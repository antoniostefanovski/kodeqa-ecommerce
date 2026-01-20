using KodeqaEcommerce.WebAPI.Models;

namespace KodeqaEcommerce.WebAPI.Services.Interface
{
    public interface IPricingService
    {
        PricingResponse CalculatePrice(OrderRequest orderRequest);
    }
}
