using System.Diagnostics;

namespace AdventOfCode.Levels._13;

[DebuggerDisplay("{FoldAlong}={Position}")]
public sealed record Fold(string FoldAlong, int Position)
{
    public enum Type
    {
        Row,
        Column
    }

    public Fold(string foldAlong, string position)
        : this(foldAlong, int.Parse(position))
    {
    }

    public Type Axis => FoldAlong == "y" ? Type.Row : Type.Column;

    public bool Equals(Fold? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FoldAlong == other.FoldAlong && Position == other.Position;
    }

    public override int GetHashCode() => HashCode.Combine(FoldAlong, Position);
}