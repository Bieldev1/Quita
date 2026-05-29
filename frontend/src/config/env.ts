// Constantes de ambiente — ponto único de leitura de variáveis Vite
// Nunca leia import.meta.env diretamente nos componentes

export const API_URL: string =
  (import.meta.env.VITE_API_URL as string | undefined) ?? 'http://localhost:5001'

export const IS_DEV: boolean = import.meta.env.DEV

export const APP_VERSION: string =
  (import.meta.env.VITE_APP_VERSION as string | undefined) ?? '1.0.0'
