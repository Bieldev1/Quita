<template>
  <section id="simulador" class="simulador-section">
    <div class="simulador-section__inner">

      <!-- Cabeçalho da seção -->
      <div class="simulador-section__header">
        <span class="label-sm">Calculadora gratuita</span>
        <h2 class="heading-1">Simule sua renegociação</h2>
      </div>

      <!-- Card do formulário -->
      <div class="form-card">

        <!-- Banner de erro da API — aparece quando a chamada falha -->
        <div
          v-if="erro"
          class="aviso-banner aviso-banner--error"
          role="alert"
          aria-live="assertive"
        >
          <span class="aviso-banner__icon" aria-hidden="true">
            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="8" x2="12" y2="12"/>
              <line x1="12" y1="16" x2="12.01" y2="16"/>
            </svg>
          </span>
          <p class="body-sm aviso-banner__text">{{ erro }}</p>
        </div>

        <form
          novalidate
          @submit.prevent="handleSubmit"
          aria-label="Formulário de simulação do Desenrola Brasil"
        >

          <!-- ── Campo: Valor da dívida ──────────────────────────────────── -->
          <div :class="['field', errors.valorDivida && 'field--error']">
            <div class="field__wrap">
              <input
                id="valorDivida"
                ref="valorDividaRef"
                class="field__input"
                type="text"
                inputmode="numeric"
                autocomplete="off"
                placeholder=" "
                :value="masks.valorDivida"
                :aria-invalid="!!errors.valorDivida"
                :aria-describedby="errors.valorDivida ? 'err-valorDivida' : undefined"
                @input="onCurrencyInput('valorDivida', $event)"
              />
              <label class="field__label" for="valorDivida">
                Valor total da dívida
              </label>
            </div>
            <span
              v-if="errors.valorDivida"
              id="err-valorDivida"
              class="field__hint"
              role="alert"
            >
              {{ errors.valorDivida }}
            </span>
          </div>

          <!-- ── Campo: Tempo de atraso (select customizado) ────────────── -->
          <div :class="['field', errors.tempoAtraso && 'field--error']">
            <div class="field__wrap select-wrap">
              <select
                id="tempoAtraso"
                ref="tempoAtrasoRef"
                class="field__input field__select"
                v-model="form.tempoAtraso"
                :aria-invalid="!!errors.tempoAtraso"
                :aria-describedby="errors.tempoAtraso ? 'err-tempoAtraso' : undefined"
              >
                <option value="" disabled hidden></option>
                <option value="90-180">90 a 180 dias</option>
                <option value="181-365">181 a 365 dias</option>
                <option value="365+">Mais de 365 dias</option>
              </select>
              <label
                class="field__label"
                :class="form.tempoAtraso ? 'field__label--floated' : ''"
                for="tempoAtraso"
              >
                Tempo de atraso
              </label>
              <span class="select-arrow" aria-hidden="true">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                  viewBox="0 0 24 24" fill="none" stroke="currentColor"
                  stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                  <polyline points="6 9 12 15 18 9"/>
                </svg>
              </span>
            </div>
            <span
              v-if="errors.tempoAtraso"
              id="err-tempoAtraso"
              class="field__hint"
              role="alert"
            >
              {{ errors.tempoAtraso }}
            </span>
          </div>

          <!-- ── Tipo de dívida (radio group) ───────────────────────────── -->
          <fieldset
            class="radio-group"
            :class="errors.tipoDivida && 'field--error'"
            :aria-describedby="errors.tipoDivida ? 'err-tipoDivida' : undefined"
          >
            <legend class="radio-group__legend">Tipo de dívida</legend>
            <div class="radio-group__options">
              <label
                v-for="opcao in tiposDivida"
                :key="opcao.value"
                class="radio-option"
                :class="form.tipoDivida === opcao.value && 'radio-option--selected'"
              >
                <input
                  type="radio"
                  class="radio-option__input"
                  name="tipoDivida"
                  :value="opcao.value"
                  v-model="form.tipoDivida"
                  :aria-invalid="!!errors.tipoDivida"
                />
                <span class="radio-option__dot" aria-hidden="true"></span>
                <span class="radio-option__label">{{ opcao.label }}</span>
              </label>
            </div>
            <span
              v-if="errors.tipoDivida"
              id="err-tipoDivida"
              class="field__hint"
              role="alert"
            >
              {{ errors.tipoDivida }}
            </span>
          </fieldset>

          <!-- ── Campo: Saldo FGTS (opcional) ──────────────────────────── -->
          <div :class="['field', errors.saldoFgts && 'field--error']">
            <div class="field__wrap">
              <input
                id="saldoFgts"
                class="field__input"
                type="text"
                inputmode="numeric"
                autocomplete="off"
                placeholder=" "
                :value="masks.saldoFgts"
                :aria-invalid="!!errors.saldoFgts"
                :aria-describedby="errors.saldoFgts ? 'err-saldoFgts' : 'hint-saldoFgts'"
                @input="onCurrencyInput('saldoFgts', $event)"
              />
              <label class="field__label" for="saldoFgts">
                Saldo disponível no FGTS
              </label>
            </div>
            <span
              v-if="errors.saldoFgts"
              id="err-saldoFgts"
              class="field__hint"
              role="alert"
            >
              {{ errors.saldoFgts }}
            </span>
            <span
              v-else
              id="hint-saldoFgts"
              class="field__hint field__hint--neutral"
            >
              Opcional — consulte o app FGTS
            </span>
          </div>

          <!-- ── Submit ─────────────────────────────────────────────────── -->
          <button
            type="submit"
            class="btn btn--primary btn--lg btn--full"
            :disabled="isLoading"
            :aria-busy="isLoading"
          >
            <template v-if="isLoading">
              <svg
                class="spinner"
                xmlns="http://www.w3.org/2000/svg"
                width="20" height="20"
                viewBox="0 0 24 24"
                fill="none" stroke="currentColor"
                stroke-width="2.5" stroke-linecap="round"
                aria-hidden="true"
              >
                <path d="M21 12a9 9 0 1 1-6.219-8.56"/>
              </svg>
              Calculando...
            </template>
            <template v-else>
              Calcular minha economia
            </template>
          </button>

        </form>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useCalculoDesenrola } from '../composables/useCalculoDesenrola'
