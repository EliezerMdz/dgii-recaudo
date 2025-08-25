namespace emdz.dgii.recaudo.Domain.Excepciones;

public sealed class LegalEntityException(string code, string message) : Exception(message)
{    
    public string Code { get; } = code;
}
