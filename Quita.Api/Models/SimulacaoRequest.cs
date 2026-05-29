namespace Quita.Api.Models;

/// <summary>
/// Payload recebido pelo endpoint POST /api/simular.
/// Record imutável — dados de entrada nunca são modificados após binding.
/// </summary>
public record SimulacaoRequest(
    decimal ValorDivida,
    string  TempoAtraso,
    string  TipoDivida,
    decimal SaldoFgts = 0m   // opcional — padrão zero
);
