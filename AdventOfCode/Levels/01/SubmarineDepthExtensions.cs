namespace AdventOfCode.Levels._01;

public static class SubmarineDepthExtensions
{
    public static IEnumerable<bool?> IncreaseCheck(this IEnumerable<int> source, int? previousValue)
    {
        foreach (var currentValue in source)
        {
            if (previousValue is null)
            {
                yield return null;
            }

            var result = previousValue < currentValue;
            previousValue = currentValue;

            yield return result;
        }
    }
    
    public static IEnumerable<int> SlidingWindowOfSums(this IEnumerable<int> source, int size)
    {
        var queue = new Queue<int>(size << 1 - 1);

        foreach (var value in source)
        {
            queue.Enqueue(value);

            if (queue.Count < size) continue;

            yield return queue.Sum();
            queue.Dequeue();
        }
    }
}