USE DBClientes;
GO

-- Listas de nombres y apellidos
DECLARE @Nombres TABLE (Nombre VARCHAR(50));
INSERT INTO @Nombres VALUES
('Juan'),('Mar�a'),('Carlos'),('Ana'),('Luis'),('Laura'),
('Pedro'),('Sof�a'),('Andr�s'),('Camila'),
('Fernando'),('Valentina'),('Diego'),('Paula'),('Jorge');

DECLARE @Apellidos TABLE (Apellido VARCHAR(50));
INSERT INTO @Apellidos VALUES
('G�mez'),('Rodr�guez'),('L�pez'),('Mart�nez'),('Garc�a'),
('Hern�ndez'),('P�rez'),('Ram�rez'),('Torres'),('D�az'),
('Moreno'),('Castro'),('Vargas'),('Jim�nez'),('Ruiz');

-- Insertar 100 registros aleatorios
DECLARE @i INT = 1;
DECLARE @Nombre VARCHAR(50);
DECLARE @Apellido VARCHAR(50);

WHILE @i <= 100
BEGIN
    -- Seleccionar aleatoriamente un nombre
    SELECT TOP 1 @Nombre = Nombre
    FROM @Nombres
    ORDER BY NEWID();

    -- Seleccionar aleatoriamente un apellido
    SELECT TOP 1 @Apellido = Apellido
    FROM @Apellidos
    ORDER BY NEWID();

    -- Insertar cliente
    INSERT INTO Clientes (Identificacion, Nombre, Apellido, Email)
    VALUES (
        CAST(10000000 + @i AS VARCHAR(20)),         -- Identificaci�n �nica
        @Nombre,
        @Apellido,
        LOWER(CONCAT(@Nombre, '.', @Apellido, @i, '@correo.com')) -- Email �nico
    );

    SET @i = @i + 1;
END;
GO
