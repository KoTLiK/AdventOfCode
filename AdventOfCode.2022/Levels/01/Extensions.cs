namespace AdventOfCode.Levels._01;

public static class Extensions
{
    public static IEnumerable<Elf> CreateElf(this IEnumerable<int?> source)
    {
        var calories = new List<int>();
        foreach (var integer in source)
        {
            if (integer is null)
            {
                yield return new Elf(calories);
                calories = new List<int>();
            }
            else
            {
                calories.Add(integer.Value);
            }
        }

        yield return new Elf(calories);
    }
}