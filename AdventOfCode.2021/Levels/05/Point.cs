using System.Diagnostics;

namespace AdventOfCode.Levels._05;

[DebuggerDisplay("({X},{Y})")]
public sealed record Point(int X, int Y)
{
    public Point(string rawX, string rawY)
        : this(int.Parse(rawX), int.Parse(rawY))
    {
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public bool Equals(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public bool IsHorizontal(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X != other.X && Y == other.Y;
    }

    public bool IsVertical(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y != other.Y;
    }
}