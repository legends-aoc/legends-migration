using Chaos.Models.Panel;
using Chaos.Models.World;

namespace Chaos.Observers;

public sealed class SkillBookObserver : Abstractions.IObserver<Skill>
{
    private readonly Aisling Aisling;

    public SkillBookObserver(Aisling aisling) => Aisling = aisling;

    public void OnAdded(Skill obj) => Aisling.Client.SendAddSkillToPane(obj);

    public void OnRemoved(byte slot, Skill obj) => Aisling.Client.SendRemoveSkillFromPane(slot);

    public void OnUpdated(byte originalSlot, Skill obj) => Aisling.Client.SendAddSkillToPane(obj);
}