<template>
  <section id="como-funciona" class="como-funciona" aria-labelledby="como-funciona-titulo">
    <div class="como-funciona__inner">

      <!-- Cabeçalho -->
      <div class="como-funciona__header">
        <span class="badge badge--info">Processo simplificado</span>
        <h2 id="como-funciona-titulo" class="display-2">Como funciona</h2>
      </div>

      <!-- Passos -->
      <ol class="steps" aria-label="Passos do processo">
        <li
          v-for="(passo, index) in passos"
          :key="passo.numero"
          class="step"
          :class="{ 'step--visible': visible[index] }"
          :style="{ '--delay': `${index * 120}ms` }"
          ref="stepRefs"
        >
          <!-- Linha conectora (desktop: à direita; mobile: abaixo) -->
          <div class="step__connector" aria-hidden="true"></div>

          <!-- Ícone numerado -->
          <div class="step__icon-wrap" aria-hidden="true">
            <span class="step__number">{{ passo.numero }}</span>
            <!-- SVG do passo -->
            <component :is="passo.icon" class="step__svg" />
          </div>

          <!-- Texto -->
          <div class="step__body">
            <h3 class="heading-3 step__title">{{ passo.titulo }}</h3>
            <p class="body-base text-secondary step__desc">{{ passo.descricao }}</p>
          </div>
        </li>
      </ol>

    </div>
  </section>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

// ─── Conteúdo dos passos ──────────────────────────────────────────────────────

// Ícones como componentes inline — sem dependência externa
const IconDivida = {
  template: `<svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24"
    fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
    aria-hidden="true">
    <line x1="12" y1="1" x2="12" y2="23"/>
    <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
  </svg>`,
}
const IconCalculo = {
  template: `<svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24"
    fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
    aria-hidden="true">
    <rect x="4" y="2" width="16" height="20" rx="2"/>
    <line x1="8" y1="6" x2="16" y2="6"/><line x1="8" y1="10" x2="16" y2="10"/>
    <line x1="8" y1="14" x2="12" y2="14"/><line x1="15" y1="17" x2="15" y2="23"/>
    <line x1="12" y1="20" x2="18" y2="20"/>
  </svg>`,
}
const IconResultado = {
  template: `<svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24"
    fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
    aria-hidden="true">
    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/>
  </svg>`,
}
const IconNegocie = {
  template: `<svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24"
    fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
    aria-hidden="true">
    <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07
             A19.5 19.5 0 0 1 4.07 12a19.79 19.79 0 0 1-3-8.59A2 2 0 0 1 3 1.18h3a2 2 0 0 1 2
             1.72c.127.96.361 1.903.7 2.81a2 2 0 0 1-.45 2.11L7.09 8.09a16 16 0 0 0 6
             6l1.27-1.27a2 2 0 0 1 2.11-.45c.907.339 1.85.573 2.81.7A2 2 0 0 1 21 15z"/>
  </svg>`,
}

const passos = [
  {
    numero:   '01',
    titulo:   'Informe sua dívida',
    descricao:'Digite o valor, o tempo de atraso e o tipo de dívida.',
    icon:     IconDivida,
  },
  {
    numero:   '02',
    titulo:   'Calculamos o desconto',
    descricao:'Aplicamos as regras oficiais do Novo Desenrola Brasil automaticamente.',
    icon:     IconCalculo,
  },
  {
    numero:   '03',
    titulo:   'Veja o resultado',
    descricao:'Desconto, parcelas e quanto você economiza em segundos.',
    icon:     IconResultado,
  },
  {
    numero:   '04',
    titulo:   'Negocie com seu banco',
    descricao:'Leve a simulação e vá direto à negociação informado.',
    icon:     IconNegocie,
  },
]

// ─── IntersectionObserver — anima cada passo ao entrar no viewport ───────────

const stepRefs = ref([])
// Controla quais passos já são visíveis (índice → boolean)
const visible  = ref(passos.map(() => false))

let observer = null

onMounted(() => {
  observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (!entry.isIntersecting) return
        const index = stepRefs.value.indexOf(entry.target)
        if (index !== -1) {
          visible.value[index] = true
          // Para de observar após animar — cada elemento entra só uma vez
          observer.unobserve(entry.target)
        }
      })
    },
    { threshold: 0.2 },   // 20% visível já dispara
  )

  stepRefs.value.forEach((el) => el && observer.observe(el))
})

