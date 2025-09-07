using Clientes.DTO;

namespace Clientes.Repositories
{
    public interface IClienteRepository
    {
        Task<List<ClienteDTO>> GetClientesAsync(string numeroIdentificacion);
        Task<List<ClienteDTO>> GetClientesAsync();
    }
}
