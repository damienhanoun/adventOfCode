namespace AdventOfCode._2024;

public class Day1 : Day
{
    protected override void Solve(string input)
    {
        var pairs = ParseInput(input);

        var leftList = pairs.Select(pair => pair[0]).ToList();
        var rightList = pairs.Select(pair => pair[1]).ToList();

        Answer($"Total Distance: {CalculateTotalDistance(leftList, rightList)}");
        Answer($"Similarity Score: {CalculateSimilarityScore(leftList, rightList)}");
    }

    private static List<int[]> ParseInput(string input)
    {
        return input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseLine)
            .Where(pair => pair != null)
            .ToList()!;
    }

    private static int[]? ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || !int.TryParse(parts[0], out var left) || !int.TryParse(parts[1], out var right))
        {
            Console.WriteLine($"Skipping invalid line: {line}");
            return null;
        }

        return new[] { left, right };
    }

    private static int CalculateTotalDistance(List<int> leftList, List<int> rightList)
    {
        leftList.Sort();
        rightList.Sort();

        return leftList
            .Select((value, index) => Math.Abs(value - rightList[index]))
            .Sum();
    }

    private static int CalculateSimilarityScore(List<int> leftList, List<int> rightList)
    {
        var rightCountMap = rightList.GroupBy(x => x)
            .ToDictionary(group => group.Key, group => group.Count());

        return leftList
            .Select(leftValue => leftValue * rightCountMap.GetValueOrDefault(leftValue, 0))
            .Sum();
    }
}