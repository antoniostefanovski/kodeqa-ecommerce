using KodeqaEcommerce.WebAPI.Models;

namespace KodeqaEcommerce.WebAPI.DataContext.Interface
{
    public interface IProductRepository
    {
        Product Get(string productId);
    }
}
