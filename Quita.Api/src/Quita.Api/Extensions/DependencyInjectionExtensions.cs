using Quita.Application.UseCases.SimularDesenrola;

namespace Quita.Api.Extensions;

/// <summary>
/// Centraliza o registro de dependências por camada.
/// Program.cs fica limpo — apenas orquestra, não conhece detalhes de DI.
/// </summary>
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Transient: SimularDesenrolaUseCase é stateless — nova instância por request
        services.AddTransient<ISimularDesenrolaUseCase, SimularDesenrolaUseCase>();
        return services;
    }
}
