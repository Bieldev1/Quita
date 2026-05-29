namespace Quita.Application.UseCases.SimularDesenrola;

/// <summary>
/// Command (objeto de entrada) do caso de uso SimularDesenrola.
/// Record imutável — dados nunca são modificados após recebimento.
/// Mapeado diretamente do corpo da requisição HTTP na camada de API.
/// </summary>
public sealed record SimularDesenrolaCommand(
    decimal ValorDivida,
    string  TempoAtraso,
    string  TipoDivida,
    decimal SaldoFgts = 0m
);
