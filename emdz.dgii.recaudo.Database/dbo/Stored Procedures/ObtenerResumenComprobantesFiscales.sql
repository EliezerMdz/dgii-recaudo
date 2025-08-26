CREATE PROCEDURE [dbo].[ObtenerResumenComprobantesFiscales]
(
    @TaxPayerId		INT
)
AS
BEGIN
	select	IdContribuyente as TaxPayerId, 
			count(NCF) as TotalRecords, 
			SUM(MONTO) as TotalAmount, 
			SUM(ITBIS) as TotalItbis
	from dbo.ComprobantesFiscales 
	where IdContribuyente = @TaxPayerId
	group by IdContribuyente;
END
