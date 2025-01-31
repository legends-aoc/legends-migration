namespace Chaos.Collections;

/*
public sealed class GuildRank
{
    /// <summary>
    ///     The name of the rank
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     The tier of the rank. Lower is better
    /// </summary>
    public int Tier { get; }

    /// <summary>
    ///     The names of the members of this rank
    /// </summary>
    private readonly HashSet<string> MemberNames;

    public GuildRank(string name, int tier, ICollection<string>? memberNames = null)
    {
        memberNames ??= Array.Empty<string>();

        Name = name;
        Tier = tier;
        MemberNames = new HashSet<string>(memberNames, StringComparer.OrdinalIgnoreCase);
    }

    public void ChangeRankName(string newName, IEnumerable<IWorldClient> clientRegistry)
    {
        Name = newName;

        foreach (var member in GetOnlineMembers(clientRegistry))
            member.GuildTitle = Name;
    }

    public void AddMember(string memberName) => MemberNames.Add(memberName);
    public bool RemoveMember(string memberName) => MemberNames.Remove(memberName);
    public bool Contains(string memberName) => MemberNames.Contains(memberName);

    public IEnumerable<Aisling> GetOnlineMembers(IEnumerable<IWorldClient> clientRegistry) =>
        clientRegistry.Where(cli => MemberNames.Contains(cli.Aisling.Name)).Select(cli => cli.Aisling);
}

public sealed class Guild
{
    private readonly IClientRegistry<IWorldClient> ClientRegistry;
    private readonly IChannelService ChannelService;
    private readonly AutoReleasingMonitor Sync;
    private readonly Dictionary<string, HashSet<string>> GuildHierarchy;
    public string Name { get; }

    public Guild(string name, IChannelService channelService, IClientRegistry<IWorldClient> clientRegistry)
    {
        Name = name;
        ChannelService = channelService;
        ClientRegistry = clientRegistry;
        GuildHierarchy = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        Sync = new AutoReleasingMonitor();
    }

    public void AddMember(Aisling aisling)
    {
        using var @lock = Sync.Enter();

        if (!string.IsNullOrEmpty(aisling.GuildName))
        {
            if (aisling.GuildName.EqualsI(Name))
                return;

            throw new InvalidOperationException(
                $"Attempted to add \"{aisling.Name}\" to guild \"{Name}\", but that player is already in guild \"{aisling.GuildName}\"");
        }

        aisling.GuildName = Name;
    }

    public IEnumerable<string> GetMemberNames()
    {
        List<string> names;

        using (Sync.Enter())
            names = GuildHierarchy.Values.SelectMany(x => x).ToList();

        return names;
    }

    public IEnumerable<Aisling> GetOnlineMembers()
    {
        List<Aisling> onlineMembers;

        using (Sync.Enter())
            onlineMembers = ClientRegistry.Where(cli => MemberNames.Contains(cli.Aisling.Name)).Select(cli => cli.Aisling).ToList();

        return onlineMembers;
    }
}
*/