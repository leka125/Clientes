using Clientes.DTO;

namespace Clientes.API.Examples
{
    public static class ClienteResponseExamples
    {
        public static ClienteDTO GetClienteExample()
        {
            return new ClienteDTO
            {
                IdCliente = 1,
                Identificacion = "10000001",
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@email.com",
                FechaCreacion = DateTime.Now.AddDays(-30),
                FechaActualizacion = DateTime.Now.AddDays(-1)
            };
        }

        public static List<ClienteDTO> GetClientesListExample()
        {
            return new List<ClienteDTO>
            {
                new ClienteDTO
                {
                    IdCliente = 1,
                    Identificacion = "10000001",
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@email.com",
                    FechaCreacion = DateTime.Now.AddDays(-30),
                    FechaActualizacion = DateTime.Now.AddDays(-1)
                },
                new ClienteDTO
                {
                    IdCliente = 2,
                    Identificacion = "10000002",
                    Nombre = "María",
                    Apellido = "Gómez",
                    Email = "maria.gomez@email.com",
                    FechaCreacion = DateTime.Now.AddDays(-45),
                    FechaActualizacion = DateTime.Now.AddDays(-5)
                }
            };
        }

        public static object GetNotFoundResponseExample()
        {
            return new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                title = "Cliente no encontrado",
                status = 404,
                detail = "No se encontró ningún cliente con la identificación: 99999999",
                traceId = "00-1234567890abcdef1234567890abcdef-9876543210fedcba-00"
            };
        }

        public static object GetBadRequestResponseExample()
        {
            return new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Parámetro inválido",
                status = 400,
                detail = "El número de identificación debe tener al menos 5 caracteres.",
                traceId = "00-1234567890abcdef1234567890abcdef-9876543210fedcba-00"
            };
        }
    }
}