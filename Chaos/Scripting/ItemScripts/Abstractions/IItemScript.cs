using Chaos.Collections;
using Chaos.Models.World;
using Chaos.Models.World.Abstractions;
using Chaos.Scripting.Abstractions;
using Chaos.Time.Abstractions;

// ReSharper disable UnusedParameter.Global

namespace Chaos.Scripting.ItemScripts.Abstractions;

public interface IItemScript : IScript, IDeltaUpdatable
{
    bool CanUse(Aisling source);
    void OnDropped(Creature source, MapInstance mapInstance);

    void OnEquipped(Aisling aisling);

    void OnPickup(Aisling aisling);
    void OnUnEquipped(Aisling aisling);

    void OnUse(Aisling source);
}