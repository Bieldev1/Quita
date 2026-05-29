using Microsoft.AspNetCore.Mvc;
using Quita.Api.Models;
using Quita.Api.Services;

namespace Quita.Api.Endpoints;

public static class SimulacaoEndpoint
{
    // Valores permitidos — validação manual sem FluentValidation
    private static readonly HashSet<string> TemposValidos  = ["90-180", "181-365", "365+"];
    private static readonly HashSet<string> TiposValidos   = ["cartao", "cheque", "pessoal"];

    private const decimal ValorMinimo = 100m;
    private const decimal ValorMaximo = 15_000m;

    public static void MapSimulacao(this WebApplication app)
    {
        app.MapPost("/api/simular", Handle)
           .WithName("Simular")
           .WithSummary("Simula renegociação pelo Novo Desenrola Brasil")
           .Produces<SimulacaoResponse>(StatusCodes.Status200OK)
           .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
           .WithOpenApi();
    }

    // Handler separado do registro — facilita testes unitários diretos
    internal static IResult Handle(
        SimulacaoRequest          request,
        ICalculoDesenrolaService  service)
    {
        var erros = Validar(request);
        if (erros.Count > 0)
        {
            // Retorna 400 com dicionário de erros por campo (padrão ProblemDetails)
            return Results.ValidationProblem(erros);
        }

        var resultado = service.Calcular(request);
        return Results.Ok(resultado);
    }

    private static Dictionary<string, string[]> Validar(SimulacaoRequest r)
    {
        var erros = new Dictionary<string, string[]>();

        // valorDivida
        if (r.ValorDivida < ValorMinimo || r.ValorDivida > ValorMaximo)
            erros["valorDivida"] = [$"Deve estar entre R$ {ValorMinimo:N2} e R$ {ValorMaximo:N2}."];

        // tempoAtraso
        if (string.IsNullOrWhiteSpace(r.TempoAtraso) || !TemposValidos.Contains(r.TempoAtraso))
            erros["tempoAtraso"] = ["Deve ser '90-180', '181-365' ou '365+'."];

        // tipoDivida
        if (string.IsNullOrWhiteSpace(r.TipoDivida) || !TiposValidos.Contains(r.TipoDivida))
            erros["tipoDivida"] = ["Deve ser 'cartao', 'cheque' ou 'pessoal'."];

        // saldoFgts (opcional — só valida se informado e > 0)
        if (r.SaldoFgts < 0 || r.SaldoFgts > ValorMaximo)
            erros["saldoFgts"] = [$"Deve estar entre R$ 0 e R$ {ValorMaximo:N2}."];

        return erros;
    }
}
