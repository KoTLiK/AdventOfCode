using System.Collections.Immutable;

namespace AdventOfCode.Levels._02;

public record Round(string One, string Two)
{
    public static readonly ImmutableSortedDictionary<string, Hand> Hands
        = new SortedDictionary<string, Hand>
        {
            {"A", Hand.Rock},
            {"X", Hand.Rock},
            {"B", Hand.Paper},
            {"Y", Hand.Paper},
            {"C", Hand.Scissors},
            {"Z", Hand.Scissors},
        }.ToImmutableSortedDictionary();

    public static readonly ImmutableSortedDictionary<string, Result> CheatSheet
        = new SortedDictionary<string, Result>
        {
            {"X", Result.Loose},
            {"Y", Result.Draw},
            {"Z", Result.Win},
        }.ToImmutableSortedDictionary();

    public Hand HandOne => Hands[One];

    public Hand HandTwo => Hands[Two];

    public Result CheatSheetTwo => CheatSheet[Two];
}

public enum Result
{
    Loose = 0,
    Draw = 3,
    Win = 6,
}

public enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}