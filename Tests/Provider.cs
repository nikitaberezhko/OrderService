using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Extensions;

namespace Tests;

public static class Provider
{
    private static readonly ServiceProvider _serviceProvider;

    static Provider()
    {
        var services = new ServiceCollection();

        services.AddServices();
        services.AddMappers();
        services.AddValidation();
        // services.AddMassTransit();
        services.AddMassTransitTestHarness();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    public static T Get<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}