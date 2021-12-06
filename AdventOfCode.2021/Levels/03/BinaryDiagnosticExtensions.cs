namespace AdventOfCode.Levels._03;

public static class BinaryDiagnosticExtensions
{
    public static int BitOccurence(this IEnumerable<string> source, int bitPosition)
        => source.Where(s => bitPosition < s.Length)
            .Select(s => s[bitPosition])
            .Aggregate(0, (current, c) => current + (c is '1' ? +1 : -1));

    public static int ConvertMostCommon(this IEnumerable<int> source)
        => source.ConvertBits(MostCommon);

    public static int ConvertLeastCommon(this IEnumerable<int> source)
        => source.ConvertBits(LeastCommon);

    public static char MostCommon(int s) => s >= 0 ? '1' : '0';

    public static char LeastCommon(int s) => s < 0 ? '1' : '0';

    public static int ConvertBits(this IEnumerable<int> source, Func<int, char> selector)
        => Convert.ToInt32(string.Join("", source.Select(selector).ToArray()), 2);
}