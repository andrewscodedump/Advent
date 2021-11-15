namespace Advent2020;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int depth = InputSplit.Length, width = InputSplit[0].Length, result = 1;
        Dictionary<(int, int), char> map = new();
        List<(int, int)> slopes = WhichPart == 1 ? new() { (3, 1) } : new() { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

        for (int i = 0; i < depth; i++)
            for (int j = 0; j < width; j++)
                map.Add((j, i), InputSplit[i][j]);

        foreach ((int dx, int dy) in slopes)
        {
            int trees = 0, x = 0;
            for (int y = 0; y < depth; y += dy)
            {
                trees += map[(x, y)] == '#' ? 1 : 0;
                x = (x + dx) % width;
            }
            result *= trees;
        }
        Output = result.ToString();
    }
}
