# ── Stage 1: build ────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos de projeto primeiro — camada de cache do Docker
# Se só o código mudar (não os .csproj), o restore não roda de novo
COPY Quita.sln .
COPY src/Quita.Domain/Quita.Domain.csproj          src/Quita.Domain/
COPY src/Quita.Application/Quita.Application.csproj src/Quita.Application/
COPY src/Quita.Api/Quita.Api.csproj                 src/Quita.Api/

RUN dotnet restore

# Copia todo o código e publica o projeto de entrada (Quita.Api)
COPY . .
RUN dotnet publish src/Quita.Api/Quita.Api.csproj \
    -c Release \
    -o /app/out \
    --no-restore

# ── Stage 2: runtime ───────────────────────────────────────────────────────────
# Imagem de runtime ~220MB vs ~800MB do SDK — sem compilador
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Usuário não-root — boa prática de segurança em containers
RUN addgroup --system appgroup \
 && adduser  --system --ingroup appgroup appuser

COPY --from=build /app/out .
RUN chown -R appuser:appgroup /app
USER appuser

# Porta padrão do Render Free
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Quita.Api.dll"]
