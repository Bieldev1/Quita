/// <reference types="vite/client" />

// Declara o tipo de todos os arquivos .vue para o TypeScript
// Sem isso o compilador não sabe que um import de .vue retorna um componente Vue
declare module '*.vue' {
  import type { DefineComponent } from 'vue'
  const component: DefineComponent<Record<string, unknown>, Record<string, unknown>, unknown>
  export default component
}
