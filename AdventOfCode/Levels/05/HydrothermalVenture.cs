namespace AdventOfCode.Levels._05;

public class HydrothermalVenture : ALevel<int>
{
    public HydrothermalVenture(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
        => ReadLine(reader)
            .CreateLines()
            .Where(l => Setup.Round == 2 || l.IsHorizontalOrVertical)
            .SelectMany(l => l.Points())
            .GroupBy(p => p)
            .Select(g => g.Count())
            .Count(c => c > 1);
}