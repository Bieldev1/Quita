using Quita.Api.Models;

namespace Quita.Api.Services;

public interface ICalculoDesenrolaService
{
    SimulacaoResponse Calcular(SimulacaoRequest request);
}

public sealed class CalculoDesenrolaService : ICalculoDesenrolaService
{
    // ── Regras oficiais do Novo Desenrola Brasil ──────────────────────────────

    // Percentual de desconto por tempo de atraso (fonte: Resolução CMN 5.108/2023)
    private static readonly Dictionary<string, decimal> DescontoBase = new()
    {
        ["90-180"]  = 0.30m,
        ["181-365"] = 0.45m,
        ["365+"]    = 0.53m,
    };

    private const decimal ParcelaMinima     = 50m;
    private const decimal MaxParcelas       = 60m;
    private const decimal ParcelaReferencia = 200m;   // base para sugerir nº de parcelas

    public SimulacaoResponse Calcular(SimulacaoRequest request)
    {
        var percentualDesconto = DescontoBase[request.TempoAtraso];

        var valorDesconto     = Round(request.ValorDivida * percentualDesconto);
        var saldoAposDesconto = Round(request.ValorDivida - valorDesconto);

        // ── FGTS ─────────────────────────────────────────────────────────────
        // Regra: o beneficiário pode usar o maior entre:
        //   • 20% do saldo disponível no FGTS
        //   • R$ 1.000 fixo
        // Limitado ao saldo devedor restante e ao próprio saldo FGTS.
        var fgtsUtilizado = 0m;
        if (request.SaldoFgts > 0)
        {
            var limitePercentual = request.SaldoFgts * 0.20m;
            var limiteFixo       = 1_000m;
            var teto             = Math.Max(limitePercentual, limiteFixo);

            fgtsUtilizado = Round(Math.Min(teto, Math.Min(saldoAposDesconto, request.SaldoFgts)));
        }

        var saldoAPagar = Round(Math.Max(0m, saldoAposDesconto - fgtsUtilizado));

        // ── Parcelas ──────────────────────────────────────────────────────────
        // Sugestão: ceil(saldo / R$200), entre 1 e 60, parcela mínima R$50.
        int numeroParcelas;
        decimal parcelaEstimada;

        if (saldoAPagar == 0m)
        {
            numeroParcelas  = 1;
            parcelaEstimada = 0m;
        }
        else
        {
            numeroParcelas = (int)Math.Min(
                MaxParcelas,
                Math.Max(1, Math.Ceiling(saldoAPagar / ParcelaReferencia))
            );
            parcelaEstimada = Round(Math.Max(ParcelaMinima, saldoAPagar / numeroParcelas));
        }

        return new SimulacaoResponse(
            DividaOriginal:     request.ValorDivida,
            PercentualDesconto: percentualDesconto,
            ValorDesconto:      valorDesconto,
            FgtsUtilizado:      fgtsUtilizado,
            SaldoAposDesconto:  saldoAposDesconto,
            SaldoAPagar:        saldoAPagar,
            ParcelaEstimada:    parcelaEstimada,
            NumeroParcelas:     numeroParcelas
        );
    }

    // Arredonda para 2 casas decimais (padrão financeiro — MidpointRounding.AwayFromZero)
    private static decimal Round(decimal value) =>
        Math.Round(value, 2, MidpointRounding.AwayFromZero);
}
