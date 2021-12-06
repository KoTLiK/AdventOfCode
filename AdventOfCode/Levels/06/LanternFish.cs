using System.Diagnostics;

namespace AdventOfCode.Levels._06;

public class LanternFish : ALevel<decimal>
{
    public LanternFish(IResultCollector<decimal> resultCollector) : base(resultCollector)
    {
    }

    protected override decimal Run(StreamReader reader)
    {
        var fishCounter = new Dictionary<int, decimal>(8);
        for (var i = 0; i <= 8; i++)
        {
            fishCounter[i] = 0;
        }

        var initialFishes = ReadLine(reader)
            .SelectMany(line => line.Split(','))
            .Select(int.Parse);
        foreach (var fish in initialFishes)
        {
            fishCounter[fish]++;
        }

        var daysLimit = Setup.Round == 1 ? 80 : 256;
        var daysPassed = 0;
        while (daysPassed++ < daysLimit)
        {
            var reset = MoveCounters(fishCounter);
            fishCounter[6] += reset;
            fishCounter[8] = reset;
        }

        return fishCounter.Sum(p => p.Value);
    }

    private static decimal MoveCounters(IDictionary<int, decimal> fishCounter)
    {
        var reset = 0m;
        for (var i = 0; i <= 8; i++)
        {
            if (i == 0)
            {
                reset = fishCounter[i];
            }
            else
            {
                fishCounter[i - 1] = fishCounter[i];
            }
        }

        return reset;
    }
}