<template>
  <section id="resultado" class="resultado-section" aria-labelledby="resultado-titulo">
    <div class="resultado-section__inner">

      <article class="resultado-card animate" aria-live="polite">

        <!-- ── Header ──────────────────────────────────────────────────── -->
        <header class="resultado-card__header">
          <span class="resultado-card__check" aria-hidden="true">
            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <polyline points="20 6 9 17 4 12"/>
            </svg>
          </span>
          <h2 id="resultado-titulo" class="heading-2">Simulação concluída</h2>
        </header>

        <!-- ── Grid de métricas ────────────────────────────────────────── -->
        <div class="metrics-grid" role="list" aria-label="Resumo da simulação">

          <div class="metric-card" role="listitem">
            <span class="label-sm">Dívida original</span>
            <span class="metric-card__value metric-card__value--danger numeric">
              {{ formatCurrency(displayed.dividaOriginal) }}
            </span>
          </div>

          <div class="metric-card" role="listitem">
            <span class="label-sm">Você economiza</span>
            <span class="metric-card__value metric-card__value--amber numeric">
              {{ formatCurrency(displayed.valorDesconto) }}
            </span>
            <span class="badge badge--highlight" aria-label="Percentual de desconto">
              {{ formatPercent(percentualDesconto) }}
            </span>
          </div>

          <div class="metric-card metric-card--featured" role="listitem">
            <span class="label-sm">Saldo a pagar</span>
            <span class="metric-card__value metric-card__value--success numeric">
              {{ formatCurrency(displayed.saldoAPagar) }}
            </span>
            <span class="metric-card__sub body-sm">
              em até {{ numeroParcelas }}x
            </span>
          </div>

        </div>

        <!-- ── Breakdown detalhado ─────────────────────────────────────── -->
        <dl class="breakdown" aria-label="Detalhes do cálculo">

          <div class="breakdown__row">
            <dt class="breakdown__label">
              <BreakdownIcon type="percent" />
              Desconto aplicado
            </dt>
            <dd class="breakdown__value numeric">{{ formatPercent(percentualDesconto) }}</dd>
          </div>

          <div class="breakdown__row">
            <dt class="breakdown__label">
              <BreakdownIcon type="tag" />
              Valor do desconto
            </dt>
            <dd class="breakdown__value numeric">{{ formatCurrency(valorDesconto) }}</dd>
          </div>

          <div v-if="fgtsUtilizado > 0" class="breakdown__row">
            <dt class="breakdown__label">
              <BreakdownIcon type="fgts" />
              FGTS utilizado
            </dt>
            <dd class="breakdown__value numeric">{{ formatCurrency(fgtsUtilizado) }}</dd>
          </div>

          <div class="breakdown__row">
            <dt class="breakdown__label">
              <BreakdownIcon type="balance" />
              Saldo após desconto
            </dt>
            <dd class="breakdown__value numeric">{{ formatCurrency(saldoAposDesconto) }}</dd>
          </div>

          <div class="breakdown__row breakdown__row--highlight">
            <dt class="breakdown__label">
              <BreakdownIcon type="installment" />
              Parcela estimada
            </dt>
            <dd class="breakdown__value breakdown__value--primary numeric">
              {{ formatCurrency(parcelaEstimada) }}<span class="breakdown__value-sub">/mês</span>
            </dd>
          </div>

          <div class="breakdown__row">
            <dt class="breakdown__label">
              <BreakdownIcon type="count" />
              Número de parcelas
            </dt>
            <dd class="breakdown__value numeric">{{ numeroParcelas }}x</dd>
          </div>

        </dl>

        <!-- ── Banner de aviso ─────────────────────────────────────────── -->
        <aside class="aviso-banner" role="note" aria-label="Aviso importante">
          <span class="aviso-banner__icon" aria-hidden="true">
            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="8" x2="12" y2="12"/>
              <line x1="12" y1="16" x2="12.01" y2="16"/>
            </svg>
          </span>
          <p class="body-sm aviso-banner__text">
            Estes valores são estimativas baseadas nas regras oficiais.
            Procure seu banco para confirmar as condições.
          </p>
        </aside>

        <!-- ── Ações ───────────────────────────────────────────────────── -->
        <footer class="resultado-card__actions">
          <a
            href="https://www.gov.br/fazenda/desenrola"
            target="_blank"
            rel="noopener noreferrer"
            class="btn btn--primary btn--lg"
          >
            Quero negociar agora
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
              aria-label="abre em nova aba">
              <path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"/>
              <polyline points="15 3 21 3 21 9"/>
              <line x1="10" y1="14" x2="21" y2="3"/>
            </svg>
          </a>
          <button class="btn btn--ghost-brand btn--lg" @click="$emit('reset')">
            Simular novamente
          </button>
        </footer>

      </article>
    </div>
  </section>
