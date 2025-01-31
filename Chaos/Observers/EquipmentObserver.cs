using Chaos.Common.Definitions;
using Chaos.Models.Panel;
using Chaos.Models.World;

namespace Chaos.Observers;

public sealed class EquipmentObserver : Abstractions.IObserver<Item>
{
    private readonly Aisling Aisling;

    public EquipmentObserver(Aisling aisling) => Aisling = aisling;

    public void OnAdded(Item obj)
    {
        Aisling.Client.SendEquipment(obj);
        Aisling.Display();
        Aisling.UserStatSheet.AddWeight(obj.Weight);

        if (obj.Modifiers != null)
            Aisling.UserStatSheet.AddBonus(obj.Modifiers);

        Aisling.Client.SendAttributes(StatUpdateType.Full);
        obj.Script.OnEquipped(Aisling);
    }

    public void OnRemoved(byte slot, Item obj)
    {
        Aisling.Client.SendUnequip((EquipmentSlot)slot);
        Aisling.Display();
        Aisling.Client.SendSelfProfile();

        Aisling.UserStatSheet.AddWeight(-obj.Weight);

        if (obj.Modifiers != null)
            Aisling.UserStatSheet.SubtractBonus(obj.Modifiers);

        Aisling.Client.SendAttributes(StatUpdateType.Full);
        obj.Script.OnUnEquipped(Aisling);
    }

    public void OnUpdated(byte originalSlot, Item obj)
    {
        //uhhhhh nothing for now i guess
    }
}