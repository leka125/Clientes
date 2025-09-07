-- Crear la base de datos
CREATE DATABASE DBClientes;
GO

-- Usar la base de datos creada
USE DBClientes;
GO

-- Crear la tabla Clientes
CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY, -- Llave primaria autoincremental
    Identificacion VARCHAR(20) NOT NULL UNIQUE, -- Documento de identificación
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL
);
GO

CREATE PROCEDURE GetClientes
    @NumeroIdentificacion VARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        IdCliente,
        Identificacion,
        Nombre,
        Apellido,
        Email,
        FechaCreacion,
        FechaActualizacion
    FROM Clientes
    WHERE (@NumeroIdentificacion IS NULL OR Identificacion = @NumeroIdentificacion)
    ORDER BY Nombre, Apellido
END