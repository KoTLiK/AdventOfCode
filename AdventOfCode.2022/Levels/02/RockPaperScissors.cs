namespace AdventOfCode.Levels._02;

public class RockPaperScissors : ALevel<int>
{
    public RockPaperScissors(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var rounds = ReadLine(reader)
            .Select(line => line.Split(' '))
            .Select(parts => new Round(parts[0], parts[1]));

        var roundQuery = IsFirstRound()
            ? rounds.Select(round => (
                Hand: round.HandTwo,
                Result: round.HandTwo.Beats(round.HandOne)))
            : rounds.Select(round => (
                Hand: round.HandOne.FindCheatHand(round.CheatSheetTwo),
                Result: round.CheatSheetTwo));

        return roundQuery
            .Select(round => (int)round.Hand + (int)round.Result)
            .Sum();
    }
}