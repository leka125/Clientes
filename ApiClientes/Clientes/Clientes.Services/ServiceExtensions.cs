using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Se registra el servicio
            services.AddScoped<IClienteService, ClienteService>();
            return services;
        }
    }
}
