namespace AdventOfCode.Levels._07;

public class TheTreacheryOfWhales : ALevel<int>
{
    public TheTreacheryOfWhales(IResultCollector<int> resultCollector) : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var values = ReadLine(reader)
            .SelectMany(line => line.Split(","))
            .Select(int.Parse)
            .OrderBy(i => i)
            .ToArray();

        var median = Median(values);

        return Setup switch
        {
            {Round: 1} => FirstRound(values, median),
            {Round: 2} => SecondRound(values, median),
            _ => throw new InvalidOperationException("No other option available")
        };
    }

    private static int SecondRound(int[] values, int distance)
    {
        var value = CalculatedLinearDistance(ref values, distance);
        var upperIncrement = FindMinimum(ref values, value, distance, 1);
        var lowerIncrement = FindMinimum(ref values, value, distance, -1);
        return new[] {value, upperIncrement, lowerIncrement}.Min();
    }

    private static int CalculatedLinearDistance(ref int[] values, int distance)
        => values.LinearDistanceFrom(distance)
            .Select(v => Enumerable.Range(1, v.Distance).Sum())
            .Sum();

    private static int FindMinimum(ref int[] values, int value, int distance, int increment)
    {
        var newDistance = distance + increment;
        var newValue = CalculatedLinearDistance(ref values, newDistance);

        return value <= newValue ? value : FindMinimum(ref values, newValue, newDistance, increment);
    }

    private static int FirstRound(IEnumerable<int> values, int median)
    {
        var data = new[] {median - 1, median, median + 1};

        var minimum = values
            .LinearDistanceFrom(data)
            .GroupBy(d => d.Position)
            .Select(g => g.Sum(d => d.Distance))
            .Min();

        return minimum;
    }

    private static int Median(in int[] values)
    {
        var count = values.Length;
        var range = ((count >> 1) + (count & 0x1))..((count >> 1) + 2);
        var data = values[range];
        return data.Length > 1 ? data.Sum() >> 1 : data.First();
    }
}