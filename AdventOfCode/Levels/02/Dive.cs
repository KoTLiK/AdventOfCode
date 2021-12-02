using System.Text;

namespace AdventOfCode.Levels._02;

public class Dive : ALevel<int>
{
    public Dive(IResultCollector<int> resultCollector)
        : base(resultCollector)
    {
    }

    protected override Task Run()
    {
        using var fileStream = File.Open(FileName, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(fileStream, Encoding.UTF8);

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

        return Result(submarine.Answer());
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