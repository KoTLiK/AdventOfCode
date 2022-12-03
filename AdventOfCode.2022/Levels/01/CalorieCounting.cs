namespace AdventOfCode.Levels._01;

public class CalorieCounting : ALevel<int>
{
    public CalorieCounting(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var elfs = ReadLine(reader)
            .Select(ParseLine)
            .CreateElf()
            .ToList();

        return 0;
    }

    private static int? ParseLine(string line)
        => int.TryParse(line, out var result) ? result : null;
}