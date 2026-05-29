// composables/useCalculoDesenrola.ts
// Lógica oficial do Novo Desenrola Brasil — isolada para facilitar testes

export type TempoAtraso = '90-180' | '181-365' | '365+'
export type TipoDivida  = 'cartao' | 'cheque' | 'pessoal'

export interface ResultadoCalculo {
  dividaOriginal:    number
  percentualDesconto:number   // 0–1
  valorDesconto:     number
  fgtsUtilizado:     number
  saldoAposDesconto: number
  saldoAPagar:       number
  parcelaEstimada:   number
  numeroParcelas:    number
}

// Desconto base por tempo de atraso (regra oficial)
const DESCONTO_BASE: Record<TempoAtraso, number> = {
  '90-180':  0.30,
  '181-365': 0.45,
  '365+':    0.53,
}

const PARCELA_MINIMA  = 50
const MAX_PARCELAS    = 60
const PARCELA_REFERENCIA = 200   // base para calcular número de parcelas sugerido

export function useCalculoDesenrola(
  valorDivida:  number,
  tempoAtraso:  string,
  saldoFgts:    number,
): ResultadoCalculo {
  const percentualDesconto = DESCONTO_BASE[tempoAtraso as TempoAtraso] ?? 0.30

  const valorDesconto     = valorDivida * percentualDesconto
  const saldoAposDesconto = valorDivida - valorDesconto

  // FGTS: abate até 20% do saldo FGTS ou R$ 1.000 (o que for maior),
  // limitado ao saldo devedor restante
  let fgtsUtilizado = 0
  if (saldoFgts > 0) {
    const limitePercentual = saldoFgts * 0.20
    const limiteFixo       = 1000
    // O que for maior entre os dois limites, não ultrapassar o saldo devedor
    fgtsUtilizado = Math.min(
      Math.max(limitePercentual, limiteFixo),
      saldoAposDesconto,
      saldoFgts,
    )
  }

  const saldoAPagar = Math.max(0, saldoAposDesconto - fgtsUtilizado)

  // Parcelas: sugerido por R$ 200/mês, entre 1 e 60, parcela mínima R$ 50
  const numeroParcelas = saldoAPagar === 0
    ? 1
    : Math.min(MAX_PARCELAS, Math.max(1, Math.ceil(saldoAPagar / PARCELA_REFERENCIA)))

  const parcelaEstimada = saldoAPagar === 0
    ? 0
    : Math.max(PARCELA_MINIMA, saldoAPagar / numeroParcelas)

  return {
    dividaOriginal:    valorDivida,
    percentualDesconto,
    valorDesconto,
    fgtsUtilizado,
    saldoAposDesconto,
    saldoAPagar,
    parcelaEstimada,
    numeroParcelas,
  }
}
