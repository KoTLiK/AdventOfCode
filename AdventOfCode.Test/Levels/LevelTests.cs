using System;
using System.Threading.Tasks;
using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using AdventOfCode.Levels._01;
using AdventOfCode.Levels._02;
using AdventOfCode.Levels._03;
using AdventOfCode.Levels._04;
using AdventOfCode.Levels._05;
using AdventOfCode.Levels._06;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace AdventOfCode.Test.Levels;

public class LevelTests
{
    private class Level
    {
        public const int SonarSweep = 1;
        public const int Dive = 2;
        public const int BinaryDiagnostic = 3;
        public const int GiantSquid = 4;
        public const int HydrothermalVenture = 5;
        public const int LanternFish = 6;
    }

    private static async Task Test<TLevel, TResult>(LevelType type, int round, TResult result)
        where TLevel : ALevel<TResult>
    {
        var level = typeof(Level)
            .GetField(typeof(TLevel).Name)
            ?.GetValue(new Level()) as int?
                    ?? throw new NullException("Level value is not defined");

        var collector = new ResultCollector<TResult>();
        var setup = new Setup(level, type, round);

        var exitCode = await (Activator.CreateInstance(typeof(TLevel), collector) as TLevel)!
            .Configure(setup).RunAsync();

        exitCode.Should().Be(0);
        collector.Retrieve().Should().Be(result);
    }

    [Theory]
    [InlineData(LevelType.Example, 1, 7)]
    [InlineData(LevelType.Example, 2, 5)]
    [InlineData(LevelType.Quest, 1, 1665)]
    [InlineData(LevelType.Quest, 2, 1702)]
    public Task SonarSweep(LevelType type, int round, int result)
        => Test<SonarSweep, int>(type, round, result);

    [Theory]
    [InlineData(LevelType.Example, 1, 150)]
    [InlineData(LevelType.Example, 2, 900)]
    [InlineData(LevelType.Quest, 1, 1636725)]
    [InlineData(LevelType.Quest, 2, 1872757425)]
    public Task Dive(LevelType type, int round, int result)
        => Test<Dive, int>(type, round, result);

    [Theory]
    [InlineData(LevelType.Example, 1, 198)]
    [InlineData(LevelType.Example, 2, 230)]
    [InlineData(LevelType.Quest, 1, 1092896)]
    [InlineData(LevelType.Quest, 2, 4672151)]
    public Task BinaryDiagnostic(LevelType type, int round, int result)
        => Test<BinaryDiagnostic, int>(type, round, result);

    [Theory]
    [InlineData(LevelType.Example, 1, 4512)]
    [InlineData(LevelType.Example, 2, 1924)]
    [InlineData(LevelType.Quest, 1, 55770)]
    [InlineData(LevelType.Quest, 2, 2980)]
    public Task GiantSquid(LevelType type, int round, int result)
        => Test<GiantSquid, int>(type, round, result);

    [Theory]
    [InlineData(LevelType.Example, 1, 5)]
    [InlineData(LevelType.Example, 2, 12)]
    [InlineData(LevelType.Quest, 1, 5084)]
    [InlineData(LevelType.Quest, 2, 17882)]
    public Task HydrothermalVenture(LevelType type, int round, int result)
        => Test<HydrothermalVenture, int>(type, round, result);

    [Theory]
    [InlineData(LevelType.Example, 1, 5934)]
    [InlineData(LevelType.Example, 2, 26984457539)]
    [InlineData(LevelType.Quest, 1, 376194)]
    [InlineData(LevelType.Quest, 2, 1693022481538)]
    public Task LanternFish(LevelType type, int round, decimal result)
        => Test<LanternFish, decimal>(type, round, result);
}