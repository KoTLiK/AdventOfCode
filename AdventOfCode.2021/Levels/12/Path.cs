namespace AdventOfCode.Levels._12;

public sealed record Path(string From, string To)
{
    public override int GetHashCode() => HashCode.Combine(From, To);

    public bool Equals(Path? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return From == other.From && To == other.To;
    }

    public override string ToString() => string.Join(',', From, To);
}