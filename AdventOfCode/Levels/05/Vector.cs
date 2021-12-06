using System.Diagnostics;

namespace AdventOfCode.Levels._05;

[DebuggerDisplay("[({Start.X},{Start.Y}) -> ({End.X},{End.Y}): {IsHorizontalOrVertical}]")]
public sealed record Vector(Point Start, Point End)
{
    public bool IsHorizontalOrVertical { get; }
        = Start.IsHorizontal(End) || Start.IsVertical(End);

    public IEnumerable<Point> Points()
    {
        var x = Start.X.To(End.X).ToList();
        var y = Start.Y.To(End.Y).ToList();

        if (x.Count == 1)
        {
            foreach (var i in y)
            {
                yield return new Point(x.First(), i);
            }
        }
        else if (y.Count == 1)
        {
            foreach (var i in x)
            {
                yield return new Point(i, y.First());
            }
        }
        else if (x.Count == y.Count)
        {
            for (var i = 0; i < x.Count; i++)
            {
                yield return new Point(x[i], y[i]);
            }
        }
    }
}