namespace AdventOfCode.Levels._02;

public class SubmarineMovement
{
    public int Horizontal { get; private set; }

    public int Depth { get; private set; }

    public int Aim { get; private set; }

    public void Move(Command command)
    {
        switch (command.Movement)
        {
            case Movement.Forward:
                Horizontal += command.Value;
                break;
            case Movement.Down:
                Depth += command.Value;
                break;
            case Movement.Up:
                Depth -= command.Value;
                break;
        }
    }

    public void MoveAim(Command command)
    {
        switch (command.Movement)
        {
            case Movement.Forward:
                Horizontal += command.Value;
                Depth += Aim * command.Value;
                break;
            case Movement.Down:
                Aim += command.Value;
                break;
            case Movement.Up:
                Aim -= command.Value;
                break;
        }
    }

    public int Answer() => Horizontal * Depth;
}