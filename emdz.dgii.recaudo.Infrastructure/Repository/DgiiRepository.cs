using Dapper;
using emdz.dgii.recaudo.Domain.Entities;
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
    /// <exception cref="NotImplementedException"></exception>
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
    /// <exception cref="NotImplementedException"></exception>
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
    /// <param name="id"></param>
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

    public async Task<DocumentType> GetDocumentTypeByIdAsync(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        var response = await connection.QuerySingleAsync<DocumentType>("[ObtenerTiposDocumentos]", new
        {
            Id = id
        },
        commandType: CommandType.StoredProcedure);

        return response;
    }

    public Task<LegalEntity> GetLegalEntityByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<NaturalPerson> GetNaturalPersonByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
