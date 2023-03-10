using Chaos.Common.Definitions;
using Chaos.Formulae.Abstractions;
using Chaos.Objects.World.Abstractions;
using Chaos.Scripting.Abstractions;

namespace Chaos.Formulae.Damage;

public class PureDamageFormula : IDamageFormula
{
    /// <inheritdoc />
    public virtual int Calculate(
        Creature? source,
        Creature target,
        IScript script,
        int damage,
        Element? elementOverride = null
    ) => Convert.ToInt32(damage);
}