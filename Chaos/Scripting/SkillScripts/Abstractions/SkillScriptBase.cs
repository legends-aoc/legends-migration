using Chaos.Models.Data;
using Chaos.Models.Panel;
using Chaos.Models.World;
using Chaos.Scripting.Abstractions;

namespace Chaos.Scripting.SkillScripts.Abstractions;

public abstract class SkillScriptBase : SubjectiveScriptBase<Skill>, ISkillScript
{
    /// <inheritdoc />
    protected SkillScriptBase(Skill subject)
        : base(subject) { }

    /// <inheritdoc />
    public virtual bool CanUse(ActivationContext context) => context.Source.IsAlive;

    /// <inheritdoc />
    public virtual void OnForgotten(Aisling aisling) { }

    /// <inheritdoc />
    public virtual void OnLearned(Aisling aisling) { }

    /// <inheritdoc />
    public virtual void OnUse(ActivationContext context) { }

    /// <inheritdoc />
    public virtual void Update(TimeSpan delta) { }
}