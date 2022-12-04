using System.Diagnostics;

namespace AdventOfCode.Levels._04;

[DebuggerDisplay("<{Start};{End}>")]
public readonly struct Interval
{
    public byte Start { get; }
    public byte End { get; }

    public Interval(byte start, byte end)
    {
        if (start > end)
        {
            throw new ArgumentOutOfRangeException($"Invalid interval values <{start};{end}>");
        }

        this.Start = start;
        this.End = end;
    }

    public Interval(string start, string end)
        : this(byte.Parse(start), byte.Parse(end))
    {
    }

    public bool Overlaps(Interval other)
        => this.Start <= other.Start && this.End >= other.End;

    public bool IsBefore(Interval other)
        => this.End < other.Start;
}