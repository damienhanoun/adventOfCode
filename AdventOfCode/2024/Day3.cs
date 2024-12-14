using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day3 : Day
{
    protected override void Solve(string input)
    {
        var mulRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        var doRegex = new Regex(@"\bdo\(\)");
        var dontRegex = new Regex(@"\bdon't\(\)");

        Answer($"Total Sum of All Multiplications: {CalculateTotalMultiplications(input, mulRegex)}");
        Answer($"Total Sum of Enabled Multiplications: {CalculateConditionalMultiplications(input, mulRegex, doRegex, dontRegex)}");
    }

    private static int CalculateTotalMultiplications(string input, Regex mulRegex)
    {
        return mulRegex.Matches(input)
            .Select(match => (
                X: int.Parse(match.Groups[1].Value),
                Y: int.Parse(match.Groups[2].Value)))
            .Sum(pair => pair.X * pair.Y);
    }

    private static int CalculateConditionalMultiplications(string input, Regex mulRegex, Regex doRegex, Regex dontRegex)
    {
        var totalSum = 0;
        var isEnabled = true;

        for (var i = 0; i < input.Length;)
        {
            if (TryMatch(input, i, mulRegex, out var mulMatch))
            {
                if (isEnabled) totalSum += ExtractMultiplicationValue(mulMatch);
                i += mulMatch.Length;
                continue;
            }

            if (TryMatch(input, i, doRegex, out _))
            {
                isEnabled = true;
                i += doRegex.ToString().Length;
                continue;
            }

            if (TryMatch(input, i, dontRegex, out _))
            {
                isEnabled = false;
                i += dontRegex.ToString().Length;
                continue;
            }

            i++;
        }

        return totalSum;
    }

    private static bool TryMatch(string input, int start, Regex regex, out Match match)
    {
        match = regex.Match(input, start);
        return match.Success && match.Index == start;
    }

    private static int ExtractMultiplicationValue(Match match)
    {
        var x = int.Parse(match.Groups[1].Value);
        var y = int.Parse(match.Groups[2].Value);
        return x * y;
    }
}