namespace AdventOfCode._2024;

public class Day2 : Day
{
    protected override void Solve(string input)
    {
        // Split the input into reports (lines)
        var reports = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var safeCount = 0;

        // Iterate over each report
        foreach (var report in reports)
        {
            // Parse the numbers in the report
            var levels = report.Split(' ').Select(int.Parse).ToArray();

            var isIncreasing = true;
            var isDecreasing = true;
            var isSafe = true;

            // Check each pair of adjacent levels
            for (var i = 1; i < levels.Length; i++)
            {
                var difference = levels[i] - levels[i - 1];

                if (difference < 1 || difference > 3)
                {
                    isSafe = false; // Break the valid difference rule
                    break;
                }

                if (difference > 0) isDecreasing = false; // If increasing, cannot be decreasing
                if (difference < 0) isIncreasing = false; // If decreasing, cannot be increasing
            }

            // A report is safe if it's either fully increasing or fully decreasing, and follows all rules
            if (isSafe && (isIncreasing || isDecreasing)) safeCount++;
        }

        // Provide the result as the answer
        Answer(safeCount.ToString());
    }
}

// input example
// 22 25 27 28 30 31 32 29
// 72 74 75 77 80 81 81
// 52 53 55 58 59 63
// 14 17 19 22 27
// 65 68 67 68 71 73 76 77