namespace AdventOfCode.Levels._13;

public class TransparentOrigami : ALevel<string>
{
    public TransparentOrigami(IResultCollector<string> resultCollector) : base(resultCollector)
    {
    }

    protected override string Run(StreamReader reader)
    {
        var lines = ReadLine(reader).ToList();
        var dots = lines
            .Where(l => l.Contains(','))
            .Select(l => l.Split(','))
            .Select(p => new Dot(p[0], p[1]))
            .ToList();
        var folds = lines
            .Where(l => l.StartsWith("fold along"))
            .Select(l => l.Split(' ').Last().Split('='))
            .Select(f => new Fold(f[0], f[1]))
            .ToList();

        
        if (Setup is {Round: 1})
        {
            return Folding(folds.First(), dots).Count.ToString();
        }

        dots = folds.Aggregate(dots, (current, fold) => Folding(fold, current));

        Print(dots);
        /*
         *     ABCDEFGHIJKLMNOPQRSTUVWXYZ
         *
         *     #  # #    ###  #  # ###   ##  #### ##
         *     #  # #    #  # #  # #  # #  # #    #
         *     #### #    ###  #  # ###  #    ###  #
         *     #  # #    #  # #  # #  # # ## #    ##
         *     #  # #    #  # #  # #  # #  # #    #
         *     #  # #### ###   ##  ###   ### #    #
         *
         *     TODO Don't know the last letter. Where I have a mistake? 
         */

        return dots.Count.ToString();
    }

    private static void Print(IEnumerable<Dot> dots)
    {
        foreach (var group in dots.GroupBy(d => d.Row).OrderBy(g => g.Key))
        {
            var row = group.Select(d => d.Column).OrderBy(v => v).ToHashSet();
            for (var i = 0; i < row.Max(); i++)
            {
                Console.Write(row.Contains(i) ? '#' : ' ');
            }

            Console.WriteLine();
        }
    }

    private static List<Dot> Folding(Fold fold, List<Dot> dots)
    {
        if (fold.Axis == Fold.Type.Row)
        {
            if (dots.Any(d => d.Row == fold.Position))
            {
                throw new InvalidOperationException();
            }

            var folded = dots.Where(d => d.Row > fold.Position)
                .Select(d => d with {Row = fold.Position - (d.Row - fold.Position)})
                .ToList();
            dots.AddRange(folded);
            return dots.Where(d => d.Row < fold.Position).Distinct().ToList();
        }
        else
        {
            if (dots.Any(d => d.Column == fold.Position))
            {
                throw new InvalidOperationException();
            }

            var folded = dots.Where(d => d.Column > fold.Position)
                .Select(d => d with {Column = fold.Position - (d.Column - fold.Position)})
                .ToList();
            dots.AddRange(folded);
            return dots.Where(d => d.Column < fold.Position).Distinct().ToList();
        }
    }
}