CREATE PROCEDURE [dbo].[ObtenerPersonas] 
(
	@Id					INT				= NULL,
    @DocumentTypeId		INT				= NULL,
	@DocumentNumber		NVARCHAR(25)	= NULL,
    @PageNumber			INT				= NULL,
    @Limit				INT				= NULL
)
AS
BEGIN
    -- Parámetros
	DECLARE @TotalRecords			int	= 0;
	DECLARE @TotalPages				int = 0;
	DECLARE @PageNumberDefaultValue int = 1;
	DECLARE @LimitDefaultValue		int = 10;

	-- Definiendo los valores por default
    SET @PageNumber = COALESCE(@PageNumber, @PageNumberDefaultValue);
    SET @Limit		= COALESCE(@Limit, @LimitDefaultValue);

	-- Calculando el offset de la paginacion
	DECLARE @Offset	int	= (@PageNumber - 1) * @Limit;

	-- Definiendo tabla temporal
	CREATE TABLE #FilteredResults
	(
		[Id]              INT,
		[IdTipoDocumento] INT,
		[NumeroDocumento] NVARCHAR(25),
		[PrimerNombre]    NVARCHAR(50),
		[SegundoNombre]   NVARCHAR(50) NULL,
		[PrimerApellido]  NVARCHAR(50),
		[SegundoApellido] NVARCHAR(50) NULL,
		[FechaNacimiento] DATE,
		[Estado]          BIT
	);

	-- Insertando en la temporal acorde a los parametros
	INSERT INTO #FilteredResults
	SELECT
		[Id],
		[IdTipoDocumento],
		[NumeroDocumento],
		[PrimerNombre],
		[SegundoNombre],
		[PrimerApellido],
		[SegundoApellido],
		[FechaNacimiento],
		[Estado]
	FROM [dbo].[Personas]
	WHERE(@Id IS NULL OR @Id = [Id])
	AND (@DocumentTypeId IS NULL OR @DocumentTypeId = [IdTipoDocumento])
	AND (@DocumentNumber IS NULL OR @DocumentNumber = [NumeroDocumento]);

	-- Calculando el total de registros y paginas
	SET @TotalRecords	= (SELECT COUNT(*) FROM #FilteredResults);
	SET @TotalPages		= CEILING(CAST(@TotalRecords AS float) / @Limit);

	-- Dataset[0] con los datos del modelo
	SELECT  [Id],
			[IdTipoDocumento]	AS DocumentTypeId,
			[NumeroDocumento]	AS DocumentNumber,
			[PrimerNombre]		AS FirstName,
			[SegundoNombre]		AS MiddleName,
			[PrimerApellido]	AS FirstLastName,
			[SegundoApellido]	AS SecondLastName,
			[FechaNacimiento]	AS Birthday,
			[Estado]			AS IsActive
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

	-- Se elimina la tabla temporal antes de terminar la operacion
	DROP TABLE #FilteredResults;
END
