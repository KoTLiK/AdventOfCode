using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Levels._05;

public class SupplyStacks : ALevel<string>
{
    private static readonly Regex regex
        = new(@"^move (?<amount>\d+) from (?<from>\d+) to (?<to>\d+)$", RegexOptions.Compiled);

    public SupplyStacks(IResultCollector<string> resultCollector)
        : base(resultCollector)
    {
    }

    protected override string Run(StreamReader reader)
    {
        var rawItems = ReadLine(reader)
            .TakeWhile(line => !string.IsNullOrEmpty(line))
            .Select(line => LineIntoGroups(line).ToList())
            .ToList();

        var rows = rawItems.Count;
        var columns = rawItems.Min(line => line.Count);
        var stacks = Convert(rawItems, rows, columns);

        var instructions = ReadLine(reader)
            .Select(CreateInstruction)
            .ToList();

        foreach (var instruction in instructions)
        {
            ExecuteInstruction(instruction, stacks);
        }

        return string.Join(string.Empty, stacks.Select(s => s.Value.Peek()));
    }

    private void ExecuteInstruction(Instruction instruction, IDictionary<string, Stack<string>> stacks)
    {
        var values = Enumerable.Range(0, instruction.Amount).Select(_ => stacks[instruction.From].Pop());
        foreach (var value in IsFirstRound() ? values : values.Reverse())
        {
            stacks[instruction.To].Push(value);
        }
    }

    private static Instruction CreateInstruction(string line)
    {
        var match = regex.Match(line);
        return new Instruction(
            match.Groups["amount"].Value,
            match.Groups["from"].Value,
            match.Groups["to"].Value);
    }

    private static IEnumerable<string> LineIntoGroups(string source)
    {
        const int Size = 3;
        var builder = new StringBuilder(Size, Size);
        for (var i = 0; i < source.Length; i++)
        {
            builder.Append(source[i]);
            if (builder.Length < Size) continue;
            yield return builder.ToString();
            builder.Clear();
            i++;
        }

        if (builder.Length >= Size)
        {
            yield return builder.ToString();
        }
    }

    private static IDictionary<string, Stack<string>> Convert(List<List<string>> source, int rows, int columns)
    {
        var namedStacks = new Dictionary<string, Stack<string>>();
        for (var col = 0; col < columns; col++)
        {
            var name = source[rows - 1][col][1..2];
            var stack = new Stack<string>();

            for (var row = 1; row < rows; row++)
            {
                var value = source[rows - row - 1][col][1..2];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    stack.Push(value);
                }
            }
            namedStacks.Add(name, stack);
        }

        return namedStacks;
    }
}

public readonly struct Instruction
{
    public int Amount { get; }

    public string From { get; }

    public string To { get; }

    public Instruction(string amount, string from, string to)
    {
        this.Amount = int.Parse(amount);
        this.From = from;
        this.To = to;
    }
}