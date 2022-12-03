namespace AdventOfCode.Levels._01;

public record Elf(ICollection<int> FoodCalories)
{
    public int TotalCalories => FoodCalories.Sum();
}