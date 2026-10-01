using System;
using System.Linq;
using MacroDeck.Localization;
using MacroDeck.Sdk;
using MacroDeck.Sdk.Actions;
using Helldivers2.Core;

namespace Helldivers2;

public sealed class HelldiversStratagemAction : IActionDefinition
{
    private const string StratagemParameter = "stratagem";

    public string Id => "helldivers-stratagem";

    public LocalizedText Name => "Helldivers 2 Stratagem";

    public LocalizedText Description =>
        "Executes the selected Helldivers 2 stratagem input sequence.";

    public IReadOnlyList<ActionParameter> Parameters { get; } =
    [
        ActionParameter.Choice(
            StratagemParameter,
            [
                .. StratagemRegistry
                    .GetAll()
                    .Select(stratagem => new ActionParameterOption
                    {
                        Value = stratagem.Id,
                        Label = stratagem.DisplayName,
                    }),
            ],
            label: "Stratagem",
            description: "Select the Helldivers 2 stratagem to execute.",
            defaultValue: StratagemRegistry.Default.Id
        ),
    ];

    public MacroDeckPlatform Platforms => MacroDeckPlatform.Windows;

    public IActionExecutor CreateExecutor() => new Executor();

    private sealed class Executor : IActionExecutor
    {
        public Task<ActionResult> ExecuteAsync(ActionExecutionContext context)
        {
            var stratagemId =
                context.Parameters.GetValueOrDefault(StratagemParameter)?.ToString();

            if (string.IsNullOrWhiteSpace(stratagemId))
            {
                return Task.FromResult(
                    ActionResult.Failed(
                        ActionErrorCodes.InvalidParameter,
                        "No stratagem was selected."
                    )
                );
            }

            if (!StratagemRegistry.TryGet(stratagemId, out var stratagem))
            {
                return Task.FromResult(
                    ActionResult.Failed(
                        ActionErrorCodes.NotFound,
                        "The selected stratagem could not be found."
                    )
                );
            }

            try
            {
                StratagemExecutor.Execute(stratagem);
                return ActionResult.SucceededTask;
            }
            catch (Exception)
            {
                return Task.FromResult(
                    ActionResult.Failed(
                        ActionErrorCodes.ProviderError,
                        "The stratagem could not be executed."
                    )
                );
            }
        }
    }
}
