namespace AdventOfCode.Levels._11;

public static class DumboOctopusExtensions
{
    public static bool Update(this HashSet<Point> data, Point point)
        => data.Remove(point) && data.Add(point);

    private static Point? Seek(Point index, HashSet<Point> data)
        => data.TryGetValue(index, out var result) ? result : null;

    public static Point? Up(this Point current, HashSet<Point> data)
        => Seek(current with {Y = current.Row - 1}, data);

    public static Point? Down(this Point current, HashSet<Point> data)
        => Seek(current with {Y = current.Row + 1}, data);

    public static Point? Left(this Point current, HashSet<Point> data)
        => Seek(current with {X = current.Column - 1}, data);

    public static Point? Right(this Point current, HashSet<Point> data)
        => Seek(current with {X = current.Column + 1}, data);
}