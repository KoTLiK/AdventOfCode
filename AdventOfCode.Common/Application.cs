using System.Reflection;
using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using Autofac;
using CommandLine;

namespace AdventOfCode;

public static class Application
{
    public static Setup CreateSetup(IEnumerable<string> args)
        => Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                if (o.Level is < 1 or > 25)
                {
                    Log.Error("The level {@Level} is not supported", o.Level);
                    Environment.Exit(1);
                }

                if (o.Round is < 1 or > 2)
                {
                    Log.Error("The round {@Round} is not supported", o.Round);
                    Environment.Exit(1);
                }
            })
            .WithNotParsed(_ => Environment.Exit(1))
            .MapResult(
                o => new Setup(o.Level!.Value, o.Type!.Value, o.Round!.Value),
                _ => throw new ArgumentException("Unable to create Setup for the application"));

    public static IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterGeneric(typeof(ResultCollector<>))
            .As(typeof(IResultCollector<>))
            .InstancePerDependency();

        var types = (Assembly.GetEntryAssembly()?.GetTypes() ?? Enumerable.Empty<Type>())
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface(nameof(ILevel)) is not null)
            .Select(t => (Type: t, Key: int.Parse(t.Namespace?.Split("_")[1] ?? "0")))
            .ToDictionary(t => t.Key, t => t.Type);

        foreach (var (key, type) in types)
            builder.RegisterType(type).Keyed<ILevel>(key);

        return builder.Build();
    }

    public static async Task<int> Run(Setup setup, IContainer container)
    {
        Log.Information("Welcome to the Advent of Code!");
        Log.Information("     Level:    {@Level}", setup.Level);
        Log.Information("Difficulty:    {@Type}", setup.Type.ToString());
        Log.Information("     Round:    {@Round}", setup.Round);
        Log.Information("============ START ============");

        try
        {
            return await container
                .ResolveKeyed<ILevel>(setup.Level)
                .Configure(setup)
                .RunAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "{@Message}", e.Message);
            return 1;
        }
        finally
        {
            Log.Information("============  END  ============");
            await Log.CloseAndFlushAsync();
        }
    }
}