using Quita.Domain.Entities;
using Quita.Domain.Services;

namespace Quita.Application.UseCases.SimularDesenrola;

/// <summary>
/// Orquestra o fluxo do caso de uso:
///   1. Cria o aggregate Simulacao (com validações de domínio)
///   2. Delega o cálculo ao Domain Service
///   3. Mapeia o resultado para o DTO de saída
///
/// Não contém lógica de negócio — apenas coordenação.
/// </summary>
public sealed class SimularDesenrolaUseCase : ISimularDesenrolaUseCase
{
    public SimularDesenrolaResult Executar(SimularDesenrolaCommand command)
    {
        // 1. Cria o aggregate — lança DomainException se dados inválidos
        var simulacao = Simulacao.Criar(
            command.ValorDivida,
            command.TempoAtraso,
            command.TipoDivida,
            command.SaldoFgts);

        // 2. Aplica as regras de negócio via Domain Service
        CalculoDesenrolaService.Calcular(simulacao);

        // 3. Projeta para o DTO de saída — sem expor internos do domínio
        return new SimularDesenrolaResult(
            DividaOriginal:     simulacao.DividaOriginal.Valor,
            PercentualDesconto: simulacao.TempoAtraso.PercentualDesconto,
            ValorDesconto:      simulacao.ValorDesconto!.Valor,
            FgtsUtilizado:      simulacao.FgtsUtilizado!.Valor,
            SaldoAposDesconto:  simulacao.SaldoAposDesconto!.Valor,
            SaldoAPagar:        simulacao.SaldoAPagar!.Valor,
            ParcelaEstimada:    simulacao.ParcelaEstimada!.Valor,
            NumeroParcelas:     simulacao.NumeroParcelas);
    }
}
