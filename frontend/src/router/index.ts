import { createRouter, createWebHistory } from 'vue-router'
import HomeView        from '../views/HomeView.vue'
import PrivacidadeView from '../views/PrivacidadeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),

  routes: [
    {
      path:      '/',
      name:      'home',
      component: HomeView,
    },
    {
      path:      '/privacidade',
      name:      'privacidade',
      component: PrivacidadeView,
    },
  ],

  scrollBehavior(to, _from, savedPosition) {
    // Posição salva (botão voltar do browser) tem prioridade
    if (savedPosition) return savedPosition

    // Hash: deixa o browser rolar suavemente até a âncora
    if (to.hash) {
      return { el: to.hash, behavior: 'smooth' }
    }

    // Navegação comum: topo da página
    return { top: 0, behavior: 'smooth' }
  },
})

export default router
