namespace AdventOfCode.Levels._04;

public class Cell
{
    public int Value { get; }
    public bool IsMarked { get; private set; }

    public Cell(int value, bool isMarked)
    {
        Value = value;
        IsMarked = isMarked;
    }

    public bool MarkIfMatch(int value)
        => Value == value && (IsMarked = true);

    public override string ToString()
        => IsMarked ? $"[{Value,2}]" : $"{Value,2}";
}