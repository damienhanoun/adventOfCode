namespace AdventOfCode._2024;

public class Day2 : Day
{
    protected override void Solve(string input)
    {
        var reports = ParseReports(input);

        var safeReportCountPart1 = reports.Count(IsSafeReport);
        Answer($"Safe Reports: {safeReportCountPart1}");

        var safeReportCountPart2 = reports.Count(IsSafeWithDampener);
        Answer($"Safe Reports: {safeReportCountPart2}");
    }

    private static List<List<int>> ParseReports(string input)
    {
        return input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
            .ToList();
    }

    private bool IsSafeReport(List<int> report)
    {
        if (report.Count < 2) return false;

        var isIncreasing = report[1] > report[0];
        var isDecreasing = report[1] < report[0];

        for (var i = 1; i < report.Count; i++)
        {
            var diff = report[i] - report[i - 1];

            if (!IsValidDifference(diff) ||
                !IsConsistentDirection(diff, isIncreasing, isDecreasing))
                return false;
        }

        return true;
    }

    private static bool IsValidDifference(int diff)
    {
        return Math.Abs(diff) >= 1 && Math.Abs(diff) <= 3;
    }

    private static bool IsConsistentDirection(int diff, bool isIncreasing, bool isDecreasing)
    {
        return !(isIncreasing && diff <= 0) && !(isDecreasing && diff >= 0);
    }

    private bool IsSafeWithDampener(List<int> report)
    {
        if (IsSafeReport(report)) return true;

        return report
            .Select((_, i) => RemoveLevelAt(report, i))
            .Any(IsSafeReport);
    }

    private static List<int> RemoveLevelAt(List<int> report, int index)
    {
        var modifiedReport = new List<int>(report);
        modifiedReport.RemoveAt(index);
        return modifiedReport;
    }
}