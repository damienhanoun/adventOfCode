namespace AdventOfCode;

public abstract class Day
{
    private const string AdventOfCodeBaseUrl = "https://adventofcode.com";
    private static string _sessionToken;

    // List to store answers for multiple parts
    private readonly List<string> _answers = new();

    public static void SetSessionToken(string sessionToken)
    {
        if (sessionToken == "your session token here")
            throw new ArgumentException("In Program.cs, set session token", nameof(sessionToken));
        _sessionToken = sessionToken ?? throw new ArgumentNullException(nameof(sessionToken));
    }

    public async Task ExecuteAsync(int year, int day)
    {
        if (year < 2015)
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be 2015 or later.");
        if (day is < 1 or > 31)
            throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31.");

        var inputData = await FetchDataAsync(year, day);

        // Clear any previous answers (in case the same instance is reused)
        _answers.Clear();

        Solve(inputData);

        // Output all answers
        for (var i = 0; i < _answers.Count; i++) Console.WriteLine($"Day {day} - Part {i + 1} : {_answers[i]}");
    }

    protected abstract void Solve(string input);

    /// <summary>
    ///     Adds an answer for a specific part of the problem.
    /// </summary>
    /// <param name="answer">The answer to add.</param>
    protected void Answer(string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be null or whitespace.", nameof(answer));

        _answers.Add(answer);
    }

    private async Task<string> FetchDataAsync(int year, int day)
    {
        if (string.IsNullOrWhiteSpace(_sessionToken))
            throw new InvalidOperationException("Session token is not set.");

        var url = $"{AdventOfCodeBaseUrl}/{year}/day/{day}/input";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={_sessionToken}");

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}