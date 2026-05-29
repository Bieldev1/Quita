using Quita.Domain.Exceptions;

namespace Quita.Domain.ValueObjects;

/// <summary>
/// Value Object que encapsula o tempo de atraso da dívida e o
/// percentual de desconto correspondente (regra oficial do Desenrola).
/// Usa instâncias estáticas (Enumeration-like) para garantir que
/// apenas valores válidos existam em tempo de compilação.
/// </summary>
public sealed record TempoAtraso
{
    // ── Valores canônicos ─────────────────────────────────────────────────────
    public static readonly TempoAtraso CurtoAtraso = new("90-180",  0.30m, "90 a 180 dias");
    public static readonly TempoAtraso MedioAtraso = new("181-365", 0.45m, "181 a 365 dias");
    public static readonly TempoAtraso LongoAtraso = new("365+",    0.53m, "Mais de 365 dias");

    private static readonly IReadOnlyDictionary<string, TempoAtraso> _todos =
        new Dictionary<string, TempoAtraso>
        {
            [CurtoAtraso.Codigo] = CurtoAtraso,
            [MedioAtraso.Codigo] = MedioAtraso,
            [LongoAtraso.Codigo] = LongoAtraso,
        };

    public string  Codigo              { get; }
    public decimal PercentualDesconto  { get; }
    public string  Descricao           { get; }

    private TempoAtraso(string codigo, decimal percentualDesconto, string descricao)
    {
        Codigo             = codigo;
        PercentualDesconto = percentualDesconto;
        Descricao          = descricao;
    }

    /// <summary>Cria um TempoAtraso a partir do código recebido pela API.</summary>
    /// <exception cref="DomainException">Código inválido.</exception>
    public static TempoAtraso FromCodigo(string codigo)
    {
        if (_todos.TryGetValue(codigo, out var tempo))
            return tempo;

        throw new DomainException(
            $"Tempo de atraso '{codigo}' inválido. Use: {string.Join(", ", _todos.Keys)}.",
            nameof(codigo));
    }
}
