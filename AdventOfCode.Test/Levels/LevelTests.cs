using System.Threading.Tasks;
using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using AdventOfCode.Levels._01;
using AdventOfCode.Levels._02;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Test.Levels;

public class LevelTests
{
    private class Level
    {
        public const int SonarSweep = 1;
        public const int Dive = 2;
    }

    [Theory]
    [InlineData(LevelType.Example, 1, 7)]
    [InlineData(LevelType.Example, 2, 5)]
    [InlineData(LevelType.Quest, 1, 1665)]
    [InlineData(LevelType.Quest, 2, 1702)]
    public async Task SonarSweep(LevelType type, int round, int result)
    {
        var collector = new ResultCollector<int>();
        var setup = new Setup(Level.SonarSweep, type, round);

        var exitCode = await new SonarSweep(collector).Configure(setup).RunAsync();

        exitCode.Should().Be(0);
        collector.Retrieve().Should().Be(result);
    }

    [Theory]
    [InlineData(LevelType.Example, 1, 150)]
    [InlineData(LevelType.Example, 2, 900)]
    [InlineData(LevelType.Quest, 1, 1636725)]
    [InlineData(LevelType.Quest, 2, 1872757425)]
    public async Task Dive(LevelType type, int round, int result)
    {
        var collector = new ResultCollector<int>();
        var setup = new Setup(Level.Dive, type, round);

        var exitCode = await new Dive(collector).Configure(setup).RunAsync();

        exitCode.Should().Be(0);
        collector.Retrieve().Should().Be(result);
    }
}