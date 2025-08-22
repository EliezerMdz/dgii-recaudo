namespace emdz.dgii.recaudo.Domain.Signatures.Response.Base;

public class Pagination
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int TotalRecords { get; set; } = 0;

    public int TotalPages { get; set; } =  0;
}
