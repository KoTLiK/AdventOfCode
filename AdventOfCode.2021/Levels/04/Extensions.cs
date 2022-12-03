namespace AdventOfCode.Levels._04;

public static class Extensions
{
    public static IEnumerable<Board> CreateBoards(this IEnumerable<string> source)
    {
        BoardBuilder? builder = null;
        foreach (var line in source)
        {
            if (line == string.Empty)
            {
                if (builder is not null)
                {
                    yield return builder.Build();
                }

                builder = new BoardBuilder();
                continue;
            }

            builder ??= new BoardBuilder();
            builder.AddRow(line);
        }

        if (builder is not null)
        {
            yield return builder.Build();
        }
    }
}