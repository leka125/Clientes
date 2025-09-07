using Clientes.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Repositories
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Se configura el contexto de la base de datos
            services.AddDbContext<ClienteDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Se registra el repositorio
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}
