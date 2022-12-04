using System.Diagnostics;
using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using FluentAssertions;
using FluentAssertions.Extensions;
using Serilog;
using Xunit.Abstractions;

namespace AdventOfCode.Test;

public abstract class LevelTest : IDisposable
{
    protected LevelTest(ITestOutputHelper output)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.TestOutput(output)
            .CreateLogger();
    }

    public void Dispose()
    {
        Log.CloseAndFlush();
        GC.SuppressFinalize(this);
    }

    protected static async Task Test<TLevel, TResult>(LevelType type, int round, TResult result)
        where TLevel : ALevel<TResult>
    {
        var level = int.Parse(typeof(TLevel).Namespace?.Split("_")[1] ?? "0");
        var collector = new ResultCollector<TResult>();
        var setup = new Setup(level, type, round);
        var test = (Activator.CreateInstance(typeof(TLevel), collector) as TLevel)!.Configure(setup);

        var watch = new Stopwatch();
        watch.Start();
        var exitCode = await test.RunAsync();
        watch.Stop();
        Log.Information("{@TimeElapsed}.{@MicroSeconds}ms", watch.ElapsedMilliseconds, watch.Elapsed.Microseconds());

        exitCode.Should().Be(0);
        collector.Retrieve().Should().Be(result);
    }
}