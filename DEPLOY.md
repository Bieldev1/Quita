# Guia de Deploy — Quita

## Por que o `vercel.json` é necessário

O Vue Router com `createWebHistory` usa URLs reais (ex: `/simulador`, `/sobre`) em vez
de hashes (`/#/simulador`). Quando o usuário acessa ou recarrega uma dessas rotas
diretamente, o servidor procura o arquivo físico — que não existe, pois o Vue é uma SPA
com apenas `index.html`. O rewrite abaixo diz ao Vercel para entregar sempre o
`index.html` e deixar o Vue Router resolver a rota no cliente:

```json
{ "rewrites": [{ "source": "/(.*)", "destination": "/index.html" }] }
```

Sem esse arquivo você recebe **404** ao recarregar qualquer página que não seja `/`.

---

## 1. Deploy do backend no Render

### Pré-requisitos
- Conta em [render.com](https://render.com) (plano Free é suficiente)
- Repositório no GitHub com a pasta `Quita.Api/`

### Passo a passo

1. No dashboard do Render, clique **New → Web Service**
2. Conecte seu repositório GitHub
3. Configure o serviço:
   - **Name:** `quita-api`
   - **Root Directory:** `Quita.Api`
   - **Runtime:** Docker
   - **Dockerfile Path:** `./Dockerfile`
   - **Instance Type:** Free
4. Em **Environment Variables**, adicione:

   | Chave                    | Valor        |
   |--------------------------|--------------|
   | `ASPNETCORE_ENVIRONMENT` | `Production` |
   | `ASPNETCORE_URLS`        | `http://+:8080` |

5. Clique **Create Web Service**
6. Aguarde o build (~3–5 min no primeiro deploy)
7. Anote a URL gerada: `https://quita-api.onrender.com` ← use no próximo passo

> **Atenção — plano Free:** o serviço hiberna após 15 min sem requisições.
> O primeiro request após hibernação demora ~30s (cold start).
> Para evitar isso em produção, use o plano Starter ($7/mês) ou configure um
> cron externo (UptimeRobot, cron-job.org) para pingar `/api/simular` a cada 10 min.

---

## 2. Deploy do frontend no Vercel

### Pré-requisitos
- Conta em [vercel.com](https://vercel.com)
- Mesmo repositório GitHub

### Passo a passo

1. No dashboard da Vercel, clique **Add New → Project**
2. Importe o repositório GitHub
3. Configure:
   - **Framework Preset:** Vite
   - **Root Directory:** `.` (raiz do repo)
   - **Build Command:** `npm run build`
   - **Output Directory:** `dist`
4. Em **Environment Variables**, adicione:

   | Chave          | Valor (Production)                    |
   |----------------|---------------------------------------|
   | `VITE_API_URL` | `https://quita-api.onrender.com`      |

5. Clique **Deploy**
6. Após o deploy, a Vercel gera uma URL: `https://quita.vercel.app`

> O `vercel.json` já está na raiz e é detectado automaticamente.

---

## 3. Teste local com frontend + backend juntos

```bash
# Terminal 1 — backend
cd Quita.Api
dotnet run
# API disponível em http://localhost:5001

# Terminal 2 — frontend
cd ..
npm run dev
# Frontend em http://localhost:5174
# .env.development já aponta VITE_API_URL=http://localhost:5001
```

Para testar o Dockerfile localmente:

```bash
cd Quita.Api
docker build -t quita-api .
docker run -p 8080:8080 quita-api
# API em http://localhost:8080
```

---

## 4. Variáveis de ambiente por plataforma

### Render (backend)

| Variável                 | Valor            | Obrigatória |
|--------------------------|------------------|-------------|
| `ASPNETCORE_ENVIRONMENT` | `Production`     | Sim         |
| `ASPNETCORE_URLS`        | `http://+:8080`  | Sim         |

### Vercel (frontend)

| Variável       | Valor                             | Obrigatória |
|----------------|-----------------------------------|-------------|
| `VITE_API_URL` | URL do serviço Render             | Sim         |

### Local (`.env.development`)

| Variável       | Valor                    |
|----------------|--------------------------|
| `VITE_API_URL` | `http://localhost:5001`  |

---

## 5. Checklist de SEO básico

### Meta tags (adicionar no `index.html`)

```html
<!-- Primárias -->
<meta name="description" content="Simule gratuitamente sua renegociação pelo Novo Desenrola Brasil. Descubra o desconto, as parcelas e quanto você economiza. Sem cadastro." />
<meta name="keywords" content="Desenrola Brasil, renegociar dívida, limpar nome, simulador desenrola, desconto dívida" />
<link rel="canonical" href="https://quita.vercel.app" />

<!-- Open Graph (WhatsApp, Facebook) -->
<meta property="og:type"        content="website" />
<meta property="og:url"         content="https://quita.vercel.app" />
<meta property="og:title"       content="Quita — Simulador do Desenrola Brasil" />
<meta property="og:description" content="Descubra quanto você pode economizar para limpar seu nome. Simulação gratuita e sem cadastro." />
<meta property="og:image"       content="https://quita.vercel.app/og-image.png" />

<!-- Twitter Card -->
<meta name="twitter:card"        content="summary_large_image" />
<meta name="twitter:title"       content="Quita — Simulador do Desenrola Brasil" />
<meta name="twitter:description" content="Simule sua renegociação pelo Desenrola. Grátis e sem cadastro." />
<meta name="twitter:image"       content="https://quita.vercel.app/og-image.png" />
```

### `public/robots.txt`

```
User-agent: *
Allow: /

Sitemap: https://quita.vercel.app/sitemap.xml
```

### `public/sitemap.xml`

```xml
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
  <url>
    <loc>https://quita.vercel.app/</loc>
    <lastmod>2026-05-29</lastmod>
    <changefreq>weekly</changefreq>
    <priority>1.0</priority>
  </url>
</urlset>
```

### Imagem OG
- Crie `public/og-image.png` — 1200×630px
- Conteúdo sugerido: logo Quita + tagline + fundo com a paleta teal

### Google Search Console
1. Acesse [search.google.com/search-console](https://search.google.com/search-console)
2. Adicione propriedade com a URL do Vercel
3. Valide via tag HTML no `index.html` ou arquivo no `public/`
4. Envie o sitemap: `https://quita.vercel.app/sitemap.xml`

---

## 6. Como solicitar o Google AdSense

### Pré-requisitos mínimos (estimativa — Google não publica oficialmente)
- Site no ar há pelo menos 2–4 semanas
- Conteúdo original e útil (o simulador já atende)
- Política de privacidade publicada (crie `/privacidade`)
- Tráfego orgânico mínimo (algumas dezenas de visitas/dia ajudam)

### Passo a passo
1. Acesse [adsense.google.com](https://adsense.google.com) e crie uma conta
2. Informe a URL `https://quita.vercel.app`
3. Cole o snippet de verificação no `<head>` do `index.html`:
   ```html
   <script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-XXXXXXXXXXXXXXXX" crossorigin="anonymous"></script>
   ```
4. Aguarde a revisão (normalmente 1–14 dias)
5. Após aprovação, crie unidades de anúncio no painel e insira os snippets nos componentes Vue
6. Posicionamentos recomendados para não prejudicar UX:
   - Entre `HeroSection` e `SimuladorForm` (banner 728×90 desktop / 320×50 mobile)
   - Após `ResultadoSimulacao` (antes do `ComoFunciona`)

---

## 7. Programas de afiliado — Serasa e Acordo Certo

### Serasa Limpa Nome
- **Programa:** Serasa Parceiros / Afiliados
- **Cadastro:** [serasa.com.br/parceiros](https://www.serasa.com.br) → busque "Afiliados" ou contate via formulário de parceria
- **Comissão típica:** CPA (custo por ação) — valor por negociação concluída
- **Integração:** link de afiliado com UTM + pixel de conversão
- **No Quita:** adicione o link de afiliado no botão "Quero negociar agora" para dívidas cujo credor seja Serasa

### Acordo Certo
- **Programa:** [acordocerto.com.br/afiliados](https://www.acordocerto.com.br)
- **Rede de afiliados:** opera via Lomadee / Awin — cadastre-se na rede primeiro
- **Comissão típica:** CPA por acordo firmado
- **Integração:** deep link para a plataforma com tracking de afiliado
- **No Quita:** exiba o banner após a simulação como alternativa ao portal do governo

### Boas práticas legais
- Divulgue a relação comercial ao usuário (ex: "Este link é de parceiro — pode gerar comissão para o Quita")
- Inclua na política de privacidade o uso de cookies de rastreamento de afiliados
- Siga as diretrizes do CONAR e da LGPD para divulgação de parcerias remuneradas
