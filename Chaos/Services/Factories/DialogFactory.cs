using Chaos.Models.Abstractions;
using Chaos.Models.Menu;
using Chaos.Models.Templates;
using Chaos.Scripting.Abstractions;
using Chaos.Services.Factories.Abstractions;
using Chaos.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace Chaos.Services.Factories;

public sealed class DialogFactory : IDialogFactory
{
    private readonly ILogger<DialogFactory> Logger;
    private readonly IScriptProvider ScriptProvider;
    private readonly ISimpleCache SimpleCache;

    public DialogFactory(ISimpleCache simpleCache, IScriptProvider scriptProvider, ILogger<DialogFactory> logger)
    {
        SimpleCache = simpleCache;
        ScriptProvider = scriptProvider;
        Logger = logger;
    }

    /// <inheritdoc />
    public Dialog Create(string templateKey, IDialogSourceEntity source, ICollection<string>? extraScriptKeys = null)
    {
        var template = SimpleCache.Get<DialogTemplate>(templateKey);

        var dialog = new Dialog(
            template,
            source,
            ScriptProvider,
            this,
            extraScriptKeys);

        Logger.LogDebug("Created {@Dialog} for {@Entity}", dialog, source);

        return dialog;
    }
}