onUnmounted(() => observer?.disconnect())
</script>

<style scoped>
/* ─── Seção ──────────────────────────────────────────────────────────────────*/

.como-funciona {
  background-color: var(--color-bg);
  padding: var(--space-16) var(--space-6);
}

.como-funciona__inner {
  max-width: var(--container-xl);
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--space-12);
}

/* ─── Cabeçalho ──────────────────────────────────────────────────────────────*/

.como-funciona__header {
  text-align: center;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--space-3);
}

/* ─── Lista de passos ────────────────────────────────────────────────────────*/

.steps {
  list-style: none;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 0;
  width: 100%;
  position: relative;
}

/* ─── Passo individual ───────────────────────────────────────────────────────*/

.step {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  gap: var(--space-4);
  position: relative;
  padding: 0 var(--space-4);

  /* Estado inicial para animação — invisível e deslocado */
  opacity: 0;
  transform: translateY(24px);
  transition:
    opacity   var(--duration-slow) var(--ease-out-quart) var(--delay, 0ms),
    transform var(--duration-slow) var(--ease-out-quart) var(--delay, 0ms);
}

/* Estado visível: disparado pelo IntersectionObserver */
.step--visible {
  opacity: 1;
  transform: translateY(0);
}

@media (prefers-reduced-motion: reduce) {
  .step { opacity: 1; transform: none; transition: none; }
}

/* ─── Linha conectora ────────────────────────────────────────────────────────
   Desktop: linha horizontal entre os ícones
   Posicionada absolutamente a partir do centro do ícone do passo atual,
   vai até o início do próximo — por isso usa calc e left/right
   ──────────────────────────────────────────────────────────────────────────── */

.step__connector {
  position: absolute;
  /* Centro vertical do ícone (wrap de 56px → 28px do topo do passo) */
  top: 28px;
  /* Começa após o meio do ícone atual, termina no meio do próximo */
  left:  calc(50% + 28px);
  right: calc(-50% + 28px);
  height: 2px;
  background-color: var(--color-primary-border);
}

/* Último passo não tem linha conectora à direita */
.step:last-child .step__connector {
  display: none;
}

/* ─── Ícone numerado ─────────────────────────────────────────────────────────*/

.step__icon-wrap {
  position: relative;
  width: 56px;
  height: 56px;
  border-radius: var(--radius-full);
  background-color: var(--color-primary-subtle);
  border: 2px solid var(--color-primary-border);
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  color: var(--color-primary);
  /* Garante que o ícone fique sobre a linha conectora */
  z-index: 1;
  background-clip: padding-box;
}

.step__number {
  position: absolute;
  top: -8px;
  right: -8px;
  width: 20px;
  height: 20px;
  border-radius: var(--radius-full);
  background-color: var(--color-primary);
  color: var(--color-text-on-primary);
  font-family: var(--font-display);
  font-size: 10px;
  font-weight: var(--weight-bold);
  display: flex;
  align-items: center;
  justify-content: center;
  line-height: 1;
  /* Afasta do ícone principal */
  border: 2px solid var(--color-bg);
}

.step__svg {
  /* SVG herda a cor do wrapper */
}

/* ─── Corpo do passo ─────────────────────────────────────────────────────────*/

.step__body {
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
  max-width: 200px;
}

.step__title {
  /* heading-3 via design system */
}

.step__desc {
  /* body-base text-secondary via design system */
}

/* ─── Mobile — coluna vertical ───────────────────────────────────────────────*/

@media (max-width: 768px) {
  .steps {
    grid-template-columns: 1fr;
    gap: 0;
  }

  .step {
    flex-direction: row;
    text-align: left;
    align-items: flex-start;
    padding: 0 0 var(--space-6) var(--space-4);
  }

  /* Linha conectora vertical no mobile */
  .step__connector {
    top:    calc(56px + var(--space-2));
    bottom: 0;
    left:   calc(var(--space-4) + 27px); /* centro do ícone */
    right:  auto;
    width:  2px;
    height: auto;
  }

  .step:last-child {
    padding-bottom: 0;
  }

  .step__body {
    max-width: 100%;
  }
}

@media (max-width: 480px) {
  .como-funciona {
    padding: var(--space-10) var(--space-4);
  }
}
</style>
