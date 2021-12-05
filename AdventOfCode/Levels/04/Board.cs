namespace AdventOfCode.Levels._04;

public class Board
{
    private readonly IReadOnlyList<IReadOnlyList<Cell>> _rows;

    public bool IsSolved { get; private set; }

    public Board(IReadOnlyList<IReadOnlyList<Cell>> rows)
    {
        _rows = rows;
    }

    public int Calculate()
        => _rows.SelectMany(row => row)
            .Where(cell => !cell.IsMarked)
            .Sum(cell => cell.Value);

    public Board Mark(int value)
    {
        foreach (var cell in _rows.SelectMany(row => row))
        {
            cell.MarkIfMatch(value);
        }

        return this;
    }

    public bool Check() => IsSolved = RowCheck() || ColumnCheck();

    private bool RowCheck()
        => _rows.Any(row => row.All(cell => cell.IsMarked));

    private bool ColumnCheck()
    {
        for (var i = 0; i < _rows[0].Count; i++)
        {
            if (_rows.All(row => row[i].IsMarked))
            {
                return true;
            }
        }

        return false;
    }
}