import type { SimulacaoResultado } from '../src/services/api'

// ─── Emit — entrega o resultado já calculado ──────────────────────────────────

const emit = defineEmits<{
  submit: [resultado: SimulacaoResultado]
}>()

// ─── Loading local (sem chamada de API por enquanto) ─────────────────────────

const isLoading = ref(false)
const erro      = ref<string | null>(null)

// ─── Opções ───────────────────────────────────────────────────────────────────

const tiposDivida = [
  { value: 'cartao',  label: 'Cartão de crédito' },
  { value: 'cheque',  label: 'Cheque especial'   },
  { value: 'pessoal', label: 'Crédito pessoal'   },
]

// ─── Estado do formulário ─────────────────────────────────────────────────────

const form = reactive({ tempoAtraso: '', tipoDivida: '' })

const raw = reactive<{ valorDivida: number | null; saldoFgts: number | null }>({
  valorDivida: null,
  saldoFgts:   null,
})

const masks = reactive({ valorDivida: '', saldoFgts: '' })

const errors = reactive({ valorDivida: '', tempoAtraso: '', tipoDivida: '', saldoFgts: '' })

const valorDividaRef = ref<HTMLInputElement | null>(null)
const tempoAtrasoRef = ref<HTMLSelectElement | null>(null)

// ─── Máscara de moeda ─────────────────────────────────────────────────────────

function formatCurrency(digits: string): string {
  if (!digits) return ''
  return (parseInt(digits, 10) / 100).toLocaleString('pt-BR', {
    style: 'currency', currency: 'BRL',
    minimumFractionDigits: 2, maximumFractionDigits: 2,
  })
}

function onCurrencyInput(field: 'valorDivida' | 'saldoFgts', event: Event): void {
  const input  = event.target as HTMLInputElement
  const digits = input.value.replace(/\D/g, '')

  if (!digits) {
    masks[field] = ''
    raw[field]   = null
    input.value  = ''
    return
  }

  masks[field]  = formatCurrency(digits)
  raw[field]    = parseInt(digits, 10) / 100
  input.value   = masks[field]

  requestAnimationFrame(() => {
    input.selectionStart = input.selectionEnd = input.value.length
  })
}

// ─── Validação (frontend — espelha as regras da API) ─────────────────────────

const LIMITE = 15_000
const MINIMO = 100

