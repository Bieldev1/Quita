<template>
  <div id="app">
    <AppHeader />
    <main>
      <RouterView />
    </main>
    <AppFooter />
  </div>
</template>

<script setup>
import AppHeader from './components/AppHeader.vue'
import AppFooter from './components/AppFooter.vue'
</script>

<style>
/* Design system importado globalmente aqui — único ponto de entrada */
@import './design-system.css';

#app {
  display: flex;
  flex-direction: column;
  min-height: 100dvh;
}

main {
  flex: 1;
  padding-top: var(--header-height, 68px);
}
</style>
