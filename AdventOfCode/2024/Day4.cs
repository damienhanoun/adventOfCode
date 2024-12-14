namespace AdventOfCode._2024;

public class Day4 : Day
{
    protected override void Solve(string input)
    {
        var grid = ParseInputToGrid(input);

        var part1Count = CountWordOccurrences(grid, "XMAS");
        Answer(part1Count.ToString());

        var part2Count = CountXMasPatterns(grid);
        Answer(part2Count.ToString());
    }

    private static char[][] ParseInputToGrid(string input)
    {
        return input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.ToCharArray())
            .ToArray();
    }

    private static int CountWordOccurrences(char[][] grid, string word)
    {
        var directions = GetSearchDirections();
        var count = 0;

        for (var row = 0; row < grid.Length; row++)
        for (var col = 0; col < grid[0].Length; col++)
            count += directions.Count(dir => IsWordFound(grid, word, row, col, dir.rowDelta, dir.colDelta));

        return count;
    }

    private static (int rowDelta, int colDelta)[] GetSearchDirections()
    {
        return new[]
        {
            (0, 1), (0, -1), (1, 0), (-1, 0),
            (1, 1), (-1, -1), (1, -1), (-1, 1)
        };
    }

    private static bool IsWordFound(char[][] grid, string word, int startRow, int startCol, int rowDelta, int colDelta)
    {
        for (var i = 0; i < word.Length; i++)
        {
            var newRow = startRow + i * rowDelta;
            var newCol = startCol + i * colDelta;

            if (IsOutOfBounds(grid, newRow, newCol) || grid[newRow][newCol] != word[i])
                return false;
        }

        return true;
    }

    private static bool IsOutOfBounds(char[][] grid, int row, int col)
    {
        return row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length;
    }

    private static int CountXMasPatterns(char[][] grid)
    {
        var count = 0;

        for (var row = 1; row < grid.Length - 1; row++)
        for (var col = 1; col < grid[0].Length - 1; col++)
            if (IsXMasPattern(grid, row, col))
                count++;

        return count;
    }

    private static bool IsXMasPattern(char[][] grid, int centerRow, int centerCol)
    {
        var validMas = new[] { "MAS", "SAM" };

        var topLeftToBottomRight = GetDiagonal(grid, centerRow, centerCol, -1, -1, 1, 1);
        var topRightToBottomLeft = GetDiagonal(grid, centerRow, centerCol, -1, 1, 1, -1);

        return validMas.Contains(topLeftToBottomRight) && validMas.Contains(topRightToBottomLeft);
    }

    private static string GetDiagonal(char[][] grid, int centerRow, int centerCol, int startRowDelta, int startColDelta, int endRowDelta, int endColDelta)
    {
        return $"{grid[centerRow + startRowDelta][centerCol + startColDelta]}" +
               $"{grid[centerRow][centerCol]}" +
               $"{grid[centerRow + endRowDelta][centerCol + endColDelta]}";
    }
}