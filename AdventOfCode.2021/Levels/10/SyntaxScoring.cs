using System.Collections.ObjectModel;

namespace AdventOfCode.Levels._10;

public class SyntaxScoring : ALevel<long>
{
    private static readonly char[] Openings = {'(', '[', '{', '<'};

    private static readonly IReadOnlyDictionary<char, int> CorruptedScore =
        new ReadOnlyDictionary<char, int>(new Dictionary<char, int> {{')', 3}, {']', 57}, {'}', 1197}, {'>', 25137}});

    private static readonly IReadOnlyDictionary<char, int> InvalidScore =
        new ReadOnlyDictionary<char, int>(new Dictionary<char, int> {{')', 1}, {']', 2}, {'}', 3}, {'>', 4}});

    private readonly IDictionary<char, int> _corrupted =
        new Dictionary<char, int> {{')', 0}, {']', 0}, {'}', 0}, {'>', 0}};

    public SyntaxScoring(IResultCollector<long> resultCollector) : base(resultCollector)
    {
    }

    protected override long Run(StreamReader reader)
    {
        var input = ReadLine(reader).ToList();

        var invalid = input
            .Select(State)
            .Where(s => s?.Any() == true)
            .ToList();

        if (Setup is {Round: 1})
        {
            return _corrupted.Aggregate(0, (current, p) => current + CorruptedScore[p.Key] * p.Value);
        }

        return invalid
            .Select(s => string.Join(string.Empty, s!.Select(Opposite)))
            .Select(l => l.Aggregate(0L, (current, c) => current * 5 + InvalidScore[c]))
            .OrderByDescending(v => v)
            .Skip(invalid.Count >> 1)
            .First();
    }

    private Stack<char>? State(string line)
    {
        var stack = new Stack<char>();
        foreach (var c in line)
        {
            if (Openings.Contains(c))
            {
                stack.Push(c);
            }
            else if (stack.Peek() == Opposite(c))
            {
                stack.Pop();
            }
            else
            {
                _corrupted[c]++;
                return null;
            }
        }

        return stack;
    }

    private static char Opposite(char source)
        => source switch
        {
            '(' => ')',
            ')' => '(',
            '[' => ']',
            ']' => '[',
            '{' => '}',
            '}' => '{',
            '<' => '>',
            '>' => '<',
            _ => throw new InvalidOperationException()
        };
}