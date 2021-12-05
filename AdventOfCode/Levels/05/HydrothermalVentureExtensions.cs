namespace AdventOfCode.Levels._05;

public static class HydrothermalVentureExtensions
{
    public static IEnumerable<Vector> CreateVectors(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var rawPoints = line.Split(" -> ");
            var start = CreatePoint(rawPoints[0]);
            var end = CreatePoint(rawPoints[1]);

            yield return new Vector(start, end);
        }

        static Point CreatePoint(string rawPoint)
        {
            var raw = rawPoint.Split(',');
            return new Point(raw[0], raw[1]);
        }
    }

    public static IEnumerable<int> Generate(this int from, int to)
    {
        var x = from;
        if (x <= to)
        {
            while (x <= to)
            {
                yield return x++;
            }
        }
        else
        {
            while (x >= to)
            {
                yield return x--;
            }
        }
    }
}