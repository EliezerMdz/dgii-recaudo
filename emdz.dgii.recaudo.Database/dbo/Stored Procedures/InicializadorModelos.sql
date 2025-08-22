CREATE PROCEDURE [InicializadorModelos]
AS
BEGIN TRANSACTION;

BEGIN TRY
    -- ==============================
    -- 1. TiposDocumentos
    -- ==============================
    INSERT INTO [dbo].[TiposDocumentos] (Codigo, Descripcion, Estado)
    VALUES
    (N'CED', N'Cédula de Identidad', 1),
    (N'PAS', N'Pasaporte', 1),
    (N'RNC', N'Registro Nacional Contribuyentes', 1);

    -- ==============================
    -- 2. TiposContribuyentes
    -- ==============================
    INSERT INTO [dbo].[TiposContribuyentes] (Codigo, Descripcion, Estado)
    VALUES
    (N'PER', N'Persona Física', 1),
    (N'EMP', N'Empresa', 1),
    (N'ORG', N'Organización sin fines de lucro', 1);

    -- ==============================
    -- 3. Entidades
    -- ==============================
    INSERT INTO [dbo].[Entidades] (Id, Rnc, Nombre, Estado)
    VALUES
    (1, N'101010101', N'Entidad de Prueba 1', 1),
    (2, N'202020202', N'Entidad de Prueba 2', 1);

    -- ==============================
    -- 4. Personas
    -- ==============================
    INSERT INTO [dbo].[Personas] 
        (IdTipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, FechaNacimiento, Estado)
    VALUES
    (1, N'001-0000000-1', N'Juan', N'Carlos', N'Pérez', N'Gómez', '1990-05-12', 1),
    (1, N'001-0000000-2', N'Ana', NULL, N'Martínez', N'Rodríguez', '1985-09-23', 1);

    -- ==============================
    -- 5. Contribuyentes
    -- ==============================
    INSERT INTO [dbo].[Contribuyentes] 
        (IdTipoContribuyente, IdTipoDocumento, NumeroDocumento, Estado)
    VALUES
    (1, 1, N'001-0000000-1', 1), -- Persona Física
    (2, 3, N'202020202', 1);     -- Empresa con RNC

    -- ==============================
    -- 6. ComprobantesFiscales
    -- ==============================
    INSERT INTO [dbo].[ComprobantesFiscales] 
        (IdContribuyente, NCF, Monto, ITBIS, Fecha)
    VALUES
    (1, N'B0100000001', 1500.00, 1, GETDATE()),
    (2, N'B0200000001', 25000.00, 1, GETDATE());

    COMMIT TRANSACTION;
    PRINT 'Initialization completed successfully.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Error occurred. Initialization rolled back.';
    THROW;
END CATCH;
