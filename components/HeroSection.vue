<template>
  <section class="hero" aria-labelledby="hero-title">

    <!-- Elemento decorativo de fundo — puramente visual, oculto de leitores de tela -->
    <div class="hero__orb" aria-hidden="true"></div>

    <div class="hero__inner">

      <!-- ── Coluna esquerda: conteúdo textual ─────────────────────────────── -->
      <div class="hero__content">

        <!-- Badge de contexto -->
        <div class="hero__badge-wrap animate animate--1">
          <span class="badge badge--highlight">
            Novo Desenrola Brasil · Atualizado maio/2026
          </span>
        </div>

        <!-- Título principal -->
        <h1 id="hero-title" class="display-1 hero__title animate animate--2">
          Descubra quanto você pode
          <!-- "economizar" em âmbar reforça o benefício visualmente -->
          <span class="hero__title-accent">economizar</span>
          para limpar seu nome
        </h1>

        <!-- Subtítulo -->
        <p class="body-lg text-secondary hero__subtitle animate animate--3">
          Simule gratuitamente sua renegociação pelo Desenrola Brasil.
          Sem cadastro, sem compromisso. Resultado em segundos.
        </p>

        <!-- CTAs -->
        <div class="hero__ctas animate animate--4">
          <a href="#simulador" class="btn btn--primary btn--lg">
            Simular agora
          </a>
          <a href="#como-funciona" class="btn btn--ghost-brand btn--lg">
            Como funciona ↓
          </a>
        </div>

        <!-- Trust badges — reforçam segurança sem exagero -->
        <ul class="hero__trust animate animate--5" aria-label="Diferenciais">
          <li class="trust-badge">
            <!-- Ícone: escudo com check -->
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
              aria-hidden="true">
              <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/>
              <polyline points="9 12 11 14 15 10"/>
            </svg>
            <span>100% gratuito</span>
          </li>
          <li class="trust-badge">
            <!-- Ícone: cadeado fechado -->
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
              aria-hidden="true">
              <rect x="3" y="11" width="18" height="11" rx="2" ry="2"/>
              <path d="M7 11V7a5 5 0 0 1 10 0v4"/>
            </svg>
            <span>Sem cadastro</span>
          </li>
          <li class="trust-badge">
            <!-- Ícone: prédio/instituição -->
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
              viewBox="0 0 24 24" fill="none" stroke="currentColor"
              stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
              aria-hidden="true">
              <line x1="3" y1="22" x2="21" y2="22"/>
              <line x1="6" y1="18" x2="6" y2="11"/>
              <line x1="10" y1="18" x2="10" y2="11"/>
              <line x1="14" y1="18" x2="14" y2="11"/>
              <line x1="18" y1="18" x2="18" y2="11"/>
              <polygon points="12 2 20 7 4 7"/>
            </svg>
            <span>Dados do Ministério da Fazenda</span>
          </li>
        </ul>

      </div>

      <!-- ── Coluna direita: card flutuante ────────────────────────────────── -->
      <div class="hero__card-wrap animate animate--3">
        <article class="preview-card" aria-label="O que você vai descobrir na simulação">

          <header class="preview-card__header">
            <span class="preview-card__icon" aria-hidden="true">
              <!-- Ícone: gráfico de barra ascendente -->
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20"
                viewBox="0 0 24 24" fill="none" stroke="currentColor"
                stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="18" y1="20" x2="18" y2="10"/>
                <line x1="12" y1="20" x2="12" y2="4"/>
                <line x1="6"  y1="20" x2="6"  y2="14"/>
                <line x1="2"  y1="20" x2="22" y2="20"/>
              </svg>
            </span>
            <h2 class="heading-3 preview-card__title">Simulação rápida</h2>
          </header>

          <!-- Itens com check -->
          <ul class="preview-card__items" aria-label="Informações da simulação">
            <li class="preview-card__item">
              <CheckIcon />
              <span class="body-base">Valor da dívida</span>
            </li>
            <li class="preview-card__item">
              <CheckIcon />
              <span class="body-base">Desconto estimado</span>
            </li>
            <li class="preview-card__item">
              <CheckIcon />
              <span class="body-base">Parcelas do Desenrola</span>
            </li>
          </ul>

          <!-- Badge de destaque — resultado concreto que ancora a proposta de valor -->
          <footer class="preview-card__footer">
            <span class="badge badge--highlight badge--lg">
              Economia média de 53%
            </span>
            <p class="body-sm preview-card__disclaimer">
              Baseado em dados oficiais do programa
            </p>
          </footer>

        </article>
      </div>

    </div>
  </section>
