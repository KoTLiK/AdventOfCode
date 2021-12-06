namespace AdventOfCode.Levels._02;

public class Dive : ALevel<int>
{
    public Dive(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override int Run(StreamReader reader)
    {
        var submarine = new SubmarineMovement();
        foreach (var command in ReadLine(reader).Select(ParseLine))
        {
            if (Setup is {Round: 1})
            {
                submarine.Move(command);
            }
            else if (Setup is {Round: 2})
            {
                submarine.MoveAim(command);
            }
        }

        return submarine.Answer();
    }

    private static Command ParseLine(string line)
    {
        var parts = line.Split(' ')
            .Select(p => p.Trim())
            .ToArray();
        var movement = Enum.Parse<Movement>(parts[0], true);
        var value = int.Parse(parts[1]);

        return new Command(movement, value);
    }
}