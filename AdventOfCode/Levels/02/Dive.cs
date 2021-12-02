using System.Text;

namespace AdventOfCode.Levels._02;

public class Dive : ALevel
{
    protected override Task Run()
    {
        using var fileStream = File.Open(this.FileName, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(fileStream, Encoding.UTF8);

        var submarine = new SubmarineMovement();
        foreach (var command in ReadLine(reader).Select(ParseLine))
        {
            if (this.Setup is {Round: 1})
            {
                submarine.Move(command);
            }
            else if (this.Setup is {Round: 2})
            {
                submarine.MoveAim(command);
            }
        }

        return Result(submarine.Answer());
    }

    private static SubmarineCommand ParseLine(string line)
    {
        var parts = line.Split(' ')
            .Select(p => p.Trim())
            .ToArray();
        var movement = Enum.Parse<Movement>(parts[0], true);
        var value = int.Parse(parts[1]);
        
        return new SubmarineCommand(movement, value);
    }
}