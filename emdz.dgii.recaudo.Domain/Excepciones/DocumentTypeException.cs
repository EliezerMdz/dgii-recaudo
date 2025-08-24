namespace emdz.dgii.recaudo.Domain.Excepciones;

public sealed class DocumentTypeException(string code, string message) : Exception(message)
{    
    public string Code { get; } = code;
}
