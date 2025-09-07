using Clientes.DTO;
using Clientes.Repositories;
using Clientes.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        public async Task<List<ClienteDTO>> ObtenerClientesAsync(string numeroIdentificacion)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(numeroIdentificacion))
            {
                throw new BusinessException("IDENT_EMPTY", "El número de identificación es requerido.");
            }

            if (numeroIdentificacion.Length < 5)
            {
                throw new BusinessException("IDENT_TOO_SHORT", "El número de identificación debe tener al menos 5 caracteres.");
            }

            try
            {
                _logger.LogInformation("Consultando cliente con identificación: {Identificacion}", numeroIdentificacion);

                var clientes = await _clienteRepository.GetClientesAsync(numeroIdentificacion);

                return clientes ?? new List<ClienteDTO>();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Error de base de datos al consultar cliente: {Identificacion}", numeroIdentificacion);
                throw new BusinessException("DATABASE_ERROR", "Error de conexión con la base de datos.", sqlEx);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de Entity Framework al consultar cliente: {Identificacion}", numeroIdentificacion);
                throw new BusinessException("DATABASE_ERROR", "Error en la operación de base de datos.", dbEx);
            }
            catch (TimeoutException timeoutEx)
            {
                _logger.LogError(timeoutEx, "Timeout de base de datos al consultar cliente: {Identificacion}", numeroIdentificacion);
                throw new BusinessException("DATABASE_TIMEOUT", "Timeout en la conexión a la base de datos.", timeoutEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar cliente: {Identificacion}", numeroIdentificacion);
                throw new BusinessException("UNEXPECTED_ERROR", "Error inesperado en el servidor.", ex);
            }
        }

        public async Task<List<ClienteDTO>> ObtenerTodosClientesAsync()
        {
            try
            {
                _logger.LogInformation("Consultando todos los clientes");

                var clientes = await _clienteRepository.GetClientesAsync();

                return clientes ?? new List<ClienteDTO>();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Error de base de datos al consultar todos los clientes");
                throw new BusinessException("DATABASE_ERROR", "Error de conexión con la base de datos.", sqlEx);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de Entity Framework al consultar todos los clientes");
                throw new BusinessException("DATABASE_ERROR", "Error en la operación de base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar todos los clientes");
                throw new BusinessException("UNEXPECTED_ERROR", "Error inesperado en el servidor.", ex);
            }
        }
    }
}