using emdz.dgii.recaudo.Domain.Interfaces.Application;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using Microsoft.AspNetCore.Mvc;

namespace emdz.dgii.recaudo.WebAPI.Controllers;

/// <summary>
/// Provides endpoints for retrieving tax-related data, including tax receipts and taxpayers, with support for filtering and pagination.
/// </summary>
/// <remarks>This controller is part of the API for interacting with the DGII (Dirección General de Impuestos
/// Internos) system.  It includes methods for retrieving tax receipts and taxpayers based on various criteria.  Use the
/// provided endpoints to query data with optional filters and pagination parameters.</remarks>
/// <param name="application"></param>
[ApiController]
[Route("api/dgii")]
public class DgiiController(IDgiiApplication application) : Controller
{
    /// <summary>
    /// Retrieves a paginated list of tax receipts based on the specified filters.
    /// </summary>
    /// <remarks>Use this method to retrieve tax receipts for a specific taxpayer or within a specific date
    /// range. Pagination parameters can be used to control the size and position of the result set.</remarks>
    /// <param name="taxpayerId">The unique identifier of the taxpayer. If null, receipts for all taxpayers are retrieved.</param>
    /// <param name="startDate">The start date of the date range for filtering receipts. If null, no lower date limit is applied.</param>
    /// <param name="endDate">The end date of the date range for filtering receipts. If null, no upper date limit is applied.</param>
    /// <param name="pageNumber">The page number for pagination. If null, the first page is retrieved.</param>
    /// <param name="limit">The maximum number of receipts to include in a single page. If null, a default limit is applied.</param>
    /// <returns>An <see cref="IActionResult"/> containing a paginated list of tax receipts that match the specified filters.
    /// Returns a 500 status code if an error occurs during processing.</returns>
    [HttpGet("taxreceipts")]
    public async Task<IActionResult> GetTaxReceipts(int? taxpayerId, DateTime? startDate, DateTime? endDate,  int? pageNumber, int? limit)
    {
        try
        {
            var response = await application.GetTaxReceipts(new TaxReceiptRequest 
            {
                TaxpayerId = taxpayerId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                Limit = limit
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a paginated list of taxpayers based on the specified criteria.
    /// </summary>
    /// <remarks>This method supports filtering by taxpayer type and pagination to manage large datasets. 
    /// Ensure that valid values are provided for <paramref name="pageNumber"/> and <paramref name="limit"/>  to avoid
    /// unexpected results.</remarks>
    /// <param name="taxpayerTypeId">The optional identifier of the taxpayer type to filter the results. If null, all taxpayer types are included.</param>
    /// <param name="pageNumber">The optional page number for pagination. If null, the first page is returned.</param>
    /// <param name="limit">The optional maximum number of taxpayers to include in the response. If null, a default limit is applied.</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of taxpayers that match the specified criteria.
    /// Returns a 500 status code if an error occurs during processing.</returns>
    [HttpGet("taxpayers")]
    public async Task<IActionResult> GetTaxpayers(int? taxpayerTypeId, int? pageNumber, int? limit)
    {
        try
        {
            var response = await application.GetTaxpayers(new TaxpayerRequest
            {
                TaxpayerTypeId = taxpayerTypeId,
                PageNumber = pageNumber,
                Limit = limit
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
