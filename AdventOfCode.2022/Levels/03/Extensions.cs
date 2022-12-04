namespace AdventOfCode.Levels._03;

public static class Extensions
{
    private const int UpperOffset = 26; 
    private const int DiffForUpper = 64 - UpperOffset; // 65 == 'A'
    private const int DiffForLower = 96; // 97 == 'a'

    public static int Priority(this char c)
        => char.IsLower(c) ? (byte) c - DiffForLower : (byte) c - DiffForUpper;

    public static IEnumerable<IList<Rucksack>> GroupByAmount(this IEnumerable<Rucksack> source, int amount)
    {
        var rucksacks = new List<Rucksack>(amount);
        foreach (var rucksack in source)
        {
            rucksacks.Add(rucksack);

            if (rucksacks.Count < amount) continue;

            yield return rucksacks;
            rucksacks = new List<Rucksack>(amount);
        }

        if (rucksacks.Count < amount) yield break;

        yield return rucksacks;
    }
}