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
    [InlineData(LevelType.Example, 1, 7)]
    // [InlineData(LevelType.Example, 2, 5)]
    // [InlineData(LevelType.Quest, 1, 1665)]
    // [InlineData(LevelType.Quest, 2, 1702)]
    public Task CalorieCounting(LevelType type, int round, int result)
        => Test<CalorieCounting, int>(type, round, result);
}