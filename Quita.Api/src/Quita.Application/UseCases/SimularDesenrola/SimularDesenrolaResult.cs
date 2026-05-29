namespace Quita.Application.UseCases.SimularDesenrola;

/// <summary>
/// Result (objeto de saída) do caso de uso SimularDesenrola.
/// Projeção plana da entidade Simulacao — desacoplada do modelo de domínio.
/// Serializada diretamente como JSON na resposta HTTP.
/// </summary>
public sealed record SimularDesenrolaResult(
    decimal DividaOriginal,
    decimal PercentualDesconto,
    decimal ValorDesconto,
    decimal FgtsUtilizado,
    decimal SaldoAposDesconto,
    decimal SaldoAPagar,
    decimal ParcelaEstimada,
    int     NumeroParcelas
);
