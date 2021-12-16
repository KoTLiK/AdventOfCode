namespace AdventOfCode.Levels._12;

public class PassagePathing : ALevel<int>
{
    public PassagePathing(IResultCollector<int> resultCollector) : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var data = ReadLine(reader)
            .Select(line => line.Split("-"))
            .Select(p => new Path(p[0], p[1]))
            .ConvertToAllPaths()
            .Aggregate(new Dictionary<string, HashSet<Path>>(), (paths, p) =>
            {
                if (!paths.TryAdd(p.From, new HashSet<Path> {p}))
                {
                    paths[p.From].Add(p);
                }

                return paths;
            });

        var routes = Foo(data, null, "start").ToList();

        return routes.Count;
    }

    private IEnumerable<Route> Foo(IReadOnlyDictionary<string, HashSet<Path>> data, Route? current, string? key)
    {
        current ??= new Route();

        if (key == "end")
        {
            yield return current.Finish(key);
        }

        data.TryGetValue(key ?? string.Empty, out var routes);
        var query = from item in routes ?? new HashSet<Path>()
            let nextRoute = current.Copy()
            let nextKey = Setup.Round == 1
                ? nextRoute.Step(item)
                : nextRoute.StepSecond(item)
            where nextKey is not null
            from route in Foo(data, nextRoute, nextKey)
            select route;
        foreach (var route in query)
        {
            yield return route;
        }

        // foreach (var item in routes ?? new HashSet<Path>())
        // {
        //     var nextRoute = current.Copy();
        //     var nextKey = Setup.Round == 1
        //         ? nextRoute.Step(item)
        //         : nextRoute.StepSecond(item);
        //     if (nextKey is null) continue;
        //     foreach (var route in Foo(data, nextRoute, nextKey))
        //     {
        //         yield return route;
        //     }
        // }
    }
}