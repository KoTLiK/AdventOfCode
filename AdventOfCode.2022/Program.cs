using static AdventOfCode.Application;

var setup = CreateSetup(args);
var container = CreateContainer();
return await Run(setup, container);