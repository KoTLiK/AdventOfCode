namespace AdventOfCode.Levels._09;

public class SmokeBasin : ALevel<int>
{
    public SmokeBasin(IResultCollector<int> resultCollector) : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var data = ReadLine(reader)
            .Select((line, row) => line
                .Select((c, column) => new Location(row, column, c))
                .ToList())
            .ToList();

        var location = GetUnSeen(data);
        Check(data, location);

        var lowLocations = data
            .SelectMany(row => row)
            .Where(p => p.IsLow)
            .ToList();

        if (Setup is {Round: 1})
        {
            return lowLocations.Sum(p => p.Value + 1);
        }

        var basins = lowLocations
            .Select(l => GetBasin(data, l))
            .ToList();

        return basins
            .Select(b => b.Count)
            .OrderByDescending(v => v)
            .Take(3)
            .Aggregate(1, (current, count) => current * count);
    }

    private static HashSet<Location> GetBasin(IReadOnlyList<List<Location>> data, Location low)
    {
        var basin = new HashSet<Location> {low};
        Gather(data, low, ref basin);

        return basin;
    }

    private static void Gather(IReadOnlyList<List<Location>> data, Location low, ref HashSet<Location> basin)
    {
        var locations = new[]
            {
                low.Up(data),
                low.Right(data),
                low.Down(data),
                low.Left(data)
            }
            .Where(p => p is not null)
            .Where(p => p!.Value < 9)
            .ToArray();

        var gather = new List<Location>();
        foreach (var location in locations)
        {
            if (!basin.Contains(location!))
            {
                gather.Add(location!);
                basin.Add(location!);
            }
        }

        foreach (var location in gather)
        {
            Gather(data, location, ref basin);
        }
    }

    private static void Check(IReadOnlyList<List<Location>> data, Location? current)
    {
        while (true)
        {
            if (current is null || current.Seen)
            {
                if (GetUnSeen(data) is not Location unSeen)
                {
                    return;
                }

                current = unSeen;
            }

            current = current.SetAsSeen(data);

            var locations = new[]
                {
                    current,
                    current.Up(data),
                    current.Right(data),
                    current.Down(data),
                    current.Left(data)
                }
                .Where(p => p is not null)
                .OrderBy(p => p!.Value)
                .Take(2)
                .ToArray();

            var location = locations[0]!.EqualByValue(locations[1])
                ? locations.FirstOrDefault(p => !p!.Seen)
                : locations.MinBy(p => p!.Value);

            if (location?.Seen == true && location == current)
            {
                current = location.SetAsLow(data);
            }
        }
    }

    private static Location? GetUnSeen(IEnumerable<List<Location>> data)
        => data.SelectMany(row => row).FirstOrDefault(p => !p.Seen);
}