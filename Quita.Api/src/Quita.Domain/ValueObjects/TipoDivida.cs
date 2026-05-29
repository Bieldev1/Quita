using Quita.Domain.Exceptions;

namespace Quita.Domain.ValueObjects;

/// <summary>
/// Value Object para o tipo de dívida elegível ao Desenrola Brasil.
/// </summary>
public sealed record TipoDivida
{
    public static readonly TipoDivida CartaoCredito  = new("cartao",  "Cartão de crédito");
    public static readonly TipoDivida ChequeEspecial = new("cheque",  "Cheque especial");
    public static readonly TipoDivida CreditoPessoal = new("pessoal", "Crédito pessoal");

    private static readonly IReadOnlyDictionary<string, TipoDivida> _todos =
        new Dictionary<string, TipoDivida>
        {
            [CartaoCredito.Codigo]  = CartaoCredito,
            [ChequeEspecial.Codigo] = ChequeEspecial,
            [CreditoPessoal.Codigo] = CreditoPessoal,
        };

    public string Codigo    { get; }
    public string Descricao { get; }

    private TipoDivida(string codigo, string descricao)
    {
        Codigo    = codigo;
        Descricao = descricao;
    }

    /// <exception cref="DomainException">Código inválido.</exception>
    public static TipoDivida FromCodigo(string codigo)
    {
        if (_todos.TryGetValue(codigo, out var tipo))
            return tipo;

        throw new DomainException(
            $"Tipo de dívida '{codigo}' inválido. Use: {string.Join(", ", _todos.Keys)}.",
            nameof(codigo));
    }
}
