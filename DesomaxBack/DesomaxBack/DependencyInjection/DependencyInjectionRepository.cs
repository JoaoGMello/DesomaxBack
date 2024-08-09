using DesomaxBack.Interfaces;
using DesomaxBack.Repositories;

namespace DesomaxBack.DependencyInjection
{
    public static class DependencyInjectionRepository
    {
        public static void ConfigurePersistence(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();
        }
    }
}
