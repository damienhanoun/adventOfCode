using System.Reflection;
using AdventOfCode;

// From cookie session after being authenticated on https://adventofcode.com/
Day.SetSessionToken("your session token here");

var year = 2024;
var dayTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(Day)))
    .OrderBy(t => t.Name);

foreach (var dayType in dayTypes)
    if (Activator.CreateInstance(dayType) is Day dayInstance)
    {
        var dayNumber = int.Parse(dayType.Name.Substring(3)); // Extract number from class name (e.g., "Day1" -> 1)
        await dayInstance.ExecuteAsync(year, dayNumber);
    }