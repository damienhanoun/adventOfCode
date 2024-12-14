namespace AdventOfCode._2024;

public class Day1 : Day
{
    protected override void Solve(string input)
    {
        // Parse input into two lists
        var pairs = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2 || !int.TryParse(parts[0], out var left) || !int.TryParse(parts[1], out var right))
                {
                    Console.WriteLine($"Skipping invalid line: {line}");
                    return null;
                }

                return new[] { left, right };
            })
            .Where(pair => pair != null)
            .ToArray();

        if (pairs.Length == 0)
        {
            Console.WriteLine("No valid input pairs to process.");
            return;
        }

        var leftList = pairs.Select(pair => pair[0]).ToList();
        var rightList = pairs.Select(pair => pair[1]).ToList();

        // Part 1: Calculate total distance with sorted lists
        leftList.Sort();
        rightList.Sort();
        var totalDistance = leftList
            .Select((value, index) => Math.Abs(value - rightList[index]))
            .Sum();
        Answer($"Total Distance: {totalDistance}");

        // Part 2: Minimize total distance

        // Create a dictionary to count occurrences of each number in the right list
        var rightCountMap = rightList.GroupBy(x => x)
            .ToDictionary(group => group.Key, group => group.Count());

        // Calculate similarity score
        var similarityScore = leftList
            .Select(leftValue => leftValue * (rightCountMap.ContainsKey(leftValue) ? rightCountMap[leftValue] : 0))
            .Sum();

        Answer($"Similarity Score: {similarityScore}");
    }
}