</template>

<script setup lang="ts">
import { reactive, onMounted } from 'vue'

// ─── Props — todos os campos chegam já calculados pela API ────────────────────
// O componente não faz mais cálculo próprio: é puramente presentacional.

const props = defineProps<{
  dividaOriginal:     number
  percentualDesconto: number
  valorDesconto:      number
  fgtsUtilizado:      number
  saldoAposDesconto:  number
  saldoAPagar:        number
  parcelaEstimada:    number
  numeroParcelas:     number
}>()

defineEmits<{ reset: [] }>()

// ─── Formatadores ─────────────────────────────────────────────────────────────

function formatCurrency(value: number): string {
  return value.toLocaleString('pt-BR', {
    style: 'currency', currency: 'BRL',
    minimumFractionDigits: 2, maximumFractionDigits: 2,
  })
}

function formatPercent(value: number): string {
  return `${Math.round(value * 100)}%`
}

// ─── Count-up animado ─────────────────────────────────────────────────────────

const DURATION_MS = 800

const displayed = reactive({ dividaOriginal: 0, valorDesconto: 0, saldoAPagar: 0 })

function animateCountUp(key: keyof typeof displayed, target: number, delay = 0): void {
  const start = performance.now() + delay

  function step(now: number): void {
    if (now < start) { requestAnimationFrame(step); return }

    const progress = Math.min((now - start) / DURATION_MS, 1)
    const eased    = 1 - (1 - progress) ** 3

    displayed[key] = eased * target

    if (progress < 1) requestAnimationFrame(step)
    else displayed[key] = target
  }

  requestAnimationFrame(step)
}

onMounted(() => {
  animateCountUp('dividaOriginal', props.dividaOriginal, 100)
  animateCountUp('valorDesconto',  props.valorDesconto,  250)
  animateCountUp('saldoAPagar',    props.saldoAPagar,    400)
})

// ─── Ícones do breakdown ──────────────────────────────────────────────────────

const iconPaths: Record<string, string> = {
  percent:     'M19 5L5 19M9 6.5a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0zM20 17.5a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z',
  tag:         'M20.59 13.41l-7.17 7.17a2 2 0 0 1-2.83 0L2 12V2h10l8.59 8.59a2 2 0 0 1 0 2.82zM7 7h.01',
  fgts:        'M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z',
  balance:     'M12 2v20M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6',
  installment: 'M3 10h18M7 15h1m4 0h1m-7 4h12a3 3 0 0 0 3-3V8a3 3 0 0 0-3-3H6a3 3 0 0 0-3 3v8a3 3 0 0 0 3 3z',
  count:       'M8 6h13M8 12h13M8 18h13M3 6h.01M3 12h.01M3 18h.01',
}

const BreakdownIcon = {
  props: ['type'],
  setup() { return { iconPaths } },
  template: `
    <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15"
      viewBox="0 0 24 24" fill="none" stroke="currentColor"
      stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
      class="breakdown__icon" aria-hidden="true">
      <path :d="iconPaths[type]" />
    </svg>
  `,
}
</script>

<style scoped>
.resultado-section {
  background-color: var(--color-bg);
  padding: var(--space-16) var(--space-6);
}

.resultado-section__inner {
  max-width: 600px;
  margin: 0 auto;
}

.resultado-card {
  background-color: var(--color-bg-elevated);
  border: 1px solid var(--color-border);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-teal-lg);
  overflow: hidden;
  display: flex;
  flex-direction: column;
  gap: 0;
}

@keyframes fade-up {
  from { opacity: 0; transform: translateY(24px); }
  to   { opacity: 1; transform: translateY(0);    }
}

.animate {
  animation: fade-up var(--duration-slow) var(--ease-out-quart) both;
}

@media (prefers-reduced-motion: reduce) {
  .animate { animation: none; }
}

