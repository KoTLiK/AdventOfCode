using System.Collections.ObjectModel;

namespace AdventOfCode.Levels._04;

public class BoardBuilder
{
    private readonly IList<IReadOnlyList<Cell>> _rows = new List<IReadOnlyList<Cell>>();

    public void AddRow(string row)
    {
        var rowData = row.Split(' ')
            .Where(col => !string.IsNullOrWhiteSpace(col))
            .Select(int.Parse)
            .Select(value => new Cell(value, false))
            .ToList();
        _rows.Add(rowData.AsReadOnly());
    }

    public Board Build() => new (new ReadOnlyCollection<IReadOnlyList<Cell>>(_rows));
}