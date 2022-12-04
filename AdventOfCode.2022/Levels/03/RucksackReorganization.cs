namespace AdventOfCode.Levels._03;

public class RucksackReorganization : ALevel<int>
{
    public RucksackReorganization(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var rucksacks = ReadLine(reader)
            .Select(line => new Rucksack(line))
            .ToList(); // TODO

        if (IsFirstRound())
        {
            return rucksacks
                .Select(r => Intersection(r.FirstCompartment, r.SecondCompartment))
                .Select(i => i.Select(c => c.Priority()).ToArray())
                .SelectMany(i => i)
                .Sum();
        }

        return rucksacks
            .GroupByAmount(3)
            .Select(Intersection)
            .Select(i => i.Select(c => c.Priority()).ToArray())
            .SelectMany(i => i)
            .Sum();
    }

    private static IEnumerable<char> Intersection(string first, string second)
        => first.Distinct().Intersect(second.Distinct());

    private static HashSet<char> Intersection(IList<Rucksack> rucksacks)
        => rucksacks.Skip(1)
            .Aggregate(
                new HashSet<char>(rucksacks.First().Items),
                (h, r) => { h.IntersectWith(r.Items); return h; });
}