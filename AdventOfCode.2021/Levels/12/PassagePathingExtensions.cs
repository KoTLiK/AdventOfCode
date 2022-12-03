namespace AdventOfCode.Levels._12;

public static class PassagePathingExtensions
{
    public static IEnumerable<Path> ConvertToAllPaths(this IEnumerable<Path> source)
    {
        static Path? Switch(Path p) =>
            (p.From, p.To) switch
            {
                ("start", _) => p,
                (_, "start") => new Path(p.To, p.From),
                ("end", _) => new Path(p.To, p.From),
                (_, "end") => p,
                (_, _) => null
            };

        foreach (var p in source)
        {
            var path = Switch(p);
            if (path is null)
            {
                yield return p;
                yield return new Path(p.To, p.From);
            }
            else
            {
                yield return path;
            }
        }
    }
}