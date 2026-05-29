<template>
  <footer class="footer">
    <div class="footer__inner">

      <!-- Bloco superior: logo + tagline -->
      <div class="footer__brand">
        <div class="footer__logo" aria-label="Quita">
          <span class="footer__logo-symbol" aria-hidden="true">✦</span>
          <span class="footer__logo-name">Quita</span>
        </div>
        <p class="footer__tagline">Simule grátis. Negocie com segurança 💚</p>
      </div>

      <!-- Links discretos -->
      <nav class="footer__nav" aria-label="Links do rodapé">
        <RouterLink class="footer__link" to="/privacidade">Privacidade</RouterLink>
        <span class="footer__separator" aria-hidden="true">·</span>
        <a class="footer__link" href="#como-funciona">Como funciona</a>
      </nav>

    </div>

    <!-- Linha inferior: copyright + aviso legal -->
    <div class="footer__bottom">
      <div class="footer__bottom-inner">
        <p class="footer__copy">
          © 2026 Quita
          <span class="footer__separator" aria-hidden="true">·</span>
          Ferramenta gratuita
          <span class="footer__separator" aria-hidden="true">·</span>
          Não somos banco
        </p>

        <!-- Aviso legal: fonte menor, cor mais suave — presente mas sem assustar -->
        <p class="footer__legal">
          As simulações são estimativas baseadas nas regras do Novo Desenrola Brasil.
          Consulte sua instituição financeira para condições definitivas.
        </p>
      </div>
    </div>
  </footer>
</template>

<script setup>
// Footer é puramente estático — sem lógica necessária
</script>

<style scoped>
.footer {
  background-color: var(--color-bg-subtle);
  border-top: 1px solid var(--color-border);
  /* Margem do topo separa o footer do conteúdo principal */
  margin-top: var(--space-3xl);
}

.footer__inner {
  max-width: var(--container-xl);
  margin: 0 auto;
  padding: var(--space-8) var(--space-6);
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-6);
}

/* ─── Brand ────────────────────────────────────────────────────────────────── */

.footer__brand {
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
}

.footer__logo {
  display: flex;
  align-items: center;
  gap: var(--space-2);
}

.footer__logo-symbol {
  font-size: var(--text-md);
  color: var(--color-secondary);
  line-height: 1;
}

.footer__logo-name {
  font-family: var(--font-display);
  font-size: var(--text-md);
  font-weight: var(--weight-bold);
  color: var(--color-primary);
  letter-spacing: var(--tracking-tight);
  line-height: 1;
}

.footer__tagline {
  font-family: var(--font-body);
  font-size: var(--text-sm);
  color: var(--color-text-secondary);
  font-weight: var(--weight-medium);
  /* Aspas tipográficas reforçam o tom de promessa / slogan */
  font-style: italic;
}

/* ─── Nav de links ─────────────────────────────────────────────────────────── */

.footer__nav {
  display: flex;
  align-items: center;
  gap: var(--space-3);
  flex-shrink: 0;
}

.footer__link {
  font-family: var(--font-body);
  font-size: var(--text-sm);
  font-weight: var(--weight-medium);
  color: var(--color-text-secondary);
  text-decoration: none;
  transition: color var(--duration-normal) var(--ease-out-quart);
}

.footer__link:hover {
  color: var(--color-primary);
}

.footer__link:focus-visible {
  outline: 2px solid var(--color-focus-ring);
  outline-offset: 2px;
  border-radius: var(--radius-xs);
}

.footer__separator {
  color: var(--color-text-tertiary);
  font-size: var(--text-sm);
  user-select: none;
}

/* ─── Linha inferior ───────────────────────────────────────────────────────── */

.footer__bottom {
  border-top: 1px solid var(--color-border);
}

.footer__bottom-inner {
  max-width: var(--container-xl);
  margin: 0 auto;
  padding: var(--space-5) var(--space-6);
  display: flex;
  flex-direction: column;
  gap: var(--space-2);
}

.footer__copy {
  font-family: var(--font-body);
  font-size: var(--text-sm);
  color: var(--color-text-secondary);
  display: flex;
  align-items: center;
  gap: var(--space-2);
  flex-wrap: wrap;
}

/* Aviso legal: discreto mas presente — requisito de transparência */
.footer__legal {
  font-family: var(--font-body);
  font-size: var(--text-xs);
  color: var(--color-text-tertiary);
  line-height: var(--leading-relaxed);
  max-width: 72ch;
}

/* ─── Mobile ───────────────────────────────────────────────────────────────── */

@media (max-width: 640px) {
  .footer__inner {
    flex-direction: column;
    align-items: flex-start;
    padding: var(--space-6) var(--space-4);
    gap: var(--space-5);
  }

  .footer__bottom-inner {
    padding: var(--space-4);
  }

  .footer__copy {
    gap: var(--space-1);
  }
}
</style>
