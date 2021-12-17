using System.Diagnostics;

namespace AdventOfCode.Levels._13;

[DebuggerDisplay("({Column},{Row})")]
public sealed record Dot(int Column, int Row)
{
    public Dot(string rawX, string rawY)
        : this(int.Parse(rawX), int.Parse(rawY))
    {
    }

    public override int GetHashCode() => HashCode.Combine(Column, Row);

    public bool Equals(Dot? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Column == other.Column && Row == other.Row;
    }
}