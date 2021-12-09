using System.Diagnostics;

namespace AdventOfCode.Levels._09;

[DebuggerDisplay("[{Y},{X}] = {Value} (Seen: {Seen}, IsLow: {IsLow})")]
public sealed record Location(
    int Y,
    int X,
    int Value,
    bool Seen = false,
    bool IsLow = false)
{
    public int Row => Y;
    public int Column => X;

    public Location(int y, int x, string rawValue)
        : this(y, x, int.Parse(rawValue))
    {
    }

    public Location(int y, int x, char rawValue)
        : this(y, x, rawValue.ToString())
    {
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Value);

    public bool Equals(Location? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Value == other.Value;
    }

    public bool EqualByValue(Location? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return Value == other.Value;
    }
}