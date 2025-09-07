using Clientes.DTO;

namespace Clientes.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ObtenerClientesAsync(string numeroIdentificacion);
        Task<List<ClienteDTO>> ObtenerTodosClientesAsync();
    }
}
