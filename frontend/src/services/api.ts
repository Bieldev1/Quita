import { API_URL } from '../config/env'

// ─── Contratos de tipo ────────────────────────────────────────────────────────
// Espelham exatamente os records do backend — nenhum campo extra, nenhum `any`

export interface SimulacaoPayload {
  valorDivida: number
  tempoAtraso: string
  saldoFgts:   number
  tipoDivida:  string
}

export interface SimulacaoResultado {
  dividaOriginal:     number
  percentualDesconto: number   // 0–1
  valorDesconto:      number
  fgtsUtilizado:      number
  saldoAposDesconto:  number
  saldoAPagar:        number
  parcelaEstimada:    number
  numeroParcelas:     number
}

// Shape do corpo de erro 400 retornado pelo ASP.NET (RFC 7807 ValidationProblem)
interface ValidationProblemDetails {
  errors: Record<string, string[]>
  title?: string
}

function isValidationProblem(body: unknown): body is ValidationProblemDetails {
  return (
    typeof body === 'object' &&
    body !== null &&
    'errors' in body &&
    typeof (body as Record<string, unknown>).errors === 'object'
  )
}

// ─── Função principal ─────────────────────────────────────────────────────────

export async function simularDesenrola(
  payload: SimulacaoPayload,
): Promise<SimulacaoResultado> {
  let response: Response

  try {
    response = await fetch(`${API_URL}/api/simular`, {
      method:  'POST',
      headers: { 'Content-Type': 'application/json' },
      body:    JSON.stringify(payload),
    })
  } catch {
    // Erro de rede: sem conexão, CORS bloqueado, servidor offline
    throw new Error(
      'Não foi possível conectar ao servidor. Verifique sua conexão e tente novamente.',
    )
  }

  if (response.status === 400) {
    // Extrai mensagens de validação do backend e monta texto legível
    const body: unknown = await response.json().catch(() => null)

    if (isValidationProblem(body)) {
      const mensagens = Object.values(body.errors).flat().join(' ')
      throw new Error(mensagens || 'Os dados informados são inválidos. Verifique e tente novamente.')
    }

    throw new Error('Os dados informados são inválidos. Verifique e tente novamente.')
  }

  if (!response.ok) {
    throw new Error(
      `Ocorreu um erro inesperado (código ${response.status}). Tente novamente em instantes.`,
    )
  }

  return response.json() as Promise<SimulacaoResultado>
}
