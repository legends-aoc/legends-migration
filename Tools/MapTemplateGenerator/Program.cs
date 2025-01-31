﻿//Vorlof provided a file with a bunch of map id / map dimension information
//this tool is to parse that file into a bunch of MapTemplate json files

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Chaos.Common.Collections.Synchronized;
using Chaos.Geometry;
using Chaos.Geometry.JsonConverters;
using Chaos.Schemas.Templates;

const string FILENAME = "Master_Maplist.txt";
const string DIRECTORY = "templates";
var regex = MyRegex();
var lines = await File.ReadAllLinesAsync(FILENAME);
var hashSet = new SynchronizedHashSet<short>();

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    PropertyNameCaseInsensitive = true,
    IgnoreReadOnlyProperties = true,
    IgnoreReadOnlyFields = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    AllowTrailingCommas = true
};

options.Converters.Add(new PointConverter());
options.Converters.Add(new JsonStringEnumConverter());

if (!Directory.Exists(DIRECTORY))
    Directory.CreateDirectory(DIRECTORY);

await Parallel.ForEachAsync(
    lines,
    new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
    ParseLineToFileAsync);

async ValueTask ParseLineToFileAsync(string line, CancellationToken _)
{
    Match? match;

    if ((match = regex.Match(line)).Success)
    {
        var mapId = match.Groups[1].Value;
        var mapIdNum = short.Parse(mapId);

        if (!hashSet.Add(mapIdNum))
            return;

        var width = match.Groups[3].Value;
        var widthNum = byte.Parse(width);
        var height = match.Groups[4].Value;
        var heightNum = byte.Parse(height);

        var path = Path.Combine(DIRECTORY, $"{mapId}.json");

        var template = new MapTemplateSchema
        {
            TemplateKey = mapIdNum.ToString(),
            Width = widthNum,
            Height = heightNum,
            WarpPoints = Array.Empty<Point>(),
            ScriptKeys = Array.Empty<string>()
        };

        await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        await JsonSerializer.SerializeAsync(stream, template, options);
    }
}

internal partial class Program
{
    [GeneratedRegex("""(\d+)\|(.+)\|(\d+)\|(\d+)""", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}