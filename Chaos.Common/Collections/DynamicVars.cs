using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using Chaos.Common.Abstractions;
using Chaos.Common.Converters;

// ReSharper disable once CheckNamespace
namespace Chaos.Collections.Common;

/// <summary>
///     An object used to dynamically retreive variables of uncertain types
/// </summary>
[JsonConverter(typeof(DynamicVarsConverter))]
public sealed class DynamicVars : IEnumerable<KeyValuePair<string, JsonElement>>, IScriptVars
{
    private readonly JsonSerializerOptions JsonOptions;
    private readonly ConcurrentDictionary<string, JsonElement> Vars;

    public DynamicVars(IDictionary<string, JsonElement> collection, JsonSerializerOptions options)
    {
        JsonOptions = options;
        Vars = new ConcurrentDictionary<string, JsonElement>(collection, StringComparer.OrdinalIgnoreCase);
    }

    public DynamicVars(JsonSerializerOptions options)
    {
        JsonOptions = options;
        Vars = new ConcurrentDictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public bool ContainsKey(string key) => Vars.ContainsKey(key);

    /// <inheritdoc />
    public T? Get<T>(string key) => Vars.TryGetValue(key, out var value) ? value.Deserialize<T>(JsonOptions) : default;

    /// <inheritdoc />
    public object? Get(Type type, string key)
    {
        if (!Vars.TryGetValue(key, out var value))
            return default;

        return value.Deserialize(type, JsonOptions);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, JsonElement>> GetEnumerator() => Vars.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public T GetRequired<T>(string key) =>
        (Vars.TryGetValue(key, out var value)
            ? value.Deserialize<T>(JsonOptions)
            : throw new KeyNotFoundException($"Required key \"{key}\" was not found while populating script variables"))
        ?? throw new NullReferenceException(
            $"Required key \"{key}\" was found, but resulted in a default value while populating script variables");
}