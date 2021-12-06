// See https://aka.ms/new-console-template for more information

using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using AdventOfCode.Levels._01;
using AdventOfCode.Levels._02;
using AdventOfCode.Levels._03;
using AdventOfCode.Levels._04;
using AdventOfCode.Levels._05;
using AdventOfCode.Levels._06;
using CommandLine;

var setup = Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o =>
    {
        if (o.Level is < 1 or > 25)
        {
            Console.Error.WriteLine($"The level {o.Level} is not supported");
            Environment.Exit(1);
        }

        if (o.Round is < 1 or > 2)
        {
            Console.Error.WriteLine($"The round {o.Round} is not supported");
            Environment.Exit(1);
        }
    })
    .WithNotParsed(_ => Environment.Exit(1))
    .MapResult(
        o => new Setup(o.Level!.Value, o.Type!.Value, o.Round!.Value),
        _ => throw new ArgumentException("Unable to create Setup for the application"));

static ILevel Level(int level)
{
    return level switch
    {
        1 => new SonarSweep(new ResultCollector<int>()),
        2 => new Dive(new ResultCollector<int>()),
        3 => new BinaryDiagnostic(new ResultCollector<int>()),
        4 => new GiantSquid(new ResultCollector<int>()),
        5 => new HydrothermalVenture(new ResultCollector<int>()),
        6 => new LanternFish(new ResultCollector<long>()),
        _ => throw new InvalidOperationException("Other levels are not implemented yet")
    };
}

Console.WriteLine("Welcome to the Advent of Code!");
Console.WriteLine("     Level:    {0}", setup.Level);
Console.WriteLine("Difficulty:    {0}", setup.Type.ToString());
Console.WriteLine("     Round:    {0}", setup.Round);
Console.WriteLine("============ START ============");

try
{
    return await Level(setup.Level)
        .Configure(setup)
        .RunAsync();
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
    return 1;
}
finally
{
    Console.WriteLine("============  END  ============");
}
