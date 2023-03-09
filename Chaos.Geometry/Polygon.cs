﻿using System.Collections;
using System.Text.Json.Serialization;
using Chaos.Geometry.Abstractions;
using Chaos.Geometry.JsonConverters;

namespace Chaos.Geometry;

[JsonConverter(typeof(PolygonConverter))]
public sealed class Polygon : IPolygon, IEquatable<IPolygon>
{
    /// <inheritdoc />
    public IReadOnlyList<IPoint> Vertices { get; init; }

    public Polygon(IEnumerable<IPoint> vertices) => Vertices = vertices.ToList();

    public Polygon() => Vertices = new List<IPoint>();

    public bool Equals(IPolygon? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Vertices.Equals(other.Vertices);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return Equals((IPolygon)obj);
    }

    public IEnumerator<IPoint> GetEnumerator() => Vertices.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode() => Vertices.GetHashCode();
}