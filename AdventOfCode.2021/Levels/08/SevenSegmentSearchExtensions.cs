namespace AdventOfCode.Levels._08;

public static class SevenSegmentSearchExtensions
{
    private static IEnumerable<string> DigitSplit(string data)
        => data.Split(" ")
            .Where(signal => !string.IsNullOrWhiteSpace(signal))
            .Select(signal => signal.Trim())
            .ToList();

    public static IEnumerable<Entry> CreateEntries(this IEnumerable<string> source)
        => from line in source select line.Split("|") into data
            let patterns = DigitSplit(data[0])
            let digits = DigitSplit(data[1])
            select new Entry(patterns, digits);

    public static int? DecodeSimpleDigit(this string signal)
        => signal.Length switch
        {
            > 7 or < 2 => throw new InvalidOperationException(),
            7 => 8,
            3 => 7,
            4 => 4,
            2 => 1,
            _ => null
        };

    public static IEnumerable<(int Digit, string Pattern)> DecodeDigits(this IEnumerable<string> source)
    {
        var patterns = source.ToList();

        var _1 = patterns.Single(p => p.Length == 2);
        var _4 = patterns.Single(p => p.Length == 4);
        var _7 = patterns.Single(p => p.Length == 3);

        yield return (Digit: 1, Pattern: _1);
        yield return (Digit: 4, Pattern: _4);
        yield return (Digit: 7, Pattern: _7);
        yield return (Digit: 8, Pattern: patterns.Single(p => p.Length == 7));

        var five = patterns.Where(p => p.Length == 5).ToList();
        foreach (var pattern in patterns.Where(p => p.Length == 6))
        {
            if (!_4.Except(pattern).Any())
            {
                yield return (Digit: 9, Pattern: pattern);
                yield return (Digit: 2, Pattern: five.Single(p => p.Except(pattern).Any()));
            }
            else if (_1.Except(pattern).Any())
            {
                yield return (Digit: 6, Pattern: pattern);
                yield return (Digit: 5, Pattern: five.Single(p => !p.Except(pattern).Any()));
            }
            else
            {
                yield return (Digit: 0, Pattern: pattern);
            }
        }

        yield return (Digit: 3, Pattern: five.Single(p => !_7.Except(p).Any()));
    }
}