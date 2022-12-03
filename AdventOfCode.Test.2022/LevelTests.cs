using AdventOfCode.Arguments;
using AdventOfCode.Levels._01;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Test.Levels._2022;

public class LevelTests : LevelTest
{
    public LevelTests(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData(LevelType.Example, 1, 24000)]
    [InlineData(LevelType.Example, 2, 45000)]
    [InlineData(LevelType.Quest, 1, 69912)]
    [InlineData(LevelType.Quest, 2, 208180)]
    public Task CalorieCounting(LevelType type, int round, int result)
        => Test<CalorieCounting, int>(type, round, result);
}