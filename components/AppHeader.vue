<template>
  <header class="header" :class="{ scrolled: isScrolled }">
    <div class="header__inner">

      <!-- Logo -->
      <a href="/" class="logo" aria-label="Quita — página inicial">
        <!-- ✦ em âmbar sinaliza conquista; nome em display font da marca -->
        <span class="logo__symbol" aria-hidden="true">✦</span>
        <span class="logo__name">Quita</span>
      </a>

      <!-- Nav principal (some no mobile) -->
      <nav class="nav" aria-label="Menu principal">
        <a class="nav__link" href="#como-funciona">Como funciona</a>
        <a class="nav__link" href="#duvidas">Dúvidas frequentes</a>
      </nav>

      <!-- Ações do header -->
      <div class="header__actions">
        <!-- Toggle de tema: só ícone, sem texto (aria-label descreve a ação) -->
        <button
          class="theme-toggle btn btn--ghost"
          :aria-label="isDark ? 'Ativar modo claro' : 'Ativar modo escuro'"
          :title="isDark ? 'Modo claro' : 'Modo escuro'"
          @click="$emit('toggle-theme')"
        >
          <!-- Ícone sol (tema claro ativo → oferece escuro) -->
          <svg v-if="!isDark" xmlns="http://www.w3.org/2000/svg" width="20" height="20"
            viewBox="0 0 24 24" fill="none" stroke="currentColor"
            stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
            aria-hidden="true">
            <circle cx="12" cy="12" r="4"/>
            <path d="M12 2v2M12 20v2M4.93 4.93l1.41 1.41M17.66 17.66l1.41 1.41
                     M2 12h2M20 12h2M4.93 19.07l1.41-1.41M17.66 6.34l1.41-1.41"/>
          </svg>
          <!-- Ícone lua (tema escuro ativo → oferece claro) -->
          <svg v-else xmlns="http://www.w3.org/2000/svg" width="20" height="20"
            viewBox="0 0 24 24" fill="none" stroke="currentColor"
            stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
            aria-hidden="true">
            <path d="M21 12.79A9 9 0 1 1 11.21 3a7 7 0 0 0 9.79 9.79z"/>
          </svg>
        </button>
      </div>

    </div>
  </header>
</template>

<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'

const props = defineProps({
  theme: { type: String, required: true },
})

defineEmits(['toggle-theme'])

const isDark = computed(() => props.theme === 'dark')

// ─── Comportamento de scroll ──────────────────────────────────────────────────

const isScrolled = ref(false)
const SCROLL_THRESHOLD = 60

function onScroll() {
  isScrolled.value = window.scrollY > SCROLL_THRESHOLD
}

onMounted(() => window.addEventListener('scroll', onScroll, { passive: true }))
onUnmounted(() => window.removeEventListener('scroll', onScroll))
</script>

<style scoped>
/* ─── Variável local: altura do header (consumida em App.vue via main padding-top) ─ */
:global(:root) {
  --header-height: 68px;
}

.header {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: var(--z-sticky);
  height: var(--header-height);

  /* Fundo translúcido com blur — cria profundidade sem bloquear o conteúdo */
  background-color: color-mix(in srgb, var(--color-bg) 80%, transparent);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);

  border-bottom: 1px solid var(--color-border);
  transition:
    box-shadow   var(--duration-normal) var(--ease-out-quart),
    border-color var(--duration-normal) var(--ease-out-quart);
}

/* Sombra aparece só após o scroll — não compete com conteúdo no topo */
.header.scrolled {
  box-shadow: var(--shadow-md);
  border-bottom-color: var(--color-border-strong);
}

.header__inner {
  max-width: var(--container-xl);
  margin: 0 auto;
  height: 100%;
  padding: 0 var(--space-6);
  display: flex;
  align-items: center;
  gap: var(--space-8);
}

/* ─── Logo ─────────────────────────────────────────────────────────────────── */

.logo {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  text-decoration: none;
  flex-shrink: 0;
}

.logo__symbol {
  /* ✦ em âmbar — identidade visual da marca */
  font-size: var(--text-lg);
  color: var(--color-secondary);
  line-height: 1;
  /* Leve animação para chamar atenção sem ser invasivo */
  transition: transform var(--duration-slow) var(--ease-spring);
}

.logo:hover .logo__symbol {
  transform: rotate(30deg) scale(1.15);
}

.logo__name {
  font-family: var(--font-display);
  font-size: var(--text-lg);
  font-weight: var(--weight-bold);
  color: var(--color-primary);
  letter-spacing: var(--tracking-tight);
  line-height: 1;
}

/* ─── Nav ──────────────────────────────────────────────────────────────────── */

.nav {
  display: flex;
  align-items: center;
  gap: var(--space-1);
  /* Empurra a nav para depois do logo, ações ficam no fim */
  flex: 1;
}

.nav__link {
  font-family: var(--font-body);
  font-size: var(--text-sm);
  font-weight: var(--weight-medium);
  color: var(--color-text-secondary);
  text-decoration: none;
  padding: var(--space-2) var(--space-3);
  border-radius: var(--radius-md);
  transition: var(--transition-base);
  white-space: nowrap;
}

.nav__link:hover {
  color: var(--color-text-primary);
  background-color: var(--color-bg-subtle);
}

.nav__link:focus-visible {
  outline: 2px solid var(--color-focus-ring);
  outline-offset: 2px;
}

/* ─── Ações ────────────────────────────────────────────────────────────────── */

.header__actions {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  margin-left: auto;
  flex-shrink: 0;
}

/* Override do .btn--ghost para o toggle ficar quadrado e compacto */
.theme-toggle {
  width: 40px;
  height: 40px;
  min-height: unset;
  padding: 0;
  border-radius: var(--radius-md);
  color: var(--color-text-secondary);
}

.theme-toggle:hover {
  color: var(--color-text-primary);
}

/* ─── Mobile: nav some, só logo + toggle ───────────────────────────────────── */

@media (max-width: 640px) {
  .nav {
    display: none;
  }

  .header__inner {
    padding: 0 var(--space-4);
  }
}
</style>