.resultado-card__header {
  display: flex;
  align-items: center;
  gap: var(--space-3);
  padding: var(--space-6) var(--space-6) var(--space-5);
  border-bottom: 1px solid var(--color-border);
}

.resultado-card__check {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 40px;
  border-radius: var(--radius-full);
  background-color: var(--color-success-bg);
  color: var(--color-success);
  flex-shrink: 0;
}

.metrics-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
}

.metric-card {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: var(--space-2);
  padding: var(--space-5) var(--space-5) var(--space-6);
  border-right: 1px solid var(--color-border);
  border-bottom: 1px solid var(--color-border);
}

.metric-card:last-child { border-right: none; }

.metric-card--featured {
  background-color: var(--color-primary-subtle);
  border-color: var(--color-primary-border);
  border-left: 2px solid var(--color-primary);
  border-right: none;
}

.metric-card__value {
  font-family: var(--font-display);
  font-size: var(--text-lg);
  font-weight: var(--weight-bold);
  line-height: 1;
  letter-spacing: var(--tracking-tight);
}

.metric-card__value--danger  { color: var(--color-error); }
.metric-card__value--amber   { color: var(--color-secondary-text); }
.metric-card__value--success { color: var(--color-primary-text); font-size: var(--text-xl); }

.metric-card__sub { color: var(--color-text-tertiary); }

.breakdown {
  padding: var(--space-5) var(--space-6);
  display: flex;
  flex-direction: column;
  gap: 0;
  border-bottom: 1px solid var(--color-border);
}

.breakdown__row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-4);
  padding: var(--space-3) 0;
  border-bottom: 1px solid var(--color-border);
}

.breakdown__row:last-child { border-bottom: none; }

.breakdown__row--highlight {
  background-color: var(--color-primary-subtle);
  margin: 0 calc(-1 * var(--space-6));
  padding: var(--space-3) var(--space-6);
}

.breakdown__label {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  font-family: var(--font-body);
  font-size: var(--text-sm);
  font-weight: var(--weight-medium);
  color: var(--color-text-secondary);
}

.breakdown__icon   { color: var(--color-text-tertiary); flex-shrink: 0; }

.breakdown__value {
  font-family: var(--font-body);
  font-size: var(--text-base);
  font-weight: var(--weight-semibold);
  color: var(--color-text-primary);
  text-align: right;
}

.breakdown__value--primary { color: var(--color-primary-text); font-size: var(--text-md); }

.breakdown__value-sub {
  font-size: var(--text-sm);
  font-weight: var(--weight-regular);
  color: var(--color-text-tertiary);
  margin-left: var(--space-1);
}

.aviso-banner {
  display: flex;
  gap: var(--space-3);
  align-items: flex-start;
  padding: var(--space-4) var(--space-6);
  background-color: var(--color-info-bg);
  border-bottom: 1px solid var(--color-border);
}

.aviso-banner__icon {
  color: var(--color-info);
  flex-shrink: 0;
  margin-top: 1px;
}

.aviso-banner__text {
  color: var(--color-info-text);
  line-height: var(--leading-relaxed);
}

.resultado-card__actions {
  display: flex;
  gap: var(--space-3);
  align-items: center;
  padding: var(--space-6);
  flex-wrap: wrap;
}

.resultado-card__actions .btn--primary { flex: 1; }

@media (max-width: 600px) {
  .resultado-section { padding: var(--space-10) var(--space-4); }

  .metrics-grid { grid-template-columns: 1fr; }

  .metric-card { border-right: none; }

  .metric-card--featured { border-left: 2px solid var(--color-primary); }

  .metric-card__value--success { font-size: var(--text-lg); }

  .resultado-card__actions {
    flex-direction: column;
    align-items: stretch;
    padding: var(--space-5) var(--space-4);
  }

  .resultado-card__actions .btn { width: 100%; justify-content: center; }

  .resultado-card__actions .btn--primary { flex: unset; }

  .breakdown { padding: var(--space-4); }

  .breakdown__row--highlight {
    margin: 0 calc(-1 * var(--space-4));
    padding: var(--space-3) var(--space-4);
  }

  .aviso-banner { padding: var(--space-4); }

  .resultado-card__header { padding: var(--space-5) var(--space-4); }
}
</style>
