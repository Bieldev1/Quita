import { ref } from 'vue'
import { simularDesenrola, type SimulacaoPayload, type SimulacaoResultado } from '../services/api'

export function useSimulacao() {
  const resultado = ref<SimulacaoResultado | null>(null)
  const isLoading = ref(false)
  const erro      = ref<string | null>(null)

  async function executar(payload: SimulacaoPayload): Promise<void> {
    isLoading.value = true
    erro.value      = null

    try {
      resultado.value = await simularDesenrola(payload)
    } catch (e) {
      // Mensagem já está em português vinda do service — apenas repassa
      erro.value = e instanceof Error
        ? e.message
        : 'Ocorreu um erro inesperado. Tente novamente.'
    } finally {
      isLoading.value = false
    }
  }

  function resetar(): void {
    resultado.value = null
    erro.value      = null
  }

  return { resultado, isLoading, erro, executar, resetar }
}
