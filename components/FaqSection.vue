<template>
  <section id="duvidas" class="faq" aria-labelledby="faq-titulo">
    <div class="faq__inner">

      <!-- Cabeçalho -->
      <div class="faq__header">
        <span class="badge badge--neutral">Dúvidas frequentes</span>
        <h2 id="faq-titulo" class="display-2">Perguntas frequentes</h2>
      </div>

      <!-- Acordeão -->
      <div class="faq__list" role="list">
        <div
          v-for="(item, index) in perguntas"
          :key="index"
          class="faq-item"
          role="listitem"
        >
          <!-- Botão de toggle — semântica de disclosure correta -->
          <button
            class="faq-item__trigger"
            :aria-expanded="aberto === index"
            :aria-controls="`faq-resposta-${index}`"
            :id="`faq-pergunta-${index}`"
            @click="toggle(index)"
          >
            <span class="faq-item__pergunta body-lg">{{ item.pergunta }}</span>
            <!-- "+" que rotaciona 45° ao abrir -->
            <span
              class="faq-item__icon"
              :class="{ 'faq-item__icon--open': aberto === index }"
              aria-hidden="true"
            >
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                viewBox="0 0 24 24" fill="none" stroke="currentColor"
                stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                <line x1="12" y1="5" x2="12" y2="19"/>
                <line x1="5"  y1="12" x2="19" y2="12"/>
              </svg>
            </span>
          </button>

          <!-- Resposta — max-height transition para animação suave sem JS de terceiros -->
          <div
            :id="`faq-resposta-${index}`"
            class="faq-item__resposta"
            :class="{ 'faq-item__resposta--open': aberto === index }"
            role="region"
            :aria-labelledby="`faq-pergunta-${index}`"
            :hidden="aberto !== index"
          >
            <!-- Wrapper interno necessário para o padding não vazar durante a animação -->
            <div class="faq-item__resposta-inner">
              <p class="body-base text-secondary">{{ item.resposta }}</p>
            </div>
          </div>
        </div>
      </div>

    </div>
  </section>
</template>

<script setup>
import { ref } from 'vue'

// ─── Conteúdo ─────────────────────────────────────────────────────────────────

const perguntas = [
  {
    pergunta: 'O Quita é gratuito?',
    resposta:  'Sim, 100%. Não cobramos nada para simular. Monetizamos de forma transparente com parcerias e anúncios.',
  },
  {
    pergunta: 'Preciso me cadastrar?',
    resposta:  'Não. Nenhum dado pessoal é coletado. A simulação é anônima.',
  },
  {
    pergunta: 'Os valores são exatos?',
    resposta:  'São estimativas baseadas nas regras oficiais do Desenrola. As condições finais dependem de cada banco.',
  },
  {
    pergunta: 'Posso usar o FGTS?',
    resposta:  'Sim. O Novo Desenrola permite usar até 20% do saldo do FGTS ou R$ 1.000 (o que for maior) para abater a dívida.',
  },
  {
    pergunta: 'Quem pode usar o Desenrola?',
    resposta:  'Pessoas com renda de até R$ 8.105 (5 salários mínimos) com dívidas de cartão, cheque especial ou crédito pessoal em atraso entre 90 dias e 2 anos.',
  },
  {
    pergunta: 'Como negocie depois da simulação?',
    resposta:  'Procure diretamente seu banco ou acesse o portal oficial do governo em gov.br/fazenda/desenrola.',
  },
]

// ─── Estado do acordeão ───────────────────────────────────────────────────────
// null = todos fechados; number = índice do item aberto

const aberto = ref(null)

function toggle(index) {
  // Fecha se já estiver aberto; abre o novo e fecha o anterior
  aberto.value = aberto.value === index ? null : index
}
</script>

<style scoped>
/* ─── Seção ──────────────────────────────────────────────────────────────────*/

.faq {
  background-color: var(--color-bg-subtle);
  padding: var(--space-16) var(--space-6);
}

.faq__inner {
  max-width: var(--container-md);
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--space-10);
}

/* ─── Cabeçalho ──────────────────────────────────────────────────────────────*/

.faq__header {
  text-align: center;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: var(--space-3);
}

/* ─── Lista de itens ─────────────────────────────────────────────────────────*/

.faq__list {
  width: 100%;
  display: flex;
  flex-direction: column;
  /* Borda superior no primeiro item via border-top em .faq-item */
  border-bottom: 1px solid var(--color-border);
}

/* ─── Item do acordeão ───────────────────────────────────────────────────────*/

.faq-item {
  border-top: 1px solid var(--color-border);
}

/* ─── Botão de trigger ───────────────────────────────────────────────────────*/

.faq-item__trigger {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-4);
  padding: var(--space-5) 0;
  background: none;
  border: none;
  cursor: pointer;
  text-align: left;
  /* Área de clique generosa para facilitar toque em mobile */
  min-height: 60px;
  transition: color var(--duration-normal) var(--ease-out-quart);
}

.faq-item__trigger:hover .faq-item__pergunta {
  color: var(--color-primary);
}

.faq-item__trigger:focus-visible {
  outline: 2px solid var(--color-focus-ring);
  outline-offset: 4px;
  border-radius: var(--radius-sm);
}

.faq-item__pergunta {
  /* body-lg vem do design system — peso reforçado para leiturabilidade */
  font-weight: var(--weight-semibold);
  color: var(--color-text-primary);
  transition: color var(--duration-normal) var(--ease-out-quart);
  flex: 1;
}

/* ─── Ícone "+" ──────────────────────────────────────────────────────────────*/

.faq-item__icon {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  width: 32px;
  height: 32px;
  border-radius: var(--radius-md);
  background-color: var(--color-bg-subtle);
  color: var(--color-text-secondary);
  transition:
    transform          var(--duration-normal) var(--ease-spring),
    background-color   var(--duration-normal) var(--ease-out-quart),
    color              var(--duration-normal) var(--ease-out-quart);
}

/* Rotaciona 45° → transforma "+" em "×" ao abrir */
.faq-item__icon--open {
  transform: rotate(45deg);
  background-color: var(--color-primary-subtle);
  color: var(--color-primary);
}

/* ─── Resposta ───────────────────────────────────────────────────────────────
   max-height transition é o único método CSS-only que anima abertura de altura
   sem JS de terceiros. Usa valor alto (600px) como teto — conteúdo nunca
   chegará perto disso.
   ──────────────────────────────────────────────────────────────────────────── */

.faq-item__resposta {
  max-height: 0;
  overflow: hidden;
  transition: max-height var(--duration-slow) var(--ease-out-quart);
}

/* Remove o hidden nativo enquanto mantemos max-height: 0 para fechar */
/* O atributo :hidden é usado para acessibilidade; o CSS gerencia o visual */
.faq-item__resposta[hidden] {
  /* Sobrescreve display:none do hidden — deixamos max-height controlar a visibilidade */
  display: block !important;
  visibility: hidden;
}

.faq-item__resposta--open {
  max-height: 600px;
  visibility: visible;
}

/* Padding interno separado do wrapper para não vazar durante a animação */
.faq-item__resposta-inner {
  padding-bottom: var(--space-5);
}

/* ─── Mobile ─────────────────────────────────────────────────────────────────*/

@media (max-width: 480px) {
  .faq {
    padding: var(--space-10) var(--space-4);
  }

  .faq-item__trigger {
    padding: var(--space-4) 0;
    min-height: 52px;
  }
}
</style>
