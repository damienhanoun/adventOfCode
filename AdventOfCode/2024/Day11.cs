namespace AdventOfCode._2024;

public class Day11 : Day
{
    protected override void Solve(string input)
    {
        var initialStones = ParseInput(input);

        const int Part1Blinks = 25;
        const int Part2Blinks = 75;

        Answer(SimulateBlinks(initialStones, Part1Blinks).ToString());
        Answer(SimulateBlinks(initialStones, Part2Blinks).ToString());
    }

    private static List<long> ParseInput(string input)
    {
        return input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }

    private static long SimulateBlinks(List<long> initialStones, int totalBlinks)
    {
        var stoneCounts = InitializeStoneCounts(initialStones);

        for (var blink = 0; blink < totalBlinks; blink++) stoneCounts = ProcessBlink(stoneCounts);

        return stoneCounts.Values.Sum();
    }

    private static Dictionary<long, long> InitializeStoneCounts(IEnumerable<long> stones)
    {
        var counts = new Dictionary<long, long>();

        foreach (var stone in stones)
        {
            if (!counts.ContainsKey(stone))
                counts[stone] = 0;

            counts[stone]++;
        }

        return counts;
    }

    private static Dictionary<long, long> ProcessBlink(Dictionary<long, long> stoneCounts)
    {
        var nextCounts = new Dictionary<long, long>();

        foreach (var (stone, count) in stoneCounts)
            if (stone == 0)
                AddToCounts(nextCounts, 1, count);
            else if (HasEvenDigits(stone))
                SplitStone(stone, count, nextCounts);
            else
                AddToCounts(nextCounts, stone * 2024, count);

        return nextCounts;
    }

    private static void SplitStone(long stone, long count, Dictionary<long, long> nextCounts)
    {
        var digits = stone.ToString();
        var mid = digits.Length / 2;

        AddToCounts(nextCounts, long.Parse(digits.Substring(0, mid)), count);
        AddToCounts(nextCounts, long.Parse(digits.Substring(mid)), count);
    }

    private static void AddToCounts(Dictionary<long, long> counts, long stone, long countToAdd)
    {
        if (!counts.ContainsKey(stone))
            counts[stone] = 0;

        counts[stone] += countToAdd;
    }

    private static bool HasEvenDigits(long stone)
    {
        return stone.ToString().Length % 2 == 0;
    }
}