namespace AdventOfCode.Levels._06;

public static class LanternFishExtensions
{
    public static IEnumerable<Fish> SpawnFish(this IEnumerable<Fish> source)
    {
        foreach (var fish in source)
        {
            if (fish.RemainingDays == 0)
            {
                yield return new Fish(8);
            }

            yield return fish;
        }
    }
}