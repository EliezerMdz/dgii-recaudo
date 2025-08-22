CREATE   PROCEDURE [dbo].[ObtenerComprobantesFiscales]
    @TaxpayerTypeId INT = NULL,   -- Contribuyentes.IdTipoContribuyente (opcional)
    @PageNumber     INT = NULL,   -- default 1 si es NULL o < 1
    @Limit          INT = NULL    -- default 10 si es NULL o < 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalizar parámetros
    SET @PageNumber = CASE WHEN ISNULL(@PageNumber, 0) < 1 THEN 1 ELSE @PageNumber END;
    SET @Limit      = CASE WHEN ISNULL(@Limit, 0) < 1 THEN 10 ELSE @Limit END;

    DECLARE @TotalRecords INT, @TotalPages INT;

    -- 0) Total de registros con el mismo filtro (sin CTE)
    SELECT @TotalRecords = COUNT(1)
    FROM dbo.ComprobantesFiscales AS cf
		INNER JOIN dbo.Contribuyentes  AS c ON c.Id = cf.IdContribuyente
    WHERE (@TaxpayerTypeId IS NULL OR c.IdTipoContribuyente = @TaxpayerTypeId);

    SET @TotalPages = CASE
                        WHEN @TotalRecords = 0 THEN 0
                        ELSE CONVERT(INT, CEILING(@TotalRecords * 1.0 / @Limit))
                      END;

    SELECT
        cf.Id               AS Id,
        cf.IdContribuyente  AS TaxpayerId,
        cf.NCF              AS Ncf,
		cf.Monto			AS Amount,
		cf.ITBIS			AS ITBIS,
        cf.Fecha            AS GeneratedAt
    FROM dbo.ComprobantesFiscales AS cf
    INNER JOIN dbo.Contribuyentes  AS c
        ON c.Id = cf.IdContribuyente
    WHERE (@TaxpayerTypeId IS NULL OR c.IdTipoContribuyente = @TaxpayerTypeId)
    ORDER BY cf.Fecha DESC, cf.Id DESC
    OFFSET (@PageNumber - 1) * @Limit ROWS
    FETCH NEXT  @Limit ROWS ONLY;

    SELECT
        @PageNumber   AS PageNumber,
        @Limit        AS PageSize,
        @TotalRecords AS TotalRecords,
        @TotalPages   AS TotalPages;
END
