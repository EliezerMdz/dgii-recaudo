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
    public async Task<TaxReceiptResponse> GetTaxReceipts(TaxReceiptRequest request)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        using var multi = await connection.QueryMultipleAsync("[ObtenerComprobantesFiscales]", new
        {
            request.TaxpayerId,
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
    public async Task<TaxpayerResponse> GetTaxpayers(TaxpayerRequest request)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(Constants.DgiiRecaudoConnectionString));

        using var multi = await connection.QueryMultipleAsync("[ObtenerContribuyentes]", new
        {
            request.@TaxpayerTypeId,
            request.PageNumber,
            request.Limit
        },
        commandType: CommandType.StoredProcedure);

        var taxpayers = await multi.ReadAsync<Taxpayer>();

        var pagination = await multi.ReadFirstOrDefaultAsync<Pagination>();

        return new TaxpayerResponse
        {
            Taxpayers = taxpayers,
            Pagination = pagination ?? new Pagination()
        };
    }

    public Task<DocumentType> GetDocumentTypeByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<LegalEntity> GetLegalEntityByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<NaturalPerson> GetNaturalPersonById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Taxpayer> GetTaxpayerById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TaxpayerType> GetTaxpayerTypeById(int id)
    {
        throw new NotImplementedException();
    }
}
