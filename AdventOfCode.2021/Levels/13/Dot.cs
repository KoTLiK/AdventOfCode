using System.Diagnostics;

namespace AdventOfCode.Levels._13;

[DebuggerDisplay("({Column},{Row})")]
public sealed class Dot
{
    public int Column { get; set; }
    public int Row { get; set; }

    public Dot(string x, string y)
    {
        Column = int.Parse(x);
        Row = int.Parse(y);
    }

    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is Dot other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(Column, Row);

    private bool Equals(Dot other)
        => Column == other.Column && Row == other.Row;
}