namespace AdventOfCode.Levels._05;

public class HydrothermalVenture : ALevel<int>
{
    public HydrothermalVenture(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
        => ReadLine(reader)
            .CreateVectors()
            .Where(v => Setup.Round == 2 || v.IsHorizontalOrVertical)
            .SelectMany(v => v.Points())
            .GroupBy(p => p)
            .Select(g => (g.Key, Count: g.Count()))
            .Count(g => g.Count > 1);
}