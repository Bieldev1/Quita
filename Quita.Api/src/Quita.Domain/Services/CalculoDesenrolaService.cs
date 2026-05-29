using Quita.Domain.Entities;
using Quita.Domain.ValueObjects;

namespace Quita.Domain.Services;

/// <summary>
/// Domain Service: contém a lógica de cálculo do Novo Desenrola Brasil.
/// É um serviço de domínio (não uma entidade) porque o cálculo envolve
/// regras que não pertencem naturalmente a um único aggregate.
///
/// Regras oficiais (Resolução CMN 5.108/2023):
///  • Desconto por tempo de atraso (encapsulado no VO TempoAtraso)
///  • Abatimento FGTS: máx(20% do saldo, R$ 1.000), limitado ao saldo devedor
///  • Parcelas: ceil(saldo / R$200), entre 1 e 60, mínimo R$50/parcela
/// </summary>
public static class CalculoDesenrolaService
{
    private const decimal ParcelaMinima     = 50m;
    private const int     MaxParcelas       = 60;
    private const decimal ParcelaReferencia = 200m;

    /// <summary>Calcula e aplica o resultado à simulação.</summary>
    public static void Calcular(Simulacao simulacao)
    {
        var valorDesconto     = simulacao.DividaOriginal * simulacao.TempoAtraso.PercentualDesconto;
        var saldoAposDesconto = simulacao.DividaOriginal - valorDesconto;

        var fgtsUtilizado = CalcularAbatimentoFgts(simulacao.SaldoFgts, saldoAposDesconto);
        var saldoAPagar   = saldoAposDesconto - fgtsUtilizado;

        var (parcelaEstimada, numeroParcelas) = CalcularParcelas(saldoAPagar);

        simulacao.AplicarCalculo(
            valorDesconto,
            fgtsUtilizado,
            saldoAposDesconto,
            saldoAPagar,
            parcelaEstimada,
            numeroParcelas);
    }

    // ── Métodos privados ──────────────────────────────────────────────────────

    private static ValorMonetario CalcularAbatimentoFgts(
        ValorMonetario saldoFgts,
        ValorMonetario saldoDevedor)
    {
        if (saldoFgts.Valor <= 0) return ValorMonetario.Zero;

        // O beneficiário usa o maior entre 20% do saldo e R$ 1.000 fixo,
        // limitado ao próprio saldo FGTS e ao saldo devedor restante
        var limitePercentual = saldoFgts.Valor * 0.20m;
        var limiteFixo       = 1_000m;
        var teto             = Math.Max(limitePercentual, limiteFixo);
        var abatimento       = Math.Min(teto, Math.Min(saldoDevedor.Valor, saldoFgts.Valor));

        return new ValorMonetario(abatimento);
    }

    private static (ValorMonetario parcela, int numeroParcelas) CalcularParcelas(ValorMonetario saldoAPagar)
    {
        if (saldoAPagar.Valor == 0m)
            return (ValorMonetario.Zero, 1);

        var numeroParcelas = (int)Math.Min(
            MaxParcelas,
            Math.Max(1, Math.Ceiling(saldoAPagar.Valor / ParcelaReferencia)));

        var parcela = new ValorMonetario(
            Math.Max(ParcelaMinima, saldoAPagar.Valor / numeroParcelas));

        return (parcela, numeroParcelas);
    }
}
