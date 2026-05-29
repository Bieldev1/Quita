using Quita.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
    options.AddPolicy("QuitaPolicy", policy =>
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "https://quita.vercel.app")
            .AllowAnyHeader()
            .AllowAnyMethod()));

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Serviços de aplicação ─────────────────────────────────────────────────────
builder.Services.AddApplicationServices();

// ── OpenAPI / Swagger ─────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new()
    {
        Title       = "Quita API",
        Version     = "v1",
        Description = "Simulador do Novo Desenrola Brasil — Clean Architecture + DDD",
    }));

var app = builder.Build();

// ── Pipeline ──────────────────────────────────────────────────────────────────
app.UseCors("QuitaPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();   // descobre todos os [ApiController] automaticamente

app.Run();

// Necessário para WebApplicationFactory nos testes de integração
public partial class Program;
