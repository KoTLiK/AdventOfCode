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
            Folding(folds.First(), dots);
            return dots.Distinct().Count().ToString();
        }

        foreach (var fold in folds)
        {
            Folding(fold, dots);
            dots = dots.Distinct().ToList();
        }

        Print(dots);
        /*

         #  # #    ###  #  # ###   ##  #### ###
         #  # #    #  # #  # #  # #  # #    #  #
         #### #    ###  #  # ###  #    ###  #  #
         #  # #    #  # #  # #  # # ## #    ###
         #  # #    #  # #  # #  # #  # #    # #
         #  # #### ###   ##  ###   ### #    #  #
 
         */

        return dots.Count.ToString();
    }

    private static void Print(IEnumerable<Dot> dots)
    {
        foreach (var group in dots.GroupBy(d => d.Row).OrderBy(g => g.Key))
        {
            var row = group.Select(d => d.Column).ToHashSet();
            for (var i = 0; i <= row.Max(); i++)
            {
                Console.Write(row.Contains(i) ? '#' : ' ');
            }

            Console.WriteLine();
        }
    }

    private static void Folding(Fold fold, IReadOnlyCollection<Dot> dots)
    {
        if (fold.Axis == Fold.Type.Row)
        {
            if (dots.Any(d => d.Row == fold.Position))
            {
                throw new InvalidOperationException();
            }

            foreach (var dot in dots.Where(d => d.Row > fold.Position))
            {
                dot.Row = 2 * fold.Position - dot.Row;
            }
        }
        else
        {
            if (dots.Any(d => d.Column == fold.Position))
            {
                throw new InvalidOperationException();
            }

            foreach (var dot in dots.Where(d => d.Column > fold.Position))
            {
                dot.Column = 2 * fold.Position - dot.Column;
            }
        }
    }
}