CREATE PROCEDURE [dbo].[ObtenerEntidades] 
(
	@Id				INT = NULL,
    @PageNumber     INT = NULL,
    @Limit          INT = NULL
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
		[Id]     INT,
		[Rnc]    NVARCHAR(25),
		[Nombre] NVARCHAR(100),
		[Estado] BIT
	);

	-- Insertando en la temporal acorde a los parametros 
	INSERT INTO #FilteredResults
	SELECT
		[Id],
		[Rnc],
		[Nombre],
		[Estado]
	FROM [dbo].[Entidades]
	WHERE (@Id IS NULL OR @Id = [Id]);

	-- Calculando el total de registros y paginas
	SET @TotalRecords	= (SELECT COUNT(*) FROM #FilteredResults);
	SET @TotalPages		= CEILING(CAST(@TotalRecords AS float) / @Limit);

	-- Dataset[0] con los datos del modelo
	SELECT  [Id],
			[Rnc],
			[Nombre]	AS [Name],
			[Estado]	AS [IsActive]
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
