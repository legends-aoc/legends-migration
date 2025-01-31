using Chaos.Models.Panel;
using Chaos.Models.Templates;
using Chaos.Scripting.Abstractions;
using Chaos.Services.Factories.Abstractions;
using Chaos.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace Chaos.Services.Factories;

public sealed class ItemFactory : IItemFactory
{
    private readonly ILogger<ItemFactory> Logger;
    private readonly IScriptProvider ScriptProvider;
    private readonly ISimpleCache SimpleCache;

    public ItemFactory(
        ISimpleCache simpleCache,
        IScriptProvider scriptProvider,
        ILogger<ItemFactory> logger
    )
    {
        SimpleCache = simpleCache;
        ScriptProvider = scriptProvider;
        Logger = logger;
    }

    public Item Create(string templateKey, ICollection<string>? extraScriptKeys = null)
    {
        extraScriptKeys ??= new List<string>();
        var template = SimpleCache.Get<ItemTemplate>(templateKey);
        var item = new Item(template, ScriptProvider, extraScriptKeys);

        Logger.LogDebug("Created {@Item}", item);

        return item;
    }

    /// <inheritdoc />
    public Item CreateFaux(string templateKey, ICollection<string>? extraScriptKeys = null)
    {
        extraScriptKeys ??= new List<string>();
        var template = SimpleCache.Get<ItemTemplate>(templateKey);

        //creates an item with a unique id of 0
        var item = new Item(
            template,
            ScriptProvider,
            extraScriptKeys,
            0);

        //no need to log creation of faux items

        return item;
    }
}