</template>

<script setup>
// CheckIcon extraído como componente inline para evitar repetição de SVG
// e manter o template limpo — sem dependências externas
const CheckIcon = {
  template: `
    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18"
      viewBox="0 0 24 24" fill="none" stroke="currentColor"
      stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
      class="check-icon" aria-hidden="true">
      <polyline points="20 6 9 17 4 12"/>
    </svg>
  `,
}
</script>

<style scoped>
/* ─── Seção ──────────────────────────────────────────────────────────────────
   Gradiente de bg → primary-subtle: sinaliza suavemente a identidade da marca
   sem usar cor direta — segue o token do design system
   ──────────────────────────────────────────────────────────────────────────── */

.hero {
  position: relative;
  overflow: hidden;
  padding: var(--space-16) var(--space-6) var(--space-20);
  background: linear-gradient(
    160deg,
    var(--color-bg)           0%,
    var(--color-bg)           45%,
    var(--color-primary-subtle) 100%
  );
}

/* ─── Orbe decorativa ────────────────────────────────────────────────────────
   Círculo borrado atrás do card — cria profundidade sem distrair
   ──────────────────────────────────────────────────────────────────────────── */

.hero__orb {
  position: absolute;
  width: 600px;
  height: 600px;
  border-radius: var(--radius-full);
  background: var(--color-primary-muted);
  filter: blur(80px);
  opacity: 0.6;
  /* Posicionado atrás e à direita — alinhado com o card no desktop */
  top: -120px;
  right: -160px;
  pointer-events: none;
  z-index: 0;
}

/* ─── Layout interno ─────────────────────────────────────────────────────────
   CSS Grid duas colunas: texto ocupa ~55%, card ~45%
   ──────────────────────────────────────────────────────────────────────────── */

.hero__inner {
  position: relative;
  z-index: 1;
  max-width: var(--container-xl);
  margin: 0 auto;
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: var(--space-12);
  align-items: center;
}

/* ─── Conteúdo textual ───────────────────────────────────────────────────────*/

.hero__content {
  display: flex;
  flex-direction: column;
  gap: var(--space-6);
  max-width: 560px;
}

.hero__badge-wrap {
  /* Garante que o badge não estique — alinha à esquerda naturalmente */
  display: flex;
}

.hero__title {
  /* Override pontual: espaçamento mais generoso entre linhas no display */
  line-height: var(--leading-snug);
}

/* Palavra de destaque em âmbar — ancora o benefício principal visualmente */
.hero__title-accent {
  color: var(--color-secondary);
  /* Sublinhado decorativo reforça a palavra-chave sem agressividade */
  text-decoration: underline;
  text-decoration-color: color-mix(in srgb, var(--color-secondary) 35%, transparent);
  text-underline-offset: 6px;
  text-decoration-thickness: 3px;
}

.hero__subtitle {
  /* Limita a largura para melhor legibilidade — ~65 caracteres por linha */
  max-width: 50ch;
}

/* ─── CTAs ───────────────────────────────────────────────────────────────────*/

.hero__ctas {
  display: flex;
  align-items: center;
  gap: var(--space-3);
  flex-wrap: wrap;
}

/* ─── Trust badges ───────────────────────────────────────────────────────────
   Lista sem estilo de lista — semântica mantida, visual limpo
   ──────────────────────────────────────────────────────────────────────────── */

.hero__trust {
  list-style: none;
  display: flex;
  align-items: center;
  gap: var(--space-5);
  flex-wrap: wrap;
}

.trust-badge {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  font-family: var(--font-body);
  font-size: var(--text-sm);
  font-weight: var(--weight-medium);
  color: var(--color-text-secondary);
}

/* Ícone herda a cor do texto — sem hardcode */
.trust-badge svg {
  color: var(--color-primary);
  flex-shrink: 0;
}

/* ─── Card de preview ────────────────────────────────────────────────────────
   "Flutua" à direita no desktop — sugere o resultado antes de simular
   ──────────────────────────────────────────────────────────────────────────── */

.hero__card-wrap {
  display: flex;
  justify-content: center;
  /* Leve rotação dá sensação de card "pousando" — elegante, não lúdico */
  transform: rotate(1.5deg);
}

