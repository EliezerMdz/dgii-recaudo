using AutoMapper;
using emdz.dgii.recaudo.CrossCutting.DataTransferObject.Signatures.Response;
using emdz.dgii.recaudo.Domain.Interfaces.Application;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;
using Microsoft.AspNetCore.Mvc;

namespace emdz.dgii.recaudo.WebAPI.Controllers;

/// <summary>
/// Provides endpoints for retrieving tax-related data, including tax receipts and taxPayers, with support for filtering and pagination.
/// </summary>
/// <remarks>This controller is part of the API for interacting with the DGII (Dirección General de Impuestos
/// Internos) system.  It includes methods for retrieving tax receipts and taxPayers based on various criteria.  Use the
/// provided endpoints to query data with optional filters and pagination parameters.</remarks>
/// <param name="application"></param>
[ApiController]
[Route("api/dgii")]
public class DgiiController(IDgiiApplication application, IMapper mapper) : Controller
{
    /// <summary>
    /// Retrieves a paginated list of tax receipts based on the specified filters.
    /// </summary>
    /// <remarks>Use this method to retrieve tax receipts for a specific taxPayer or within a specific date
    /// range. Pagination parameters can be used to control the size and position of the result set.</remarks>
    /// <param name="taxPayerId">The unique identifier of the taxPayer. If null, receipts for all taxPayers are retrieved.</param>
    /// <param name="startDate">The start date of the date range for filtering receipts. If null, no lower date limit is applied.</param>
    /// <param name="endDate">The end date of the date range for filtering receipts. If null, no upper date limit is applied.</param>
    /// <param name="pageNumber">The page number for pagination. If null, the first page is retrieved.</param>
    /// <param name="limit">The maximum number of receipts to include in a single page. If null, a default limit is applied.</param>
    /// <returns>An <see cref="IActionResult"/> containing a paginated list of tax receipts that match the specified filters.
    /// Returns a 500 status code if an error occurs during processing.</returns>
    [HttpGet("taxreceipts")]
    public async Task<IActionResult> GetTaxReceipts(int? taxPayerId, DateTime? startDate, DateTime? endDate,  int? pageNumber, int? limit)
    {
        try
        {
            var response = await application.GetTaxReceiptsAsync(new TaxReceiptRequest 
            {
                TaxPayerId = taxPayerId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                Limit = limit
            });

            // Return OK with no content if there are no tax receipts
            if (response.TaxReceipts?.Count() == 0) return Ok();
            
            var responseDto = mapper.Map<TaxReceiptResponse, TaxReceiptResponseDto>(response);

            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a paginated list of taxPayers based on the specified criteria.
    /// </summary>
    /// <remarks>This method supports filtering by taxPayer type and pagination to manage large datasets. 
    /// Ensure that valid values are provided for <paramref name="pageNumber"/> and <paramref name="limit"/>  to avoid
    /// unexpected results.</remarks>
    /// <param name="taxPayerTypeId">The optional identifier of the taxPayer type to filter the results. If null, all taxPayer types are included.</param>
    /// <param name="pageNumber">The optional page number for pagination. If null, the first page is returned.</param>
    /// <param name="limit">The optional maximum number of taxPayers to include in the response. If null, a default limit is applied.</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of taxPayers that match the specified criteria.
    /// Returns a 500 status code if an error occurs during processing.</returns>
    [HttpGet("taxPayers")]
    public async Task<IActionResult> GetTaxPayers(int? taxPayerTypeId, int? pageNumber, int? limit)
    {
        try
        {
            var response = await application.GetTaxPayersAsync(new TaxPayerRequest
            {
                TaxPayerTypeId = taxPayerTypeId,
                PageNumber = pageNumber,
                Limit = limit
            });


            // Return OK with no content if there are no tax receipts
            if (response.TaxPayers?.Count() == 0) return Ok();

            var responseDto = mapper.Map<TaxPayerResponse, TaxPayerResponseDto>(response);

            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
