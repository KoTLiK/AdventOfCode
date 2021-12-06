using System.Diagnostics;

namespace AdventOfCode.Levels._06;

public class LanternFish : ALevel<long>
{
    public LanternFish(IResultCollector<long> resultCollector) : base(resultCollector)
    {
    }

    protected override long Run(StreamReader reader)
    {
        var initialFishes = ReadLine(reader)
            .SelectMany(line => line.Split(','))
            .Select(int.Parse)
            .ToList();

        var timing = new List<TimeSpan>(256);

        var fishes = new LinkedList<int>(initialFishes);
        var daysLimit = Setup.Round == 1 ? 80 : 256;
        var daysPassed = 0;
        while (daysPassed++ < daysLimit)
        {
            var timer = new Stopwatch();
            timer.Start();

            var counter = 0;
            var limit = fishes.Count;
            var fish = fishes.First;
            while (counter++ < limit && fish is not null)
            {
                if (fish.ValueRef == 0)
                {
                    fishes.AddLast(8);
                    fish.ValueRef = 6;
                }
                else
                {
                    fish.ValueRef--;
                }
                fish = fish.Next;
            }

            timer.Stop();
            var time = timer.Elapsed;
            timing.Add(time);

            var avgTime = new TimeSpan((long) timing.Average(t => t.Ticks));
            Console.WriteLine($"Day: {daysPassed}");
            Console.WriteLine($"T1: {time:g} \tA: {avgTime:g} \tCount: {fishes.Count,12}");
        }

        return fishes.Count;
    }
}