.preview-card {
  background-color: var(--color-bg-elevated);
  border: 1px solid var(--color-border);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-teal-lg);
  padding: var(--space-6);
  width: 100%;
  max-width: 360px;
  display: flex;
  flex-direction: column;
  gap: var(--space-5);
}

/* ─── Header do card ─────────────────────────────────────────────────────────*/

.preview-card__header {
  display: flex;
  align-items: center;
  gap: var(--space-3);
}

.preview-card__icon {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 40px;
  border-radius: var(--radius-md);
  background-color: var(--color-primary-subtle);
  color: var(--color-primary);
  flex-shrink: 0;
}

.preview-card__title {
  /* heading-3 já vem do design system — só posicionamento aqui */
  margin: 0;
}

/* ─── Itens do card ──────────────────────────────────────────────────────────*/

.preview-card__items {
  list-style: none;
  display: flex;
  flex-direction: column;
  gap: var(--space-3);
}

.preview-card__item {
  display: flex;
  align-items: center;
  gap: var(--space-3);
  color: var(--color-text-primary);
}

/* Ícone de check — cor primária, tamanho fixo para alinhar os textos */
:deep(.check-icon),
.check-icon {
  color: var(--color-primary);
  flex-shrink: 0;
}

/* ─── Footer do card ─────────────────────────────────────────────────────────*/

.preview-card__footer {
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
  padding-top: var(--space-4);
  /* Linha separadora sutil — não usa <hr> para não quebrar o flex flow */
  border-top: 1px solid var(--color-border);
}

.preview-card__disclaimer {
  /* Reforça credibilidade sem ocupar muito espaço visual */
  color: var(--color-text-tertiary);
  font-style: italic;
}

/* ─── Animações de entrada ───────────────────────────────────────────────────
   @keyframes simples: fade + slide-up
   Delay escalonado cria cascata natural sem ser excessivo
   ──────────────────────────────────────────────────────────────────────────── */

@keyframes fade-up {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

/* Classe base de animação */
.animate {
  opacity: 0;
  animation: fade-up var(--duration-slow) var(--ease-out-quart) forwards;
}

/* Delays escalonados — cada elemento entra 80ms após o anterior */
.animate--1 { animation-delay: 80ms;  }
.animate--2 { animation-delay: 160ms; }
.animate--3 { animation-delay: 240ms; }
.animate--4 { animation-delay: 320ms; }
.animate--5 { animation-delay: 440ms; }

/* O card usa o mesmo delay do subtítulo — entram "juntos", pesos diferentes */
.hero__card-wrap.animate--3 {
  animation-name: fade-up-card;
}

/* Animação do card tem leve rotação extra para reforçar a inclinação decorativa */
@keyframes fade-up-card {
  from {
    opacity: 0;
    transform: translateY(28px) rotate(1.5deg);
  }
  to {
    opacity: 1;
    transform: translateY(0) rotate(1.5deg);
  }
}

/* Respeita preferência de movimento reduzido (acessibilidade) */
@media (prefers-reduced-motion: reduce) {
  .animate {
    animation: none;
    opacity: 1;
  }
}

/* ─── Mobile ─────────────────────────────────────────────────────────────────
   Uma coluna: texto → card; hero mais compacto
   ──────────────────────────────────────────────────────────────────────────── */

@media (max-width: 768px) {
  .hero {
    padding: var(--space-10) var(--space-4) var(--space-12);
  }

  .hero__inner {
    grid-template-columns: 1fr;
    gap: var(--space-8);
  }

  .hero__content {
    max-width: 100%;
    gap: var(--space-5);
  }

  .hero__subtitle {
    max-width: 100%;
  }

  /* Remove a rotação decorativa no mobile — evita overflow em telas pequenas */
  .hero__card-wrap {
    transform: none;
  }

  @keyframes fade-up-card {
    from { opacity: 0; transform: translateY(28px); }
    to   { opacity: 1; transform: translateY(0);    }
  }

  .preview-card {
    max-width: 100%;
  }

  .hero__trust {
    gap: var(--space-4);
  }

  .hero__orb {
    width: 340px;
    height: 340px;
    top: -60px;
    right: -100px;
  }
}

@media (max-width: 400px) {
  .hero__ctas {
    flex-direction: column;
    align-items: stretch;
  }

  .hero__ctas .btn {
    width: 100%;
    justify-content: center;
  }
}
</style>
