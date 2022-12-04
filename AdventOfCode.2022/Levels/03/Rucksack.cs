namespace AdventOfCode.Levels._03;

public class Rucksack
{
    public string Items { get; init; }
    public string FirstCompartment { get; }
    public string SecondCompartment { get; }

    public Rucksack(string items)
    {
        this.Items = (items.Length & 0x1) == 0 ? items
            : throw new ArgumentOutOfRangeException(nameof(items), $"Too many characters {items.Length} in {items}");
        this.FirstCompartment = items[..(items.Length >> 1)];
        this.SecondCompartment = items[(items.Length >> 1)..];
    }
}