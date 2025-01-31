using Chaos.Extensions.Common;
using Chaos.Models.Data;
using Chaos.Models.Panel.Abstractions;
using Chaos.Models.Templates;
using Chaos.Models.World.Abstractions;
using Chaos.Scripting.Abstractions;
using Chaos.Scripting.SkillScripts.Abstractions;

namespace Chaos.Models.Panel;

/// <summary>
///     Represents an object that exists within the skill panel.
/// </summary>
public sealed class Skill : PanelEntityBase, IScripted<ISkillScript>
{
    public string PanelDisplayName { get; }
    public ISkillScript Script { get; }
    public override SkillTemplate Template { get; }

    public Skill(
        SkillTemplate template,
        IScriptProvider scriptProvider,
        ICollection<string>? extraScriptKeys = null,
        ulong? uniqueId = null,
        int? elapsedMs = null
    )
        : base(template, uniqueId, elapsedMs)
    {
        Template = template;

        if (extraScriptKeys != null)
            ScriptKeys.AddRange(extraScriptKeys);

        if (template.IsAssail && !Cooldown.HasValue)
            Cooldown = TimeSpan.Zero;

        Script = scriptProvider.CreateScript<ISkillScript, Skill>(ScriptKeys, this);
        PanelDisplayName = $"{Template.Name} (Lev:100/100)";
    }

    /// <inheritdoc />
    public override void BeginCooldown(Creature creature)
    {
        //don't send cooldowns for assails
        if (Template.IsAssail)
        {
            Elapsed ??= TimeSpan.Zero;

            return;
        }

        base.BeginCooldown(creature);
    }

    /// <inheritdoc />
    public override void Update(TimeSpan delta)
    {
        base.Update(delta);
        Script.Update(delta);
    }

    public void Use(ActivationContext context)
    {
        Script.OnUse(context);

        context.Source.LastAttack = DateTime.UtcNow;

        //for assails, their cooldowns are re-calculated each time they are used
        //the cooldown is based on the soruce's assail interval and atk speed
        if (Template.IsAssail)
        {
            var assailInterval = context.Source.StatSheet.CalculateEffectiveAssailInterval(context.Source.AssailIntervalMs);
            Cooldown = TimeSpan.FromMilliseconds(assailInterval);
        }

        BeginCooldown(context.Source);
    }
}