namespace AdventOfCode.Levels._01;

public class SonarSweep : ALevel<int>
{
    public SonarSweep(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        int? previousValue = null;
        var parsedLines = ReadLine(reader).Select(int.Parse);

        var roundModifications = Setup!.Round == 1
            ? parsedLines
            : parsedLines.SlidingWindowOfSums(3);

        return roundModifications
            .IncreaseCheck(previousValue)
            .Count(check => check == true);
    }
}