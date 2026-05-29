namespace Quita.Application.UseCases.SimularDesenrola;

/// <summary>
/// Contrato do caso de uso — a camada de API depende desta abstração,
/// não da implementação concreta (Dependency Inversion Principle).
/// </summary>
public interface ISimularDesenrolaUseCase
{
    SimularDesenrolaResult Executar(SimularDesenrolaCommand command);
}
