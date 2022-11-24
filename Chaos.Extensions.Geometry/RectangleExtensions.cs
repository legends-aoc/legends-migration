using Chaos.Geometry;
using Chaos.Geometry.Abstractions;

namespace Chaos.Extensions.Geometry;

/// <summary>
///     Provides extension methods for <see cref="IRectangle"/>
/// </summary>
public static class RectangleExtensions
{
    /// <summary>
    ///     Determines whether the specified <see cref="IRectangle"/> contains another <see cref="IRectangle"/>
    /// </summary>
    /// <param name="rect">The possibly outer rectangle</param>
    /// <param name="other">The possible inner rectangle</param>
    /// <returns><c>true</c> if <paramref name="rect"/> fully encompasses <paramref name="other"/>, otherwise <c>false</c></returns>
    public static bool Contains(this IRectangle rect, IRectangle other)
    {
        ArgumentNullException.ThrowIfNull(rect);

        ArgumentNullException.ThrowIfNull(other);

        return (rect.Bottom >= other.Bottom) && (rect.Left >= other.Left) && (rect.Right <= other.Right) && (rect.Top <= other.Top);
    }

    /// <summary>
    ///     Determines whether the specified <see cref="IRectangle"/> contains an <see cref="IPoint"/>
    /// </summary>
    /// <param name="rect">The rectangle to check</param>
    /// <param name="point">The point to check</param>
    /// <returns><c>true</c> if <paramref name="point"/> is inside of the <paramref name="rect"/>, otherwise <c>false</c></returns>
    public static bool Contains(this IRectangle rect, IPoint point)
    {
        ArgumentNullException.ThrowIfNull(rect);

        ArgumentNullException.ThrowIfNull(point);

        return (rect.Left <= point.X) && (rect.Right >= point.X) && (rect.Top <= point.Y) && (rect.Bottom >= point.Y);
    }

    /// <summary>
    ///     Determines whether the specified <see cref="IRectangle"/> intersects another <see cref="IRectangle"/>
    /// </summary>
    /// <param name="rect">A rectangle</param>
    /// <param name="other">Another rectangle</param>
    /// <returns><c>true</c> if the rectangles intersect at any point or if either rect fully contains the other, otherwise <c>false</c></returns>
    public static bool Intersects(this IRectangle rect, IRectangle other)
    {
        ArgumentNullException.ThrowIfNull(rect);

        ArgumentNullException.ThrowIfNull(other);

        return !((rect.Bottom >= other.Top) || (rect.Left >= other.Right) || (rect.Right <= other.Left) || (rect.Top <= other.Bottom));
    }

    /// <summary>
    ///     Lazily generates all points inside of the specified <see cref="IRectangle"/>
    /// </summary>
    /// <param name="rect">The rectangle togenerate points for</param>
    public static IEnumerable<Point> Points(this IRectangle rect)
    {
        ArgumentNullException.ThrowIfNull(rect);

        for (var x = rect.Left; x <= rect.Right; x++)
            for (var y = rect.Top; y <= rect.Bottom; y++)
                yield return new Point(x, y);
    }

    /// <summary>
    ///     Generates a random point inside the <see cref="IRectangle"/>
    /// </summary>
    /// <param name="rect">The rect to use as bounds</param>
    public static Point RandomPoint(this IRectangle rect) => new(
        rect.Left + Random.Shared.Next(rect.Width),
        rect.Top + Random.Shared.Next(rect.Height));
}