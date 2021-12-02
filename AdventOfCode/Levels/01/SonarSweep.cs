using System.Text;

namespace AdventOfCode.Levels._01;

public class SonarSweep : ALevel<int>
{
    public SonarSweep(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override Task Run()
    {
        using var fileStream = File.Open(FileName, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(fileStream, Encoding.UTF8);

        int? previousValue = null;
        var parsedLines = ReadLine(reader).Select(int.Parse);

        var roundModifications = Setup!.Round == 1
            ? parsedLines
            : parsedLines.SlidingWindowOfSums(3);

        var value = roundModifications.IncreaseCheck(previousValue).Count(check => check == true);

        return Result(value);
    }
}