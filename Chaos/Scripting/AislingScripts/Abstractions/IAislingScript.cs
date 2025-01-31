using Chaos.Models.Panel;
using Chaos.Models.World;
using Chaos.Models.World.Abstractions;
using Chaos.Scripting.Abstractions;

namespace Chaos.Scripting.AislingScripts.Abstractions;

public interface IAislingScript : ICreatureScript
{
    bool CanUseItem(Item item);
    void OnClicked(Aisling source);
    void OnDeath(Creature source);
    void OnGoldDroppedOn(Aisling source, int amount);
    void OnItemDroppedOn(Aisling source, Item item);
}