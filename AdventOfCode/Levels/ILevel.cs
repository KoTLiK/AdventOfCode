using AdventOfCode.Arguments;

namespace AdventOfCode.Levels;

public interface ILevel
{
    ILevel Configure(Setup setup);

    Task<int> RunAsync();
}