using System.Text;

namespace AdventOfCode.Levels._12;

public sealed class Route
{
    private readonly StringBuilder _builder = new();
    private readonly IDictionary<string, int> _points = new Dictionary<string, int>();

    public Route()
    {
    }

    private Route(StringBuilder builder, IDictionary<string, int> points)
    {
        _builder = builder;
        _points = points;
    }

    public Route Copy()
    {
        var points = new Dictionary<string, int>(_points);
        var builder = new StringBuilder(_builder.Length);
        foreach (var chunk in _builder.GetChunks())
        {
            builder.Append(chunk);
        }

        return new Route(builder, points);
    }

    public string? Step(Path path)
    {
        var (@from, to) = path;
        if (!_points.ContainsKey(@from))
        {
            _points[@from] = 0;
        }
        else if (@from.All(char.IsLower))
        {
            return null;
        }

        AddKey(@from);

        _points[@from]++;
        return to;
    }

    public string? StepSecond(Path path)
    {
        var (@from, to) = path;
        if (!_points.ContainsKey(@from))
        {
            _points[@from] = 0;
        }
        else if (@from.All(char.IsLower))
        {
            if (_points.Any(p => p.Key.All(char.IsLower) && p.Value > 1))
            {
                return null;
            }
        }

        AddKey(@from);

        _points[@from]++;
        return to;
    }

    public Route Finish(string lastKey) => AddKey(lastKey);

    public override int GetHashCode() => _builder.ToString().GetHashCode();

    public bool Equals(Route? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetHashCode() == other.GetHashCode();
    }

    public override string ToString() => _builder.ToString();

    private Route AddKey(string key)
    {
        if (_builder.Length > 0)
        {
            _builder.Append(',');
        }

        _builder.Append(key);

        return this;
    }
}