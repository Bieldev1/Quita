namespace Quita.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando uma regra de negócio do domínio é violada.
/// Capturada na camada de API e convertida em HTTP 400.
/// </summary>
public sealed class DomainException : Exception
{
    public string Campo { get; }

    public DomainException(string mensagem, string campo = "geral")
        : base(mensagem)
    {
        Campo = campo;
    }
}
