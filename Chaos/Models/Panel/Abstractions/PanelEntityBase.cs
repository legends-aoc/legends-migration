using Chaos.Common.Identity;
using Chaos.Models.Templates.Abstractions;
using Chaos.Models.World;
using Chaos.Models.World.Abstractions;
using Chaos.Scripting.Abstractions;
using Chaos.Time.Abstractions;

namespace Chaos.Models.Panel.Abstractions;

/// <summary>
///     Represents an object that exists within the in-game panels.
/// </summary>
public abstract class PanelEntityBase : IDeltaUpdatable, IScripted
{
    public TimeSpan? Cooldown { get; set; }
    public TimeSpan? Elapsed { get; set; }
    public byte Slot { get; set; }
    public uint Id => (uint)UniqueId;
    public ISet<string> ScriptKeys { get; }
    public virtual PanelObjectTemplateBase Template { get; }
    public ulong UniqueId { get; }

    protected PanelEntityBase(PanelObjectTemplateBase template, ulong? uniqueId = null, int? elapsedMs = null)
    {
        uniqueId ??= PersistentIdGenerator<ulong>.Shared.NextId;
        Template = template;
        Cooldown = template.Cooldown;
        ScriptKeys = new HashSet<string>(template.ScriptKeys, StringComparer.OrdinalIgnoreCase);
        UniqueId = uniqueId.Value;

        if (elapsedMs.HasValue)
            Elapsed = TimeSpan.FromMilliseconds(elapsedMs.Value);
    }

    public virtual void BeginCooldown(Creature creature)
    {
        if (Cooldown is { Ticks: > 0 })
        {
            Elapsed ??= TimeSpan.Zero;

            if (creature is Aisling aisling)
                aisling.Client.SendCooldown(this);
        }
    }

    public virtual bool CanUse() => !Cooldown.HasValue || !Elapsed.HasValue || (Elapsed > Cooldown);

    public virtual void Update(TimeSpan delta)
    {
        if (!Elapsed.HasValue)
            return;

        var ts = Elapsed.Value + delta;
        Elapsed = ts;

        if (Elapsed > Cooldown)
            Elapsed = null;
    }
}