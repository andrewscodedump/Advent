namespace Advent2022;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        int width = Inputs[0].Length, height = Inputs.Length;
        Dictionary<(int, int), TreeStats> trees = new();
        for (int y = 0; y < height; y++)
        {
            string line = Inputs[y];
            for (int x = 0; x < width; x++)
            {
                trees[(x, y)] = new TreeStats(Inputs[x][y] - '0');
            }
        }

        foreach ((int x, int y) in trees.Keys)
            GetStats(x, y, height, width, trees);

        int visibleTrees = trees.Where(t => t.Value.Visible).Count();
        int maxScore = trees.Max(t => t.Value.Score);
        Output = (Part1 ? visibleTrees : maxScore).ToString();
    }
    private class TreeStats
    {
        public TreeStats(int height) => Height = height;

        public int Height { get; set; }
        public bool Visible { get; set; }
        public int Score { get; set; }
    }

    private void GetStats(int x, int y, int height, int width, Dictionary<(int, int), TreeStats> trees)
    {
        int up = 0, down = 0, left = 0, right = 0;
        int baseSize = trees[(x,y)].Height;
        bool visible = x == 0 || y == 0 || x == width - 1 || y == height - 1;
        //up
        for (int i = y - 1; i >= 0; i--)
        {
            up++;
            if (trees[(x, i)].Height >= baseSize) break;
            if (i == 0) visible = true;
        }
        //down
        for (int i = y + 1; i < height; i++)
        {
            down++;
            if (trees[(x, i)].Height >= baseSize) break;
            if (i == height - 1) visible = true;
        }
        //left
        for (int i = x - 1; i >= 0; i--)
        {
            left++;
            if (trees[(i, y)].Height >= baseSize) break;
            if (i == 0) visible = true;
        }
        //right
        for (int i = x + 1; i < width; i++)
        {
            right++;
            if (trees[(i, y)].Height >= baseSize) break;
            if (i == width - 1) visible = true;
        }
        trees[(x, y)].Score = up * down * left * right;
        trees[(x, y)].Visible = visible;
    }
}
