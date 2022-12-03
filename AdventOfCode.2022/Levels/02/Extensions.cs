namespace AdventOfCode.Levels._02;

public static class Extensions
{
    public static Result Beats(this Hand hand, Hand other)
        => (hand, other) switch
        {
            (Hand.Paper, Hand.Rock) => Result.Win,
            (Hand.Paper, Hand.Scissors) => Result.Loose,
            (Hand.Rock, Hand.Scissors) => Result.Win,
            (Hand.Rock, Hand.Paper) => Result.Loose,
            (Hand.Scissors, Hand.Paper) => Result.Win,
            (Hand.Scissors, Hand.Rock) => Result.Loose,
            _ => Result.Draw
        };

    public static Hand FindCheatHand(this Hand hand, Result result)
    {
        foreach (var current in typeof(Hand).GetEnumValues().Cast<Hand>())
        {
            if (current.Beats(hand) == result)
            {
                return current;
            }
        }

        throw new InvalidOperationException();
    }
}