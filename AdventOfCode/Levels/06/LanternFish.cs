namespace AdventOfCode.Levels._06;

public class LanternFish : ALevel<long>
{
    public LanternFish(IResultCollector<long> resultCollector) : base(resultCollector)
    {
    }

    protected override long Run(StreamReader reader)
    {
        var initialFishes = ReadLine(reader)
            .SelectMany(line => line.Split(','))
            .Select(s => new Fish(int.Parse(s)))
            .ToList();

        var limit = Setup.Round == 1 ? 80 : 256;
        var daysPassed = 0;
        var query = initialFishes.AsEnumerable();
        while (daysPassed++ <= limit)
        {
            query = query.SpawnFish().Select(fish => --fish);
        }

        return query.LongCount();
    }
}