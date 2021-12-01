using System.Reflection;
using System.Text.Json;
using AdventOfCode.Arguments;

namespace AdventOfCode.Levels;

public abstract class ALevel : ILevel
{
    protected Setup? Setup;
    private string partialFileName = string.Empty;

    protected string FileName
        => GetFileName() ?? throw new InvalidOperationException("Unable to find file in file-system");

    public ALevel Configure(Setup setup)
    {
        this.Setup = setup;
        this.partialFileName = $"{this.Setup?.Level}/{this.Setup?.Type.ToString()}";
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

    protected static Task Result<T>(T result)
    {
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
        => Directory.GetFiles($"{GetPath()}/Assets", "*", SearchOption.AllDirectories)
            .FirstOrDefault(name => name.Contains(this.partialFileName));

    private static string GetPath()
        => Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath)
           ?? throw new InvalidOperationException("Unable to resolve executing assembly path");
}