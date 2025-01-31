﻿using System.Text.Json.Serialization;
using Chaos.Geometry.Abstractions;
using Chaos.Geometry.Definitions;
using Chaos.Geometry.JsonConverters;

namespace Chaos.Geometry;

/// <inheritdoc cref="ILocation" />
[JsonConverter(typeof(LocationConverter))]
public readonly struct Location : ILocation, IEquatable<ILocation>
{
    /// <inheritdoc />
    public string Map { get; init; }
    /// <inheritdoc />
    public int X { get; init; }
    /// <inheritdoc />
    public int Y { get; init; }

    /// <summary>
    ///     Compares two locations
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Location left, ILocation right) => left.Equals(right);

    /// <summary>
    ///     Compares two locations
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Location left, ILocation right) => !left.Equals(right);

    /// <summary>
    ///     Creates a new location
    /// </summary>
    /// <param name="map"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Location(string map, int x, int y)
    {
        X = x;
        Y = y;
        Map = map;
    }

    /// <summary>
    ///     Deconstructs a location
    /// </summary>
    /// <param name="map"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Deconstruct(out string map, out int x, out int y)
    {
        map = Map;
        x = X;
        y = Y;
    }

    /// <inheritdoc />
    public bool Equals(ILocation? other) => other is not null
                                            && (X == other.X)
                                            && (Y == other.Y)
                                            && (Map == other.Map);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ILocation other && Equals(other);

    /// <summary>
    ///     Creates a new <see cref="Chaos.Geometry.Location" /> from an existing <see cref="Chaos.Geometry.Abstractions.ILocation" />
    /// </summary>
    /// <param name="location">An implementation of ILocation</param>
    /// <returns></returns>
    public static Location From(ILocation location)
    {
        if (location is Location loc)
            return loc;

        return new Location(location.Map, location.X, location.Y);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y, Map);

    /// <inheritdoc />
    public override string ToString() => ILocation.ToString(this);

    /// <summary>
    ///     Tries to parse a location from a string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public static bool TryParse(string str, out Location location)
    {
        location = new Location();
        var match = RegexCache.LOCATION_REGEX.Match(str);

        if (!match.Success)
            return false;

        if (!ushort.TryParse(match.Groups[2].Value, out var x))
            return false;

        if (!ushort.TryParse(match.Groups[3].Value, out var y))
            return false;

        location = new Location(match.Groups[1].Value, x, y);

        return true;
    }
}