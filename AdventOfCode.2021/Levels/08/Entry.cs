namespace AdventOfCode.Levels._08;

public record Entry(IEnumerable<string> Patterns, IEnumerable<string> Digits)
{
    public int NumberOfEasyDigits() => Digits.Count(d => d.DecodeSimpleDigit() is not null);

    public int DecodedOutput()
    {
        var signal = Patterns.DecodeDigits().OrderBy(p => p.Digit).ToList();
        var decodedDigits = Digits
            .Select(d => signal
                .Where(s => s.Pattern.Length == d.Length)
                .Single(s => !d.Except(s.Pattern).Any())
                .Digit)
            .ToList();

        return int.Parse(string.Join("", decodedDigits.ToArray()));
    }

    public int DecodedOutput2()
    {
        var signal = Patterns.DecodeDigits2().OrderBy(p => p.Digit).ToList();
        var decodedDigits = Digits
            .Select(d => signal
                .Where(s => s.Pattern.Length == d.Length)
                .Single(s => !d.Except(s.Pattern).Any())
                .Digit)
            .ToList();

        return int.Parse(string.Join("", decodedDigits.ToArray()));
    }
}