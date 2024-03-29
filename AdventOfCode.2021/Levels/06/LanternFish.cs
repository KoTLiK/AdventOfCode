using AdventOfCode.Levels._05;

namespace AdventOfCode.Levels._06;

public class LanternFish : ALevel<decimal>
{
    public LanternFish(IResultCollector<decimal> resultCollector)
        : base(resultCollector)
    {
    }

    protected override decimal Run(StreamReader reader)
    {
        var fishCounter = 0.To(8).ToDictionary(k => k, _ => 0m);

        var initialFishes = ReadLine(reader)
            .SelectMany(line => line.Split(','))
            .Select(int.Parse);
        foreach (var fish in initialFishes)
        {
            fishCounter[fish]++;
        }

        var daysLimit = IsFirstRound() ? 80 : 256;
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