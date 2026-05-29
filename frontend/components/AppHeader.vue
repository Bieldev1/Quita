<template>
  <header class="header" :class="{ scrolled: isScrolled }">
    <div class="header__inner">

      <!-- Logo -->
      <a href="/" class="logo" aria-label="Quita — página inicial">
        <span class="logo__symbol" aria-hidden="true">✦</span>
        <span class="logo__name">Quita</span>
      </a>

      <!-- Nav principal (some no mobile) -->
      <nav class="nav" aria-label="Menu principal">
        <a class="nav__link" href="#como-funciona">Como funciona</a>
        <a class="nav__link" href="#duvidas">Dúvidas frequentes</a>
      </nav>

    </div>
  </header>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

const isScrolled = ref(false)
const SCROLL_THRESHOLD = 60

function onScroll() {
  isScrolled.value = window.scrollY > SCROLL_THRESHOLD
}

onMounted(() => window.addEventListener('scroll', onScroll, { passive: true }))
onUnmounted(() => window.removeEventListener('scroll', onScroll))
</script>

<style scoped>
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
  background-color: color-mix(in srgb, var(--color-bg) 80%, transparent);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);
  border-bottom: 1px solid var(--color-border);
  transition:
    box-shadow   var(--duration-normal) var(--ease-out-quart),
    border-color var(--duration-normal) var(--ease-out-quart);
}

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

/* ─── Logo ───────────────────────────────────────────────────────────────────*/

.logo {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  text-decoration: none;
  flex-shrink: 0;
}

.logo__symbol {
  font-size: var(--text-lg);
  color: var(--color-secondary);
  line-height: 1;
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

/* ─── Nav ────────────────────────────────────────────────────────────────────*/

.nav {
  display: flex;
  align-items: center;
  gap: var(--space-1);
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

/* ─── Mobile ─────────────────────────────────────────────────────────────────*/

@media (max-width: 640px) {
  .nav { display: none; }
  .header__inner { padding: 0 var(--space-4); }
}
</style>
