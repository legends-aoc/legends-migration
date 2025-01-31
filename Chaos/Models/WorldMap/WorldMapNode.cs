using Chaos.Collections;
using Chaos.Models.World;
using Chaos.Networking.Entities.Server;
using Chaos.Storage.Abstractions;

namespace Chaos.Models.WorldMap;

public sealed record WorldMapNode : WorldMapNodeInfo
{
    private readonly ISimpleCache SimpleCache;

    public required string NodeKey { get; init; }
    public WorldMapNode(ISimpleCache simpleCache) => SimpleCache = simpleCache;

    public void OnClick(Aisling aisling)
    {
        var destinationMap = SimpleCache.Get<MapInstance>(Destination.Map);
        aisling.TraverseMap(destinationMap, Destination, fromWolrdMap: true);
        //destinationMap.AddObject(aisling, Destination);

        aisling.ActiveObject.Set(null);
    }
}