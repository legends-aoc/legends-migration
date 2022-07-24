using Chaos.Containers.Abstractions;
using Chaos.Containers.Interfaces;
using Chaos.Data;
using Chaos.Objects.Panel;

namespace Chaos.Containers;

public class Equipment : PanelBase<Item>, IEquipment
{
    public Item? this[EquipmentSlot slot] => this[(byte)slot];
    public Attributes Modifiers { get; }

    public Equipment()
        : base(
            PanelType.Equipment,
            19,
            new byte[] { 0 }) => Modifiers = new Attributes();

    public bool TryEquip(Item item, out Item? returnedItem)
    {
        returnedItem = null;

        if (item.Template.EquipmentType is null or EquipmentType.NotEquipment)
            return false;

        using var @lock = Sync.Enter();

        var possibleSlots = item.Template.EquipmentType.Value.ToEquipmentSlots();
        byte bSlot = 0;

        //check for empty slots
        foreach (var slot in possibleSlots)
        {
            bSlot = (byte)slot;

            if (TryAdd(bSlot, item))
                return true;
        }

        //no slots empty? try to replace
        TryGetRemove(bSlot, out returnedItem);
        TryAdd(bSlot, item);

        return true;
    }
}