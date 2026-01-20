
using KodeqaEcommerce.WebAPI.Configuration;
using KodeqaEcommerce.WebAPI.DataContext.Implementation;
using KodeqaEcommerce.WebAPI.DataContext.Interface;
using KodeqaEcommerce.WebAPI.Services.Implementation;
using KodeqaEcommerce.WebAPI.Services.Interface;

namespace KodeqaEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPricingService, PricingService>();
            builder.Services.Configure<PricingOptions>(
                builder.Configuration.GetSection("Pricing"));

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
