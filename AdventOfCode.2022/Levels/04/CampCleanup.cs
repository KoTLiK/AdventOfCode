namespace AdventOfCode.Levels._04;

public class CampCleanup : ALevel<int>
{
    public CampCleanup(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var intervals = ReadLine(reader).Select(ToIntervals);

        return IsFirstRound()
            ? intervals.Count(pair => pair.Left.Overlaps(pair.Right) || pair.Right.Overlaps(pair.Left))
            : intervals.Count(pair => !pair.Left.IsBefore(pair.Right) && !pair.Right.IsBefore(pair.Left));
    }

    private static (Interval Left, Interval Right) ToIntervals(string value)
    {
        var parts = value.Split(',');
        return (Left: CreateInterval(parts[0]), Right: CreateInterval(parts[1]));
    }

    private static Interval CreateInterval(string value)
    {
        var parts = value.Split('-');
        return new Interval(parts[0], parts[1]);
    }
}