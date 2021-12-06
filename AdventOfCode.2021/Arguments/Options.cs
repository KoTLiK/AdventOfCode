using CommandLine;

namespace AdventOfCode.Arguments;

public class Options
{
    [Option('l', "level", Required = true, HelpText = "Choose your level: 1-25")]
    public int? Level { get; set; }

    [Option('t', "type", Required = true, HelpText = "Choose one of [Example, Quest]")]
    public LevelType? Type { get; set; }

    [Option('r', "round", Required = true, HelpText = "Round of the given level: 1-2")]
    public int? Round { get; set; }
}