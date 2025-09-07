using Clientes.DTO;
using Clientes.Service;
using Clientes.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClientesController> _logger;


        public ClientesController(IClienteService clienteService, ILogger<ClientesController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene un cliente por su número de identificación
        /// </summary>
        /// <param name="identificacion">Número de identificación del cliente</param>
        /// <returns>Información del cliente si existe</returns>
        [HttpGet("{identificacion}")]
        [ProducesResponseType(typeof(ClienteDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetClientePorIdentificacion(string identificacion)
        {
            try
            {
                _logger.LogInformation("Consultando cliente con identificación: {Identificacion}", identificacion);

                var clientes = await _clienteService.ObtenerClientesAsync(identificacion);

                if (clientes == null || clientes.Count == 0)
                {
                    _logger.LogWarning("Cliente con identificación {Identificacion} no encontrado", identificacion);
                    return NotFound(new ProblemDetails
                    {
                        Title = "Cliente no encontrado",
                        Detail = $"No se encontró ningún cliente con la identificación: {identificacion}",
                        Status = (int)HttpStatusCode.NotFound
                    });
                }

                // Se toma el primer cliente en caso de múltiples coincidencias
                var cliente = clientes.FirstOrDefault();
                _logger.LogInformation("Cliente encontrado: {ClienteId}", cliente?.IdCliente);
                return Ok(cliente);
            }
            catch(BusinessException ex)
            {
                return HandleBusinessException(ex, identificacion);
            }
            //catch (ArgumentException ex)
            //{
            //    _logger.LogWarning(ex, "Error de validación en la identificación: {Identificacion}", identificacion);
            //    return BadRequest(new ProblemDetails
            //    {
            //        Title = "Parámetro inválido",
            //        Detail = ex.Message,
            //        Status = (int)HttpStatusCode.BadRequest
            //    });
            //}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al consultar cliente con identificación: {Identificacion}", identificacion);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Error interno del servidor",
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista de todos los clientes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllClientes()
        {
            try
            {
                _logger.LogInformation("Consultando todos los clientes");

                var clientes = await _clienteService.ObtenerTodosClientesAsync();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al consultar todos los clientes");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Error interno del servidor",
                    Detail = "Ocurrió un error interno al procesar la solicitud",
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        private IActionResult HandleBusinessException(BusinessException ex, string identificacion)
        {
            switch (ex.ErrorCode)
            {
                case "IDENT_EMPTY":
                case "IDENT_TOO_SHORT":
                case "IDENT_INVALID_FORMAT":
                    // 400 Bad Request para errores de validación
                    _logger.LogWarning(ex, "Error de validación: {Identificacion}", identificacion);
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Parámetro inválido",
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.BadRequest
                    });

                case "DATABASE_ERROR":
                case "DATABASE_TIMEOUT":
                case "UNEXPECTED_ERROR":
                    // 500 Internal Server Error para errores de base de datos y del servidor
                    _logger.LogError(ex, "Error del servidor: {Identificacion}", identificacion);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                    {
                        Title = "Error interno del servidor",
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.InternalServerError
                    });

                default:
                    // Por defecto, Bad Request para otras BusinessException
                    _logger.LogWarning(ex, "Error de negocio: {Identificacion}", identificacion);
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Error de negocio",
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.BadRequest
                    });
            }
        }
    }
}
