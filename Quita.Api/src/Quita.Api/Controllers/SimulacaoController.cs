using Microsoft.AspNetCore.Mvc;
using Quita.Application.UseCases.SimularDesenrola;
using Quita.Domain.Exceptions;

namespace Quita.Api.Controllers;

/// <summary>
/// Controller responsável pelos endpoints de simulação do Desenrola Brasil.
/// Segue Clean Architecture: não contém lógica de negócio —
/// apenas recebe a requisição, delega ao Use Case e traduz o resultado em HTTP.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class SimulacaoController : ControllerBase
{
    private readonly ISimularDesenrolaUseCase _useCase;

    public SimulacaoController(ISimularDesenrolaUseCase useCase)
    {
        _useCase = useCase;
    }

    /// <summary>Simula a renegociação de uma dívida pelo Novo Desenrola Brasil.</summary>
    /// <param name="command">Dados da dívida: valor, tempo de atraso, tipo e saldo FGTS opcional.</param>
    /// <returns>Resultado com desconto, parcelas e economia estimada.</returns>
    /// <response code="200">Simulação calculada com sucesso.</response>
    /// <response code="400">Dados inválidos — mensagem de erro retornada no corpo.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SimularDesenrolaResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),         StatusCodes.Status400BadRequest)]
    public IActionResult Simular([FromBody] SimularDesenrolaCommand command)
    {
        try
        {
            var result = _useCase.Executar(command);
            return Ok(result);
        }
        catch (DomainException ex)
        {
            // DomainException → 400 com ProblemDetails (RFC 7807)
            // Construído manualmente para incluir o campo que causou o erro
            var problem = new ProblemDetails
            {
                Title      = "Dados inválidos",
                Detail     = ex.Message,
                Status     = StatusCodes.Status400BadRequest,
            };
            problem.Extensions["campo"] = ex.Campo;
            return BadRequest(problem);
        }
    }
}
