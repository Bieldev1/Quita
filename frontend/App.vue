<template>
  <!-- data-theme no html é lido pelo design system para alternar variáveis CSS -->
  <div id="app">
    <AppHeader :theme="theme" @toggle-theme="toggleTheme" />
    <main>
      <RouterView />
    </main>
    <AppFooter />
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import AppHeader from './components/AppHeader.vue'
import AppFooter from './components/AppFooter.vue'

// ─── Tema ────────────────────────────────────────────────────────────────────

const STORAGE_KEY = 'quita:theme'

function getInitialTheme() {
  // 1. Preferência salva tem prioridade
  const saved = localStorage.getItem(STORAGE_KEY)
  if (saved === 'dark' || saved === 'light') return saved

  // 2. Fallback: preferência do sistema operacional
  return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
}

const theme = ref('light')

function applyTheme(value) {
  // O design system lê [data-theme="dark"] no <html>
  document.documentElement.setAttribute('data-theme', value)
}

function toggleTheme() {
  theme.value = theme.value === 'light' ? 'dark' : 'light'
}

// Sincroniza o atributo e persiste sempre que o tema muda
watch(theme, (value) => {
  applyTheme(value)
  localStorage.setItem(STORAGE_KEY, value)
})

onMounted(() => {
  theme.value = getInitialTheme()
  applyTheme(theme.value)

  // Escuta mudanças do sistema em tempo real (usuário troca o modo no SO)
  // mas só aplica se o usuário não tiver escolhido manualmente
  window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
    if (!localStorage.getItem(STORAGE_KEY)) {
      theme.value = e.matches ? 'dark' : 'light'
    }
  })
})
</script>

<style>
/* Design system importado globalmente aqui — único ponto de entrada */
@import './design-system.css';

/* Reset mínimo de layout */
#app {
  display: flex;
  flex-direction: column;
  min-height: 100dvh;
}

main {
  flex: 1;
  /* Compensa o header fixo (altura definida no AppHeader) */
  padding-top: var(--header-height, 68px);
}
</style>
