<template>
  <div class="privacidade">
    <div class="privacidade__inner">

      <!-- Cabeçalho -->
      <header class="privacidade__header">
        <span class="label-sm">Legal</span>
        <h1 class="display-2">Política de Privacidade</h1>
        <p class="body-base text-secondary">
          Atualizado em <time datetime="2026-05">maio de 2026</time>
        </p>
      </header>

      <!-- Seções -->
      <div class="privacidade__content">

        <!-- 1. O que coletamos -->
        <section class="privacy-section" aria-labelledby="sec-coleta">
          <h2 id="sec-coleta" class="heading-2 privacy-section__title">
            O que coletamos
          </h2>
          <p class="body-base">
            <strong>Não coletamos nenhum dado pessoal.</strong> A simulação é 100% anônima —
            você não precisa informar nome, CPF, e-mail ou qualquer outra informação pessoal.
            Os valores digitados no simulador existem apenas no seu navegador e não são
            enviados ou armazenados em nenhum servidor.
          </p>
        </section>

        <!-- 2. Cookies e anúncios -->
        <section class="privacy-section" aria-labelledby="sec-cookies">
          <h2 id="sec-cookies" class="heading-2 privacy-section__title">
            Cookies e anúncios
          </h2>
          <p class="body-base">
            Utilizamos o <strong>Google AdSense</strong> para exibir anúncios que ajudam a
            manter o Quita gratuito. O AdSense pode usar cookies para personalizar os anúncios
            com base nas suas preferências de navegação.
          </p>
          <p class="body-base">
            Você pode gerenciar ou desativar os cookies de publicidade a qualquer momento nas
            configurações do seu navegador ou pelo painel de preferências de anúncios do Google em
            <a
              href="https://adssettings.google.com"
              target="_blank"
              rel="noopener noreferrer"
              class="privacy-link"
            >adssettings.google.com</a>.
          </p>
        </section>

        <!-- 3. Links de afiliados -->
        <section class="privacy-section" aria-labelledby="sec-afiliados">
          <h2 id="sec-afiliados" class="heading-2 privacy-section__title">
            Links de afiliados
          </h2>
          <p class="body-base">
            Alguns links neste site — como os que direcionam para plataformas de renegociação —
            são links de afiliados. Isso significa que podemos receber uma comissão caso você
            contrate um serviço por meio deles.
          </p>
          <p class="body-base">
            <strong>Isso não tem nenhum custo adicional para você</strong> e não influencia
            as simulações ou recomendações exibidas. Nossa ferramenta permanece gratuita e imparcial.
          </p>
        </section>

        <!-- 4. Contato -->
        <section class="privacy-section" aria-labelledby="sec-contato">
          <h2 id="sec-contato" class="heading-2 privacy-section__title">
            Contato
          </h2>
          <p class="body-base">
            Dúvidas sobre esta política ou sobre o funcionamento do Quita? Entre em contato:
          </p>
          <a
            href="mailto:contato.gletech@gmail.com"
            class="privacy-link privacy-link--email"
          >
            contato.gletech@gmail.com
          </a>
        </section>

      </div>

      <!-- Rodapé da página -->
      <footer class="privacidade__footer">
        <RouterLink to="/" class="btn btn--ghost-brand">
          ← Voltar ao simulador
        </RouterLink>
      </footer>

    </div>
  </div>
</template>

<script setup lang="ts">
// Página estática — sem lógica necessária
</script>

<style scoped>
.privacidade {
  background-color: var(--color-bg);
  padding: var(--space-16) var(--space-6);
  min-height: 100dvh;
}

.privacidade__inner {
  max-width: 720px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: var(--space-12);
}

/* ─── Cabeçalho ──────────────────────────────────────────────────────────────*/

.privacidade__header {
  display: flex;
  flex-direction: column;
  gap: var(--space-3);
  padding-bottom: var(--space-8);
  border-bottom: 1px solid var(--color-border);
}

.privacidade__header .label-sm {
  color: var(--color-primary-text);
}

/* ─── Conteúdo ───────────────────────────────────────────────────────────────*/

.privacidade__content {
  display: flex;
  flex-direction: column;
  gap: var(--space-10);
}

/* ─── Seção individual ───────────────────────────────────────────────────────*/

.privacy-section {
  display: flex;
  flex-direction: column;
  gap: var(--space-4);
  padding-bottom: var(--space-10);
  border-bottom: 1px solid var(--color-border);
}

.privacy-section:last-child {
  border-bottom: none;
  padding-bottom: 0;
}

.privacy-section__title {
  display: flex;
  align-items: center;
  gap: var(--space-3);
}

/* Parágrafos com espaçamento entre si */
.privacy-section p + p {
  margin-top: calc(-1 * var(--space-2)); /* ajuste visual entre parágrafos */
}

/* ─── Links ──────────────────────────────────────────────────────────────────*/

.privacy-link {
  color: var(--color-primary);
  text-decoration: underline;
  text-decoration-color: color-mix(in srgb, var(--color-primary) 40%, transparent);
  text-underline-offset: 3px;
  font-weight: var(--weight-medium);
  transition: var(--transition-base);
}

.privacy-link:hover {
  color: var(--color-primary-hover);
  text-decoration-color: var(--color-primary-hover);
}

.privacy-link--email {
  display: inline-flex;
  align-items: center;
  gap: var(--space-2);
  font-family: var(--font-body);
  font-size: var(--text-base);
}

/* ─── Rodapé da página ───────────────────────────────────────────────────────*/

.privacidade__footer {
  padding-top: var(--space-4);
  border-top: 1px solid var(--color-border);
}

/* ─── Mobile ─────────────────────────────────────────────────────────────────*/

@media (max-width: 640px) {
  .privacidade {
    padding: var(--space-10) var(--space-4);
  }

  .privacidade__inner {
    gap: var(--space-8);
  }
}
</style>
