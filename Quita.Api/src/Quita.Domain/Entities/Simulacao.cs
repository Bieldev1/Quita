using Quita.Domain.Exceptions;
using Quita.Domain.ValueObjects;

namespace Quita.Domain.Entities;

/// <summary>
/// Aggregate Root que representa uma simulação de renegociação.
/// Encapsula as invariantes de negócio: só permite criar simulações
/// com dados válidos e aplica o cálculo uma única vez (imutabilidade pós-cálculo).
/// </summary>
public sealed class Simulacao
{
    // ── Limites do Programa Desenrola Brasil ──────────────────────────────────
    private const decimal ValorMinimoPermitido = 100m;
    private const decimal ValorMaximoPermitido = 15_000m;

    // ── Dados de entrada ──────────────────────────────────────────────────────
    public ValorMonetario DividaOriginal { get; }
    public TempoAtraso    TempoAtraso    { get; }
    public TipoDivida     TipoDivida     { get; }
    public ValorMonetario SaldoFgts      { get; }

    // ── Resultado do cálculo (preenchido pelo Domain Service) ─────────────────
    public ValorMonetario? ValorDesconto     { get; private set; }
    public ValorMonetario? FgtsUtilizado     { get; private set; }
    public ValorMonetario? SaldoAposDesconto { get; private set; }
    public ValorMonetario? SaldoAPagar       { get; private set; }
    public ValorMonetario? ParcelaEstimada   { get; private set; }
    public int             NumeroParcelas    { get; private set; }
    public bool            CalculoAplicado   { get; private set; }

    // Construtor privado — criação controlada pela factory method
    private Simulacao(
        ValorMonetario dividaOriginal,
        TempoAtraso    tempoAtraso,
        TipoDivida     tipoDivida,
        ValorMonetario saldoFgts)
    {
        DividaOriginal = dividaOriginal;
        TempoAtraso    = tempoAtraso;
        TipoDivida     = tipoDivida;
        SaldoFgts      = saldoFgts;
    }

    /// <summary>
    /// Factory method — único ponto de criação. Valida todas as invariantes
    /// antes de construir o objeto, garantindo que nenhuma instância inválida exista.
    /// </summary>
    public static Simulacao Criar(
        decimal valorDivida,
        string  tempoAtrasoCodigo,
        string  tipoDividaCodigo,
        decimal saldoFgts = 0m)
    {
        // Validações de domínio — DomainException capturada na camada de API
        if (valorDivida < ValorMinimoPermitido || valorDivida > ValorMaximoPermitido)
            throw new DomainException(
                $"Valor da dívida deve estar entre R$ {ValorMinimoPermitido:N2} e R$ {ValorMaximoPermitido:N2}.",
                "valorDivida");

        if (saldoFgts < 0 || saldoFgts > ValorMaximoPermitido)
            throw new DomainException(
                $"Saldo do FGTS deve estar entre R$ 0,00 e R$ {ValorMaximoPermitido:N2}.",
                "saldoFgts");

        // Value Objects validam o formato (lançam DomainException se inválidos)
        var tempoAtraso = TempoAtraso.FromCodigo(tempoAtrasoCodigo);
        var tipoDivida  = TipoDivida.FromCodigo(tipoDividaCodigo);

        return new Simulacao(
            new ValorMonetario(valorDivida),
            tempoAtraso,
            tipoDivida,
            new ValorMonetario(saldoFgts));
    }

    /// <summary>
    /// Aplica o resultado calculado pelo Domain Service.
    /// Idempotência garantida: lança se já foi calculado.
    /// </summary>
    internal void AplicarCalculo(
        ValorMonetario valorDesconto,
        ValorMonetario fgtsUtilizado,
        ValorMonetario saldoAposDesconto,
        ValorMonetario saldoAPagar,
        ValorMonetario parcelaEstimada,
        int            numeroParcelas)
    {
        if (CalculoAplicado)
            throw new InvalidOperationException("O cálculo já foi aplicado a esta simulação.");

        ValorDesconto     = valorDesconto;
        FgtsUtilizado     = fgtsUtilizado;
        SaldoAposDesconto = saldoAposDesconto;
        SaldoAPagar       = saldoAPagar;
        ParcelaEstimada   = parcelaEstimada;
        NumeroParcelas    = numeroParcelas;
        CalculoAplicado   = true;
    }
}