function validate(): boolean {
  Object.keys(errors).forEach(k => ((errors as Record<string, string>)[k] = ''))
  let valid = true

  if (!raw.valorDivida) {
    errors.valorDivida = 'Informe o valor total da dívida.'
    valid = false
  } else if (raw.valorDivida < MINIMO) {
    errors.valorDivida = `O valor mínimo é R$ ${MINIMO},00.`
    valid = false
  } else if (raw.valorDivida > LIMITE) {
    errors.valorDivida = `O Desenrola cobre dívidas de até R$ ${LIMITE.toLocaleString('pt-BR')},00.`
    valid = false
  }

  if (!form.tempoAtraso) { errors.tempoAtraso = 'Selecione o tempo de atraso da dívida.'; valid = false }
  if (!form.tipoDivida)  { errors.tipoDivida  = 'Selecione o tipo de dívida.';            valid = false }

  if (raw.saldoFgts && raw.saldoFgts > LIMITE) {
    errors.saldoFgts = `O saldo informado excede o limite de R$ ${LIMITE.toLocaleString('pt-BR')},00.`
    valid = false
  }

  return valid
}

// ─── Submit ───────────────────────────────────────────────────────────────────

async function handleSubmit(): Promise<void> {
  if (!validate()) { scrollToFirstError(); return }

  isLoading.value = true
  erro.value      = null

  // Simula latência mínima para o spinner aparecer (feedback visual)
  await new Promise(r => setTimeout(r, 600))

  try {
    // Cálculo local — mesmas regras do backend, sem depender da API
    const calculo = useCalculoDesenrola(
      raw.valorDivida!,
      form.tempoAtraso,
      raw.saldoFgts ?? 0,
    )

    // Converte para o shape de SimulacaoResultado (mesmo contrato da API)
    const resultado: SimulacaoResultado = {
      dividaOriginal:     calculo.dividaOriginal,
      percentualDesconto: calculo.percentualDesconto,
      valorDesconto:      calculo.valorDesconto,
      fgtsUtilizado:      calculo.fgtsUtilizado,
      saldoAposDesconto:  calculo.saldoAposDesconto,
      saldoAPagar:        calculo.saldoAPagar,
      parcelaEstimada:    calculo.parcelaEstimada,
      numeroParcelas:     calculo.numeroParcelas,
    }

    emit('submit', resultado)
  } finally {
    isLoading.value = false
  }
}

// ─── Scroll ao primeiro erro de validação frontend ───────────────────────────

function scrollToFirstError(): void {
  const ordem = ['valorDivida', 'tempoAtraso', 'tipoDivida', 'saldoFgts'] as const

  for (const campo of ordem) {
    if (!errors[campo]) continue

    const el: HTMLElement | null =
      campo === 'valorDivida' ? valorDividaRef.value
      : campo === 'tempoAtraso' ? tempoAtrasoRef.value
      : document.getElementById(campo)

    if (el) {
      el.scrollIntoView({ behavior: 'smooth', block: 'center' })
      el.focus({ preventScroll: true })
      break
    }
  }
}
</script>

<style scoped>
/* ─── Seção ──────────────────────────────────────────────────────────────────*/

.simulador-section {
  background-color: var(--color-bg-subtle);
  padding: var(--space-16) var(--space-6);
}

.simulador-section__inner {
  max-width: var(--container-md);
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--space-10);
}

.simulador-section__header {
  text-align: center;
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
}

.simulador-section__header .label-sm {
  color: var(--color-primary-text);
}

/* ─── Card do formulário ──────────────────────────────────────────────────────*/

.form-card {
  width: 100%;
  max-width: 560px;
  background-color: var(--color-bg-elevated);
  border: 1px solid var(--color-border);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-md);
  overflow: hidden; /* banner de erro encosta nas bordas do card */
}

form {
  display: flex;
  flex-direction: column;
  gap: var(--space-5);
  padding: var(--space-8);
}

/* ─── Banner de erro da API ──────────────────────────────────────────────────*/

.aviso-banner {
  display: flex;
  gap: var(--space-3);
  align-items: flex-start;
  padding: var(--space-4) var(--space-6);
  border-bottom: 1px solid var(--color-border);
}

.aviso-banner--error {
  background-color: var(--color-error-bg);
}

.aviso-banner--error .aviso-banner__icon {
  color: var(--color-error);
  flex-shrink: 0;
  margin-top: 1px;
}

