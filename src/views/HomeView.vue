<template>
  <div>
    <HeroSection />

    <SimuladorForm @submit="onSimulacaoSubmit" />

    <!-- Monta apenas após a API retornar com sucesso -->
    <Transition name="fade">
      <ResultadoSimulacao
        v-if="simulacaoData"
        :divida-original="simulacaoData.dividaOriginal"
        :percentual-desconto="simulacaoData.percentualDesconto"
        :valor-desconto="simulacaoData.valorDesconto"
        :fgts-utilizado="simulacaoData.fgtsUtilizado"
        :saldo-apos-desconto="simulacaoData.saldoAposDesconto"
        :saldo-a-pagar="simulacaoData.saldoAPagar"
        :parcela-estimada="simulacaoData.parcelaEstimada"
        :numero-parcelas="simulacaoData.numeroParcelas"
        @reset="onReset"
      />
    </Transition>

    <ComoFunciona />
    <FaqSection />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { SimulacaoResultado } from '../../src/services/api'

import HeroSection        from '../../components/HeroSection.vue'
import SimuladorForm      from '../../components/SimuladorForm.vue'
import ResultadoSimulacao from '../../components/ResultadoSimulacao.vue'
import ComoFunciona       from '../../components/ComoFunciona.vue'
import FaqSection         from '../../components/FaqSection.vue'

// ─── Estado ───────────────────────────────────────────────────────────────────

// Armazena o resultado já calculado pela API — null = resultado não exibido
const simulacaoData = ref<SimulacaoResultado | null>(null)

// ─── Handlers ─────────────────────────────────────────────────────────────────

function onSimulacaoSubmit(resultado: SimulacaoResultado): void {
  simulacaoData.value = resultado

  // Aguarda o v-if montar o ResultadoSimulacao antes de rolar
  setTimeout(() => {
    document.getElementById('resultado')?.scrollIntoView({ behavior: 'smooth', block: 'start' })
  }, 100)
}

function onReset(): void {
  simulacaoData.value = null

  setTimeout(() => {
    document.getElementById('simulador')?.scrollIntoView({ behavior: 'smooth', block: 'start' })
  }, 100)
}
</script>

<style>
/* Não-scoped: classes .fade-* são injetadas pelo Vue no elemento raiz da Transition */

.fade-enter-active,
.fade-leave-active {
  transition:
    opacity   350ms cubic-bezier(0.25, 1, 0.5, 1),
    transform 350ms cubic-bezier(0.25, 1, 0.5, 1);
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
  transform: translateY(16px);
}
</style>
