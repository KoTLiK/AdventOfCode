namespace AdventOfCode.Levels._07;

public static class Extensions
{
    public static IEnumerable<(int Position, int Distance)> LinearDistanceFrom(
        this IEnumerable<int> source,
        params int[] data)
    {
        foreach (var value in source)
        {
            foreach (var distance in data)
            {
                yield return (Position: distance, Distance: Math.Abs(distance - value));
            }
        }
    }
}