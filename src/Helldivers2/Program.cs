using MacroDeck.Plugin.Hosting;
using MacroDeck.Plugin.Serilog;
using Helldivers2;

var plugin = MacroDeckPlugin.CreatePlugin(args)
.UseMacroDeckLogging()
.UseLocalization(Strings.LocalizationCatalog)
.RegisterIntegration<PluginIntegration>()
.Build();

await plugin.RunAsync();
