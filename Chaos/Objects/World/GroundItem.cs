using Chaos.Common.Definitions;
using Chaos.Containers;
using Chaos.Data;
using Chaos.Extensions;
using Chaos.Geometry.Abstractions;
using Chaos.Objects.Abstractions;
using Chaos.Objects.Panel;
using Chaos.Objects.World.Abstractions;

namespace Chaos.Objects.World;

public sealed class GroundItem : GroundEntity, IDialogSourceEntity
{
    public Item Item { get; set; }

    /// <inheritdoc />
    DisplayColor IDialogSourceEntity.Color => Item.Color;

    /// <inheritdoc />
    EntityType IDialogSourceEntity.EntityType => EntityType.Item;

    public GroundItem(Item item, MapInstance mapInstance, IPoint point)
        : base(
            item.DisplayName,
            item.ItemSprite.OffsetPanelSprite,
            mapInstance,
            point) =>
        Item = item;

    /// <inheritdoc />
    public override void Animate(Animation animation, uint? sourceId = null)
    {
        var targetedAnimation = animation.GetTargetedAnimation(Id, sourceId);

        foreach (var obj in MapInstance.GetEntitiesWithinRange<Aisling>(this)
                                       .ThatCanSee(this))
            obj.Client.SendAnimation(targetedAnimation);
    }

    /// <inheritdoc />
    public override bool CanPickUp(Aisling source) => source.IsAdmin || (Owners?.Contains(source.Name) != false);

    public override void OnClicked(Aisling source)
    {
        //nothing
        //there's a different packet for picking up items
    }

    /// <inheritdoc />
    public override void Update(TimeSpan delta)
    {
        Item.Update(delta);
        base.Update(delta);
    }
}