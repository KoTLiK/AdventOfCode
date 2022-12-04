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
        var result = IsFirstRound()
            ? PowerConsumption(lines)
            : LifeSupportRating(lines);

        return result;
    }

    private static int LifeSupportRating(IReadOnlyList<string> lines)
    {
        var length = lines[0].Length;
        var oxygen = OxygenGeneratorRating(lines, length);
        var co2 = Co2ScrubberRating(lines, length);

        return oxygen * co2;
    }

    private static int OxygenGeneratorRating(IEnumerable<string> lines, int length)
        => RatingCalculator(lines, Extensions.MostCommon, length, 0);

    private static int Co2ScrubberRating(IEnumerable<string> lines, int length)
        => RatingCalculator(lines, Extensions.LeastCommon, length, 0);

    private static int RatingCalculator(IEnumerable<string> lines, Func<int, char> selector, int length, int position)
    {
        if (position >= length)
        {
            throw new ArgumentException("Unable to continue", nameof(position));
        }

        var common = selector.Invoke(lines.BitOccurence(position));
        var filtered = lines.Where(line => line[position] == common);

        return filtered.Count() == 1
            ? Convert.ToInt32(filtered.First(), 2)
            : RatingCalculator(filtered, selector, length, position + 1);
    }

    private static int PowerConsumption(IReadOnlyList<string> lines)
    {
        var length = lines[0].Length;
        var counters = new List<int>(length);
        for (var i = 0; i < length; i++)
        {
            counters.Add(lines.BitOccurence(i));
        }

        var mostCommon = counters.ConvertMostCommon();
        var leastCommon = counters.ConvertLeastCommon();

        return mostCommon * leastCommon;
    }
}