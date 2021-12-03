namespace AdventOfCode.Levels._03;

public class BinaryDiagnostic : ALevel<int>
{
    public BinaryDiagnostic(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var lines = ReadLine(reader).ToList();

        var counters = new List<int>(lines.First().Length);
        for (var i = 0; i < lines.First().Length; i++)
        {
            counters.Add(0);
        }

        foreach (var line in lines)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] is '1')
                {
                    counters[i]++;
                }
                else
                {
                    counters[i]--;
                }
            }
        }

        var mostCommon = counters
            .Select(counter => counter > 0 ? 1 : 0)
            .Aggregate(0, (current, bit) => (current << 1) + bit);

        var leastCommon = counters
            .Select(counter => counter > 0 ? 0 : 1)
            .Aggregate(0, (current, bit) => (current << 1) + bit);

        return mostCommon * leastCommon;
    }
}