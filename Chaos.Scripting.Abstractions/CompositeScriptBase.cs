using System.Collections;
using System.Runtime.InteropServices;

namespace Chaos.Scripting.Abstractions;

/// <summary>
///     A script that is composed of multiple scripts
/// </summary>
/// <inheritdoc cref="Chaos.Scripting.Abstractions.ICompositeScript{T}"/>
/// <implements><see cref="Chaos.Scripting.Abstractions.ICompositeScript{T}"/>, <see cref="IScript"/></implements>
public abstract class CompositeScriptBase<TScript> : ScriptBase, ICompositeScript<TScript> where TScript: IScript
{
    protected List<TScript> Components { get; }

    protected CompositeScriptBase() => Components = new List<TScript>();

    public void Add(TScript script) => Components.Add(script);

    /// <inheritdoc />
    public T? GetComponent<T>() where T: TScript
    {
        foreach (var component in Components)
        {
            switch (component)
            {
                case T typedScript:
                    return typedScript;
                case ICompositeScript<T> composite:
                    return composite.GetComponent<T>();
                default:
                    continue;
            }
        }

        return default;
    }

    public IEnumerator<TScript> GetEnumerator()
    {
        foreach (var component in Components)
            if (component is ICompositeScript<TScript> composite)
            {
                yield return component;

                foreach (var subComponent in composite)
                    yield return subComponent;
            } else
                yield return component;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Remove(TScript script) => Components.Remove(script);
}