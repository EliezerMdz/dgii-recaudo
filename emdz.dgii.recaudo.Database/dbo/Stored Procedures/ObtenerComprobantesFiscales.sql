
CREATE PROCEDURE [dbo].[ObtenerComprobantesFiscales]
(
	@Id				INT = NULL,
    @TaxPayerId		INT = NULL,
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
		[Id]				int,
		[IdContribuyente]	int,
		[NCF]				nvarchar(50),
		[Monto]				money,
		[ITBIS]				money,
		[Fecha]				datetime
	);

	-- Insertando en la temporal acorde a los parametros
	INSERT INTO #FilteredResults
	SELECT [Id]
		  ,[IdContribuyente]
		  ,[NCF]
		  ,[Monto]
		  ,[ITBIS]
		  ,[Fecha]
	FROM [dbo].[ComprobantesFiscales]
	WHERE (@Id IS NULL OR @Id = [Id]) 
	AND (@TaxPayerId IS NULL OR @TaxPayerId = [IdContribuyente]);

	-- Calculando el total de registros y paginas
	SET @TotalRecords	= (SELECT COUNT(*) FROM #FilteredResults);
	SET @TotalPages		= CEILING(CAST(@TotalRecords AS float) / @Limit);

	-- Dataset[0] con los datos del modelo
	SELECT [Id]
		  ,[IdContribuyente]	AS [TaxpayerId]
		  ,[NCF]				AS [Ncf]
		  ,[Monto]				AS [Amount]
		  ,[ITBIS]				AS [ITBIS]
		  ,[Fecha]				AS [GeneratedAt]
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
