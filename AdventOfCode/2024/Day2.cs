namespace AdventOfCode._2024;

public class Day2 : Day
{
    protected override void Solve(string input)
    {
        // Parse the input into a list of reports (each report is a list of integers)
        var reports = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
            .ToList();

        // Part 1: Count directly safe reports
        var safeReportCountPart1 = reports.Count(IsSafeReport);
        Answer($"Part 1 - Safe Reports: {safeReportCountPart1}");

        // Part 2: Count safe reports with the Problem Dampener
        var safeReportCountPart2 = reports.Count(report => IsSafeWithDampener(report));
        Answer($"Part 2 - Safe Reports: {safeReportCountPart2}");
    }

    /// <summary>
    ///     Determines whether a given report is safe.
    /// </summary>
    private bool IsSafeReport(List<int> report)
    {
        if (report.Count < 2) return false; // A report must have at least two levels

        // Determine the direction (increasing or decreasing)
        var isIncreasing = report[1] > report[0];
        var isDecreasing = report[1] < report[0];

        // Iterate through the report to check all conditions
        for (var i = 1; i < report.Count; i++)
        {
            var diff = report[i] - report[i - 1];

            // Check the difference condition
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                return false;

            // Ensure consistency in direction
            if (isIncreasing && diff <= 0) return false;
            if (isDecreasing && diff >= 0) return false;
        }

        // If we made it here, the report is safe
        return true;
    }

    /// <summary>
    ///     Determines whether a report is safe with the Problem Dampener.
    /// </summary>
    private bool IsSafeWithDampener(List<int> report)
    {
        // If the report is already safe, return true
        if (IsSafeReport(report)) return true;

        // Try removing each level and check if the modified report is safe
        for (var i = 0; i < report.Count; i++)
        {
            var modifiedReport = new List<int>(report);
            modifiedReport.RemoveAt(i);

            if (IsSafeReport(modifiedReport))
                return true;
        }

        // If no single removal makes it safe, return false
        return false;
    }
}