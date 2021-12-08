// See https://aka.ms/new-console-template for more information

using System.Reflection;
using AdventOfCode.Arguments;
using AdventOfCode.Levels;
using Autofac;
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

var builder = new ContainerBuilder();
builder.RegisterGeneric(typeof(ResultCollector<>))
    .As(typeof(IResultCollector<>))
    .InstancePerDependency();

var types = (Assembly.GetAssembly(typeof(ALevel<>))?.GetTypes() ?? Enumerable.Empty<Type>())
    .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface(nameof(ILevel)) is not null)
    .Select(t => (Type: t, Key: int.Parse(t.Namespace?.Split("_")[1] ?? "0")))
    .ToDictionary(t => t.Key, t => t.Type);

foreach (var (key, type) in types)
{
    builder.RegisterType(type).Keyed<ILevel>(key);
}

var container = builder.Build();

Console.WriteLine("Welcome to the Advent of Code!");
Console.WriteLine("     Level:    {0}", setup.Level);
Console.WriteLine("Difficulty:    {0}", setup.Type.ToString());
Console.WriteLine("     Round:    {0}", setup.Round);
Console.WriteLine("============ START ============");

try
{
    return await container
        .ResolveKeyed<ILevel>(setup.Level)
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