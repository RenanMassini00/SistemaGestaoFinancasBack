using Financeiro.Application.Interfaces;
using Financeiro.Application.Interfaces.Services;
using Financeiro.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Financeiro.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        return services;
    }
}
