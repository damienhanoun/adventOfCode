namespace AdventOfCode._2024;

public class Day5 : Day
{
    protected override void Solve(string input)
    {
        var sections = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var orderingRules = ParseOrderingRules(sections[0]);
        var updates = sections[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var (correctlyOrderedUpdates, incorrectlyOrderedUpdates) = SeparateUpdates(updates, orderingRules);

        var part1Sum = correctlyOrderedUpdates.Sum();
        var fixedMiddleSum = CalcFixedMiddleSum(incorrectlyOrderedUpdates, orderingRules);

        Answer(part1Sum.ToString());
        Answer(fixedMiddleSum.ToString());
    }

    private static Dictionary<int, HashSet<int>> ParseOrderingRules(string section)
    {
        return section
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(rule => rule.Split('|'))
            .GroupBy(parts => int.Parse(parts[0]))
            .ToDictionary(
                group => group.Key,
                group => group.Select(parts => int.Parse(parts[1])).ToHashSet()
            );
    }

    private static (List<int>, List<List<int>>) SeparateUpdates(IEnumerable<string> updates, Dictionary<int, HashSet<int>> rules)
    {
        var correctlyOrderedUpdates = new List<int>();
        var incorrectlyOrderedUpdates = new List<List<int>>();

        foreach (var update in updates)
        {
            var pages = update.Split(',').Select(int.Parse).ToList();
            if (IsValidOrder(pages, rules))
                correctlyOrderedUpdates.Add(GetMiddlePage(pages));
            else
                incorrectlyOrderedUpdates.Add(pages);
        }

        return (correctlyOrderedUpdates, incorrectlyOrderedUpdates);
    }

    private static bool IsValidOrder(List<int> update, Dictionary<int, HashSet<int>> rules)
    {
        var positions = update
            .Select((page, index) => (page, index))
            .ToDictionary(x => x.page, x => x.index);

        return rules.All(rule => rule.Value.All(y =>
            !positions.ContainsKey(rule.Key) ||
            !positions.ContainsKey(y) ||
            positions[rule.Key] <= positions[y]
        ));
    }

    private static int GetMiddlePage(List<int> update)
    {
        return update[update.Count / 2];
    }

    private static int CalcFixedMiddleSum(List<List<int>> incorrectlyOrderedUpdates, Dictionary<int, HashSet<int>> rules)
    {
        return incorrectlyOrderedUpdates
            .Select(update => FixOrder(update, rules))
            .Select(GetMiddlePage)
            .Sum();
    }

    private static List<int> FixOrder(List<int> update, Dictionary<int, HashSet<int>> rules)
    {
        var graph = BuildDependencyGraph(update, rules);
        return TopologicalSort(update, graph);
    }

    private static Dictionary<int, List<int>> BuildDependencyGraph(List<int> update, Dictionary<int, HashSet<int>> rules)
    {
        var graph = update.ToDictionary(page => page, _ => new List<int>());

        foreach (var (x, ySet) in rules)
        foreach (var y in ySet)
            if (update.Contains(x) && update.Contains(y))
                graph[x].Add(y);

        return graph;
    }

    private static List<int> TopologicalSort(IEnumerable<int> nodes, Dictionary<int, List<int>> graph)
    {
        var sorted = new List<int>();
        var visited = new HashSet<int>();
        var tempMarked = new HashSet<int>();

        foreach (var node in nodes) Visit(node);

        void Visit(int node)
        {
            if (tempMarked.Contains(node))
                throw new InvalidOperationException("Graph has a cycle.");

            if (visited.Contains(node)) return;

            tempMarked.Add(node);
            foreach (var neighbor in graph[node]) Visit(neighbor);
            tempMarked.Remove(node);
            visited.Add(node);
            sorted.Add(node);
        }

        return sorted;
    }
}