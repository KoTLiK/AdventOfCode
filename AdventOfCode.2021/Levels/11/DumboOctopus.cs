namespace AdventOfCode.Levels._11;

public class DumboOctopus : ALevel<int>
{
    public DumboOctopus(IResultCollector<int> resultCollector) : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var data = ReadLine(reader)
            .SelectMany((line, row) => line
                .Select((value, column) => new Point(row, column, value))
                .ToList())
            .ToHashSet();

        var flashCounter = 0;
        var daysLimit = IsFirstRound() ? 100 : int.MaxValue;
        var daysPassed = 0;
        while (daysPassed++ < daysLimit)
        {
            var updated = data.Select(p => p with {Value = p.Value + 1}).ToHashSet();
            while (updated.Any(p => p.Value > 9 && !p.Flashed))
            {
                var flashing = updated.Where(p => p.Value > 9 && !p.Flashed).ToHashSet();
                foreach (var point in flashing)
                {
                    var points = new[]
                    {
                        point.Up(updated),
                        point.Up(updated)?.Left(updated),
                        point.Up(updated)?.Right(updated),
                        point.Down(updated),
                        point.Down(updated)?.Left(updated),
                        point.Down(updated)?.Right(updated),
                        point.Left(updated),
                        point.Right(updated)
                    };

                    updated.Update(point with {Flashed = true});
                    foreach (var p in points.Where(item => item is not null))
                    {
                        updated.Update(p! with {Value = p.Value + 1});
                    }
                }
            }

            foreach (var point in updated)
            {
                if (point.Flashed)
                {
                    flashCounter++;
                    data.Update(point with {Value = 0, Flashed = false});
                }
                else
                {
                    data.Update(point);
                }
            }

            if (data.All(p => p.Value == 0))
            {
                return daysPassed;
            }
        }

        return flashCounter;
    }
}