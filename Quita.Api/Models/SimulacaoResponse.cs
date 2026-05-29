namespace Quita.Api.Models;

/// <summary>
/// Resultado calculado pelo CalculoDesenrolaService.
/// Todos os valores monetários em reais (BRL), arredondados em 2 casas.
/// </summary>
public record SimulacaoResponse(
    decimal DividaOriginal,
    decimal PercentualDesconto,   // 0–1  (ex: 0.53 = 53%)
    decimal ValorDesconto,
    decimal FgtsUtilizado,
    decimal SaldoAposDesconto,
    decimal SaldoAPagar,
    decimal ParcelaEstimada,
    int     NumeroParcelas
);
