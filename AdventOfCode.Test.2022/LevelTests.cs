using AdventOfCode.Arguments;
using AdventOfCode.Levels._01;
using AdventOfCode.Levels._02;
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

    [Theory]
    [InlineData(LevelType.Example, 1, 15)]
    [InlineData(LevelType.Example, 2, 12)]
    [InlineData(LevelType.Quest, 1, 13268)]
    [InlineData(LevelType.Quest, 2, 15508)]
    public Task RockPaperScissors(LevelType type, int round, int result)
        => Test<RockPaperScissors, int>(type, round, result);
}