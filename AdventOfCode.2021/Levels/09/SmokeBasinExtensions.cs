namespace AdventOfCode.Levels._09;

public static class SmokeBasinExtensions
{
    private static Location? Seek(int row, int column, IReadOnlyList<List<Location>> data)
    {
        if (row < 0 || row >= data.Count) return null;
        if (column < 0 || column >= data.Min(r => r.Count)) return null;
        return data[row][column];
    }

    public static Location? Up(this Location current, IReadOnlyList<List<Location>> data)
        => Seek(current.Row - 1, current.Column, data);

    public static Location? Down(this Location current, IReadOnlyList<List<Location>> data)
        => Seek(current.Row + 1, current.Column, data);

    public static Location? Left(this Location current, IReadOnlyList<List<Location>> data)
        => Seek(current.Row, current.Column - 1, data);

    public static Location? Right(this Location current, IReadOnlyList<List<Location>> data)
        => Seek(current.Row, current.Column + 1, data);

    public static Location SetAsLow(this Location current, IReadOnlyList<List<Location>> data)
        => data[current.Row][current.Column] = current with {IsLow = true};

    public static Location SetAsSeen(this Location current, IReadOnlyList<List<Location>> data)
        => data[current.Row][current.Column] = current with {Seen = true};
}