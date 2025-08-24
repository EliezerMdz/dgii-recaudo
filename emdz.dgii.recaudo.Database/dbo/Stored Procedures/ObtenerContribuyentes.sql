CREATE PROCEDURE [dbo].[ObtenerContribuyentes] 
(
	@Id				INT = NULL,
    @TaxpayerTypeId INT = NULL,
	@DocumentTypeId	INT = NULL,
    @PageNumber     INT = NULL,
    @Limit          INT = NULL
)
AS
BEGIN
    -- Parámetros
    DECLARE @TotalRecords            INT = 0;
    DECLARE @TotalPages              INT = 0;
    DECLARE @PageNumberDefaultValue  INT = 1;
    DECLARE @LimitDefaultValue       INT = 10;

    -- Definiendo los valores por default
    SET @PageNumber = COALESCE(@PageNumber, @PageNumberDefaultValue);
    SET @Limit      = COALESCE(@Limit, @LimitDefaultValue);

    -- Calculando el offset de la paginación
    DECLARE @Offset INT = (@PageNumber - 1) * @Limit;

    -- Definiendo tabla temporal
    CREATE TABLE #FilteredResults
    (
        [Id]                  INT,
        [IdTipoContribuyente] INT,
        [IdTipoDocumento]     INT,
        [NumeroDocumento]     NVARCHAR(25),
        [Estado]              BIT
    );

    -- Insertando en la temporal acorde a los parámetros
    INSERT INTO #FilteredResults
    SELECT [Id],
           [IdTipoContribuyente],
           [IdTipoDocumento],
           [NumeroDocumento],
           [Estado]
    FROM [dbo].[Contribuyentes]
	WHERE (@Id IS NULL OR @Id = [Id]) 
	AND (@TaxpayerTypeId IS NULL OR [IdTipoContribuyente] = @TaxpayerTypeId)
	AND (@DocumentTypeId IS NULL OR [IdTipoDocumento] = @DocumentTypeId);

    -- Calculando el total de registros y páginas
    SET @TotalRecords = (SELECT COUNT(*) FROM #FilteredResults);
    SET @TotalPages   = CEILING(CAST(@TotalRecords AS FLOAT) / @Limit);

    -- Dataset[0] con los datos del modelo
    SELECT [Id],
           [IdTipoContribuyente]	AS [TaxpayerTypeId],
           [IdTipoDocumento]		AS [DocumentTypeId],
           [NumeroDocumento]		AS [DocumentNumber],
           [Estado]					AS [IsActive]
    FROM #FilteredResults
    ORDER BY [Id]
    OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;

	-- Cuando se busca por mas de 1 registro
	IF @Id IS NULL
	BEGIN
		-- Dataset[1] con los datos de la paginacion
		SELECT	@PageNumber   AS PageNumber,
				@Limit        AS PageSize,
				@TotalRecords AS TotalRecords,
				@TotalPages   AS TotalPages;
	END;

    -- Se elimina la tabla temporal antes de terminar la operación
    DROP TABLE #FilteredResults;
END
