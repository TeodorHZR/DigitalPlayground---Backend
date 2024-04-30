using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using DigitalPlayground.Data;

namespace DigitalPlayground.Data
{
    public static class DataConfig
    {
        public static void ApplyServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
        }
    }
}
