using Dapper;
using emdz.dgii.recaudo.Domain.Entities;
using emdz.dgii.recaudo.Domain.Excepciones;
using emdz.dgii.recaudo.Domain.Interfaces.Repository;
using emdz.dgii.recaudo.Domain.Signatures.Request;
using emdz.dgii.recaudo.Domain.Signatures.Response;
using emdz.dgii.recaudo.Domain.Signatures.Response.Base;
using emdz.dgii.recaudo.Domain.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace emdz.dgii.recaudo.Infrastructure.Repository;

public class DgiiRepository(IConfiguration configuration) : IDgiiRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxReceiptResponse> GetTaxReceiptsAsync(TaxReceiptRequest request)
    {
        try 
        { 
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            using var multi = await connection.QueryMultipleAsync("[ObtenerComprobantesFiscales]", new
            {
                request.TaxPayerId,
                request.PageNumber,
                request.Limit
            }, 
            commandType: CommandType.StoredProcedure);

            var taxReceipts = await multi.ReadAsync<TaxReceipt>();

            var pagination = await multi.ReadFirstOrDefaultAsync<Pagination>();

            return new TaxReceiptResponse
            {
                TaxReceipts = taxReceipts,
                Pagination = pagination ?? new Pagination()
            };
        }
        catch
        {
            throw new TaxReceiptException("infrastructure.repository.TaxReceiptResponse", $"Could not retrieve tax receipts for tax payer with ID {request.TaxPayerId} response from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxReceiptSummaryResponse> GetTaxReceiptsSummaryAsync(TaxReceiptSummaryRequest request)
    {
        try
        {
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<TaxReceiptSummaryResponse>("[ObtenerResumenComprobantesFiscales]", new
            {
                request.TaxPayerId
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new TaxReceiptException("infrastructure.repository.TaxReceiptResponse", $"Could not retrieve tax receipts summary for tax payer with ID {request.TaxPayerId} response from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request)
    {
        try 
        { 
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            using var multi = await connection.QueryMultipleAsync("[ObtenerContribuyentes]", new
            {
                request.@TaxPayerTypeId,
                request.PageNumber,
                request.Limit
            },
            commandType: CommandType.StoredProcedure);

            var taxPayers = await multi.ReadAsync<TaxPayer>();

            var pagination = await multi.ReadFirstOrDefaultAsync<Pagination>();

            return new TaxPayerResponse
            {
                TaxPayers = taxPayers,
                Pagination = pagination ?? new Pagination()
            };
        }
        catch
        {
            throw new TaxPayerException("infrastructure.repository.TaxPayerResponse", $"Could not retrieve tax payers response from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayer> GetTaxPayerByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<TaxPayer>("[ObtenerContribuyentes]", new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new TaxPayerException("infrastructure.repository.TaxPayer", $"Could not retrieve tax payer with ID {id} from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayerType> GetTaxPayerTypeByIdAsync(int id)
    {
        try 
        { 
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<TaxPayerType>("[ObtenerTiposContribuyentes]", new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new TaxPayerTypeException("infrastructure.repository.TaxPayerType", $"Could not retrieve tax payer type with ID {id} from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<DocumentType> GetDocumentTypeByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<DocumentType>("[ObtenerTiposDocumentos]", new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new DocumentTypeException("infrastructure.repository.DocumentType", $"Could not retrieve document type with ID {id} from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<NaturalPerson> GetNaturalPersonByDocumentAsync(int documentTypeId, string documentNumber)
    {
        try
        {
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<NaturalPerson>("[ObtenerPersonas]", new
            {
                DocumentTypeId = documentTypeId,
                DocumentNumber = documentNumber
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new NaturalPersonException("infrastructure.repository.NaturalPerson", $"Could not retrieve natural person with documento type {documentTypeId} and documentNumber {documentNumber} from the database");
        }   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LegalEntity> GetLegalEntityByRncAsync(string documentNumber)
    {
        try
        {
            using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

            var response = await connection.QuerySingleAsync<LegalEntity>("[ObtenerEntidades]", new
            {
                Rnc = documentNumber
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new LegalEntityException("infrastructure.repository.LegalEntity", $"Could not retrieve legal entity with documento number {documentNumber} from the database");
        }   
    }
}
