using System.Reflection;
using System.Text.Json;
using AdventOfCode.Arguments;

namespace AdventOfCode.Levels;

public abstract class ALevel<T> : ILevel
{
    private readonly IResultCollector<T> _resultCollector;
    protected Setup? Setup;
    private string _partialFileName = string.Empty;

    protected string FileName
        => GetFileName() ?? throw new InvalidOperationException("Unable to find file in file-system");

    protected ALevel(IResultCollector<T> resultCollector)
    {
        _resultCollector = resultCollector;
    }

    public ILevel Configure(Setup setup)
    {
        Setup = setup;
        _partialFileName = $"{Setup?.Level}/{Setup?.Type.ToString()}";
        return this;
    }

    public virtual async Task<int> RunAsync()
    {
        if (Setup is null)
        {
            return 42;
        }

        await Run();
        return 0;
    }

    protected abstract Task Run();

    protected Task Result(T result)
    {
        _resultCollector.Collect(result);
        Console.WriteLine(JsonSerializer.Serialize(result));
        return Task.CompletedTask;
    }

    protected static IEnumerable<string> ReadLine(TextReader reader)
    {
        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }

    private string? GetFileName()
        => GetFileNames().FirstOrDefault(name => name.Contains(_partialFileName));

    private static IEnumerable<string> GetFileNames()
        => Directory.GetFiles($"{GetPath()}/Assets", "*", SearchOption.AllDirectories);

    private static string GetPath()
        => Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath)
           ?? throw new InvalidOperationException("Unable to resolve executing assembly path");
}