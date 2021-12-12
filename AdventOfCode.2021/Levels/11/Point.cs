namespace AdventOfCode.Levels._11;

public sealed record Point(int Y, int X, int Value, bool Flashed = false)
{
    public int Row => Y;
    public int Column => X;

    public Point(int y, int x, char rawValue)
        : this(y, x, int.Parse(rawValue.ToString()))
    {
    }

    public override int GetHashCode() => HashCode.Combine(Y, X);

    public bool Equals(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Y == other.Y && X == other.X;
    }

    public override string ToString()
    {
        var value = Flashed ? "#" : $"{Value,1}";
        return $"[{Y},{X}] {value}";
    }
}