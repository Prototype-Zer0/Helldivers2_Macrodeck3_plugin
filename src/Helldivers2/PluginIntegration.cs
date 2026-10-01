using MacroDeck.Sdk;
using MacroDeck.Sdk.Actions;
using Serilog;

namespace Helldivers2;

public sealed class PluginIntegration : IPluginIntegration
{
    private readonly ILogger _logger;

    public PluginIntegration(ILogger logger)
    {
        _logger = logger.ForContext<PluginIntegration>();

        Actions = [new HelldiversStratagemAction()];

    }

    public IReadOnlyList<IActionDefinition> Actions { get; }

    public Task InitializeAsync(IIntegrationContext context)
    {
        _logger.Information("Initialized.");
        return Task.CompletedTask;
    }

    public Task ShutdownAsync() => Task.CompletedTask;
}
