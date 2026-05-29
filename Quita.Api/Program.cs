using Quita.Api.Endpoints;
using Quita.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ── CORS ──────────────────────────────────────────────────────────────────────
// Vite dev server em localhost:5173 + domínio de produção no Vercel.
// Em produção, a variável ALLOWED_ORIGIN sobrescreve o padrão.
var allowedOrigins = new[]
{
    "http://localhost:5173",
    "https://quita.vercel.app",
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("QuitaPolicy", policy =>
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// ── Serviços ──────────────────────────────────────────────────────────────────
// Transient: o serviço é stateless (cálculo puro), instância nova a cada request.
builder.Services.AddTransient<ICalculoDesenrolaService, CalculoDesenrolaService>();

// Swagger/OpenAPI (útil em desenvolvimento)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title       = "Quita API",
        Version     = "v1",
        Description = "Simulador do Novo Desenrola Brasil — cálculo de renegociação de dívidas.",
    });
});

// ── Pipeline ──────────────────────────────────────────────────────────────────
var app = builder.Build();

app.UseCors("QuitaPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ── Endpoints ─────────────────────────────────────────────────────────────────
app.MapSimulacao();

app.Run();

// Torna a classe parcial visível para testes de integração (WebApplicationFactory)
public partial class Program;
