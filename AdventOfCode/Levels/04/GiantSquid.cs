using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Levels._04;

public class GiantSquid : ALevel<int>
{
    public GiantSquid(IResultCollector<int> resultCollector) : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var numbers = RandomNumbers(reader);
        var boards = ReadLine(reader).CreateBoards().ToList();

        
        if (Setup is {Round: 1})
        {
            if (CalculateFirstBoard(numbers, boards, out var result))
            {
                return result.Value;
            }
        }
        else if (Setup is {Round: 2})
        {
            if (CalculateLastBoard(numbers, boards, out var result))
            {
                return result.Value;
            }
        }

        throw new InvalidOperationException("No match found");
    }

    private static bool CalculateLastBoard(
        IEnumerable<int> numbers,
        ICollection<Board> boards,
        [NotNullWhen(true)] out int? result)
    {
        Board? finalBoard = null;
        foreach (var number in numbers)
        {
            var solved = boards.Select(b => b.Mark(number))
                .Count(b => b.Check());

            if (boards.Count - solved == 1)
            {
                finalBoard ??= boards.First(b => !b.IsSolved);
            }
            
            if (finalBoard?.Check() == true)
            {
                var value = finalBoard.Calculate();
                result = value * number;
                return true;   
            }
        }

        result = null;
        return false;
    }

    private static bool CalculateFirstBoard(
        IEnumerable<int> numbers,
        ICollection<Board> boards,
        [NotNullWhen(true)] out int? result)
    {
        foreach (var number in numbers)
        {
            var winner = boards.Select(b => b.Mark(number))
                .FirstOrDefault(b => b.Check());

            if (winner is null)
            {
                continue;
            }

            var value = winner.Calculate();
            result = value * number;
            return true;
        }

        result = null;
        return false;
    }

    private static IEnumerable<int> RandomNumbers(TextReader reader)
        => reader.ReadLine()?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
}