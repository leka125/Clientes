using Clientes.DTO;
using Clientes.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDTO>> GetClientesAsync(string numeroIdentificacion)
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC GetClientes @NumeroIdentificacion = {0}", numeroIdentificacion)
                .ToListAsync();
        }

        public async Task<List<ClienteDTO>> GetClientesAsync()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC GetClientes")
                .ToListAsync();
        }
    }
}
