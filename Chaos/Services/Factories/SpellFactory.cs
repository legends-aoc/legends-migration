using Chaos.Models.Panel;
using Chaos.Models.Templates;
using Chaos.Scripting.Abstractions;
using Chaos.Services.Factories.Abstractions;
using Chaos.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace Chaos.Services.Factories;

public sealed class SpellFactory : ISpellFactory
{
    private readonly ILogger<SpellFactory> Logger;
    private readonly IScriptProvider ScriptProvider;
    private readonly ISimpleCache SimpleCache;

    public SpellFactory(
        ISimpleCache simpleCache,
        IScriptProvider scriptProvider,
        ILogger<SpellFactory> logger
    )
    {
        SimpleCache = simpleCache;
        ScriptProvider = scriptProvider;
        Logger = logger;
    }

    public Spell Create(string templateKey, ICollection<string>? extraScriptKeys = null)
    {
        extraScriptKeys ??= Array.Empty<string>();
        var template = SimpleCache.Get<SpellTemplate>(templateKey);
        var spell = new Spell(template, ScriptProvider, extraScriptKeys);

        Logger.LogDebug("Created {@Spell}", spell);

        return spell;
    }

    /// <inheritdoc />
    public Spell CreateFaux(string templateKey, ICollection<string>? extraScriptKeys = null)
    {
        extraScriptKeys ??= new List<string>();
        var template = SimpleCache.Get<SpellTemplate>(templateKey);

        //creates an spell with a unique id of 0
        var spell = new Spell(
            template,
            ScriptProvider,
            extraScriptKeys,
            0);

        //no need to log creation of faux spells

        return spell;
    }
}