using System.Runtime.InteropServices;
using Chaos.Objects.World.Abstractions;
using Chaos.Scripting.Abstractions;
using Chaos.Scripts.MapScripts.Abstractions;

namespace Chaos.Scripts.MapScripts;

/// <summary>
///     DO NOT EDIT THIS SCRIPT
/// </summary>
public class CompositeMapScript : CompositeScriptBase<IMapScript>, IMapScript
{
    /// <summary>
    ///     DO NOT EDIT THIS SCRIPT
    /// </summary>
    public virtual void OnEntered(Creature creature)
    {
        foreach (ref var component in CollectionsMarshal.AsSpan(Components))
            component.OnEntered(creature);
    }

    /// <summary>
    ///     DO NOT EDIT THIS SCRIPT
    /// </summary>
    public virtual void OnExiting(Creature creature)
    {
        foreach (ref var component in CollectionsMarshal.AsSpan(Components))
            component.OnExiting(creature);
    }
}