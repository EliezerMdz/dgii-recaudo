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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayerResponse> GetTaxPayersAsync(TaxPayerRequest request)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayer> GetTaxPayerByIdAsync(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        var response = await connection.QuerySingleAsync<TaxPayer>("[ObtenerContribuyentes]", new
        {
            Id = id
        },
        commandType: CommandType.StoredProcedure);

        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TaxPayerType> GetTaxPayerTypeByIdAsync(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        var response = await connection.QuerySingleAsync<TaxPayerType>("[ObtenerTiposContribuyentes]", new
        {
            Id = id
        },
        commandType: CommandType.StoredProcedure);

        return response;
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

            var response = await connection.QuerySingleAsync<DocumentType>("[ObtenerTiposDocumentos2]", new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);

            return response;
        }
        catch
        {
            throw new DocumentTypeException("infrastructure.repository.documentType", $"Could not retrieve document type with ID {id} from the database");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<NaturalPerson> GetNaturalPersonByDocumentAsync(int documentTypeId, string documentNumber)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LegalEntity> GetLegalEntityByRncAsync(string documentNumber)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        var response = await connection.QuerySingleAsync<LegalEntity>("[ObtenerEntidades]", new
        {
            Rnc = documentNumber
        },
        commandType: CommandType.StoredProcedure);

        return response;
    }
}
