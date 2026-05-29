# ── Stage 1: build ────────────────────────────────────────────────────────────
# SDK completo apenas para compilar — não vai para a imagem final
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia só os arquivos de projeto primeiro para aproveitar o cache de camadas
# (restore só roda de novo se .csproj mudar)
COPY Quita.Api.csproj ./
RUN dotnet restore

# Copia o restante do código e publica em modo Release
COPY . .
RUN dotnet publish -c Release -o /app/out --no-restore

# ── Stage 2: runtime ───────────────────────────────────────────────────────────
# Imagem de runtime é ~3x menor que a SDK — sem compilador, sem ferramentas
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Cria usuário não-root por segurança (evita escalonamento de privilégio)
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Copia apenas o artefato publicado do stage anterior
COPY --from=build /app/out .

# Transfere ownership para o usuário não-root antes de trocar
RUN chown -R appuser:appgroup /app
USER appuser

# Render usa a porta 8080 por padrão
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Quita.Api.dll"]
