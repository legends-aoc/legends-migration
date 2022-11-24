using System.Text.Json;
using Chaos.Common.Utilities;
using Chaos.Containers;
using Chaos.Data;
using Chaos.Extensions;
using Chaos.Factories.Abstractions;
using Chaos.Pathfinding.Abstractions;
using Chaos.Schemas.Aisling;
using Chaos.Schemas.Content;
using Chaos.Storage.Abstractions;
using Chaos.Storage.Options;
using Chaos.TypeMapper.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chaos.Storage;

public sealed class MapInstanceCache : SimpleFileCacheBase<MapInstance, MapInstanceSchema, MapInstanceCacheOptions>
{
    private readonly IMerchantFactory MerchantFactory;
    private readonly IReactorTileFactory ReactorTileFactory;
    private readonly IPathfindingService PathfindingService;

    /// <inheritdoc />
    protected override Func<MapInstance, string> KeySelector => m => m.InstanceId;

    public MapInstanceCache(
        ITypeMapper mapper,
        IPathfindingService pathfindingService,
        IOptions<JsonSerializerOptions> jsonSerializerOptions,
        IOptionsSnapshot<MapInstanceCacheOptions> options,
        ILogger<MapInstanceCache> logger,
        IMerchantFactory merchantFactory,
        IReactorTileFactory reactorTileFactory
    )
        : base(
            mapper,
            jsonSerializerOptions,
            options,
            logger)
    {
        PathfindingService = pathfindingService;
        MerchantFactory = merchantFactory;
        ReactorTileFactory = reactorTileFactory;

        if (!Directory.Exists(Options.Directory))
            Directory.CreateDirectory(Options.Directory);

        AsyncHelpers.RunSync(ReloadAsync);
    }

    protected override async Task<MapInstance?> LoadFromFileAsync(string directory)
    {
        var mapInstancePath = Path.Combine(directory, "instance.json");
        var monsterSpawnsPath = Path.Combine(directory, "monsters.json");
        var merchantSpawnsPath = Path.Combine(directory, "merchants.json");
        var reactorsPath = Path.Combine(directory, "reactors.json");

        await using var mapInstanceStream = File.OpenRead(mapInstancePath);
        await using var monsterSpawnsStream = File.OpenRead(monsterSpawnsPath);
        await using var merchantSpawnsStream = File.OpenRead(merchantSpawnsPath);
        await using var reactorsStream = File.OpenRead(reactorsPath);

        var mapInstanceSchema = await JsonSerializer.DeserializeAsync<MapInstanceSchema>(mapInstanceStream, JsonSerializerOptions);

        var monsterSpawnSchemas =
            await JsonSerializer.DeserializeAsync<List<MonsterSpawnSchema>>(monsterSpawnsStream, JsonSerializerOptions);

        var merchantSpawnSchemas =
            await JsonSerializer.DeserializeAsync<List<MerchantSpawnSchema>>(merchantSpawnsStream, JsonSerializerOptions);

        var reactorsSchemas = await JsonSerializer.DeserializeAsync<List<ReactorTileSchema>>(reactorsStream, JsonSerializerOptions);

        if (mapInstanceSchema == null)
            return null;

        var mapInstance = Mapper.Map<MapInstance>(mapInstanceSchema);
        var monsterSpawns = Mapper.MapMany<MonsterSpawn>(monsterSpawnSchemas!);

        foreach (var reactorSchema in reactorsSchemas!)
        {
            var reactor = ReactorTileFactory.Create(
                mapInstance,
                reactorSchema.Source,
                reactorSchema.ShouldBlockPathfinding,
                reactorSchema.ScriptKeys,
                reactorSchema.ScriptVars);

            mapInstance.SimpleAdd(reactor);
        }

        foreach (var monsterSpawn in monsterSpawns)
            mapInstance.AddSpawner(monsterSpawn);

        foreach (var merchantSpawn in merchantSpawnSchemas!)
        {
            var merchant = MerchantFactory.Create(
                merchantSpawn.MerchantTemplateKey,
                mapInstance,
                merchantSpawn.SpawnPoint,
                merchantSpawn.ExtraScriptKeys);

            merchant.Direction = merchantSpawn.Direction;
            mapInstance.SimpleAdd(merchant);
        }

        mapInstance.Pathfinder = PathfindingService;
        PathfindingService.RegisterGrid(mapInstance);

        return mapInstance;
    }
}