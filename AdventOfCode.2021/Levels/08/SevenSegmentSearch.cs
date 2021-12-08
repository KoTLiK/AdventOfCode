namespace AdventOfCode.Levels._08;

public class SevenSegmentSearch : ALevel<int>
{
    public SevenSegmentSearch(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var entries = ReadLine(reader).CreateEntries().ToList();

        return Setup switch
        {
            {Round: 1} => entries.Select(e => e.NumberOfEasyDigits()).Sum(),
            {Round: 2} => entries.Select(e =>
            {
                var origin = e.DecodedOutput();
                var foreign = e.DecodedOutput2();
                if (origin != foreign)
                {
                    throw new InvalidOperationException();
                }

                return origin;
            }).Sum(),
            _ => throw new InvalidOperationException()
        };
    }
}