.aviso-banner--error .aviso-banner__text {
  color: var(--color-error-text);
  line-height: var(--leading-relaxed);
}

/* ─── Hints e erros de campo ─────────────────────────────────────────────────*/

.field__hint--neutral {
  color: var(--color-text-tertiary);
}

/* ─── Select customizado ─────────────────────────────────────────────────────*/

.field__select {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  padding-right: var(--space-10);
  cursor: pointer;
}

.field__label--floated {
  top: var(--space-3) !important;
  transform: translateY(0) scale(0.78) !important;
  color: var(--color-primary) !important;
  font-weight: var(--weight-semibold) !important;
}

.select-arrow {
  position: absolute;
  right: var(--space-4);
  top: 50%;
  transform: translateY(-50%);
  color: var(--color-text-tertiary);
  pointer-events: none;
  display: flex;
  align-items: center;
  transition: color var(--duration-normal) var(--ease-out-quart),
              transform var(--duration-normal) var(--ease-out-quart);
}

.field__select:focus ~ .select-arrow {
  color: var(--color-primary);
  transform: translateY(-50%) rotate(180deg);
}

/* ─── Radio group ────────────────────────────────────────────────────────────*/

.radio-group {
  border: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: var(--space-3);
}

.radio-group__legend {
  font-family: var(--font-body);
  font-size: var(--text-base);
  font-weight: var(--weight-regular);
  color: var(--color-text-tertiary);
  float: left;
  width: 100%;
  margin-bottom: var(--space-3);
  & + * { clear: both; }
}

.radio-group.field--error .radio-group__legend {
  color: var(--color-error-text);
}

.radio-group__options {
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
}

.radio-option {
  display: flex;
  align-items: center;
  gap: var(--space-3);
  padding: var(--space-3) var(--space-4);
  border: 1.5px solid var(--color-border);
  border-radius: var(--radius-md);
  cursor: pointer;
  transition: var(--transition-base);
  position: relative;
  background-color: var(--color-bg);
}

.radio-option:hover {
  border-color: var(--color-primary-border);
  background-color: var(--color-primary-subtle);
}

.radio-option--selected {
  border-color: var(--color-primary);
  background-color: var(--color-primary-subtle);
}

.radio-option__input {
  position: absolute;
  opacity: 0;
  width: 0;
  height: 0;
  margin: 0;
}

.radio-option__input:focus-visible ~ .radio-option__dot {
  outline: 2px solid var(--color-focus-ring);
  outline-offset: 2px;
}

.radio-option__dot {
  width: 18px;
  height: 18px;
  border-radius: var(--radius-full);
  border: 2px solid var(--color-border-strong);
  flex-shrink: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: var(--transition-base);
  background-color: var(--color-bg-elevated);
}

.radio-option__dot::after {
  content: '';
  width: 8px;
  height: 8px;
  border-radius: var(--radius-full);
  background-color: var(--color-primary);
  opacity: 0;
  transform: scale(0);
  transition:
    opacity   var(--duration-fast) var(--ease-out-quart),
    transform var(--duration-normal) var(--ease-spring);
}

.radio-option--selected .radio-option__dot {
  border-color: var(--color-primary);
  background-color: var(--color-primary-subtle);
}

.radio-option--selected .radio-option__dot::after {
  opacity: 1;
  transform: scale(1);
}

.radio-option__label {
  font-family: var(--font-body);
  font-size: var(--text-base);
  font-weight: var(--weight-medium);
  color: var(--color-text-primary);
  line-height: 1;
}

.radio-group.field--error .radio-option {
  border-color: color-mix(in srgb, var(--color-error) 40%, var(--color-border));
}

.radio-group.field--error .radio-option:hover {
  border-color: var(--color-error);
}

/* ─── Spinner ────────────────────────────────────────────────────────────────*/

@keyframes spin {
  to { transform: rotate(360deg); }
}

.spinner {
  animation: spin 0.75s linear infinite;
  flex-shrink: 0;
}

/* ─── Mobile ─────────────────────────────────────────────────────────────────*/

@media (max-width: 640px) {
  .simulador-section {
    padding: var(--space-10) var(--space-4);
  }

  form {
    padding: var(--space-6) var(--space-4);
  }

  .aviso-banner {
    padding: var(--space-3) var(--space-4);
  }
}
</style>
