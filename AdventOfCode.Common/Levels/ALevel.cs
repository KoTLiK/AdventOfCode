using System.Reflection;
using System.Text;
using System.Text.Json;
using AdventOfCode.Arguments;

namespace AdventOfCode.Levels;

public abstract class ALevel<T> : ILevel
{
    private readonly IResultCollector<T> _resultCollector;
    private string _partialFileName = string.Empty;
    protected Setup Setup = null!;

    protected ALevel(IResultCollector<T> resultCollector)
    {
        _resultCollector = resultCollector;
    }

    private string FileName
        => GetFileName() ?? throw new InvalidOperationException("Unable to find file in file-system");

    public ILevel Configure(Setup setup)
    {
        Setup = setup;
        _partialFileName = Path.Combine(Setup?.Level.ToString() ?? "", Setup?.Type.ToString() ?? "");
        return this;
    }

    public virtual Task<int> RunAsync()
    {
        if (Setup is null)
        {
            return Task.FromResult(42);
        }

        using var reader = ContentStreamReader();
        Result(Run(reader));

        return Task.FromResult(0);
    }

    protected abstract T Run(StreamReader reader);

    protected static IEnumerable<string> ReadLine(TextReader reader)
    {
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    protected bool IsFirstRound()
        => Setup.Round == 1;

    protected bool IsSecondRound()
        => Setup.Round == 2;

    private void Result(T result)
    {
        _resultCollector.Collect(result);
        Log.Information(JsonSerializer.Serialize(result));
    }

    private StreamReader ContentStreamReader()
        => new StreamReader(File.Open(FileName, FileMode.Open, FileAccess.Read), Encoding.UTF8);

    private string? GetFileName()
        => GetFileNames().FirstOrDefault(name => name.Contains(_partialFileName));

    private static IEnumerable<string> GetFileNames()
        => Directory.GetFiles(Path.Combine(GetPath(), "Assets"), "*", SearchOption.AllDirectories);

    private static string GetPath()
        => Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath)
           ?? throw new InvalidOperationException("Unable to resolve executing assembly path");
}