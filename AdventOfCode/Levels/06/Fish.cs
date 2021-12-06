namespace AdventOfCode.Levels._06;

public class Fish
{
    public int RemainingDays { get; private set; }

    private bool _newBorn;

    public Fish(int remainingDays)
    {
        RemainingDays = remainingDays;
        _newBorn = true;
    }

    public static Fish operator --(Fish fish)
    {
        if (fish._newBorn)
        {
            fish._newBorn = false;
        }
        else if (fish.RemainingDays-- == 0)
        {
            fish.RemainingDays = 6;
        }
        return fish;
    }
}