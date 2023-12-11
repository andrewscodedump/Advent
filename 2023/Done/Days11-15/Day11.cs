namespace Advent2023;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        long totalDistance = 0;
        List<int> emptyRows = [], emptyCols = [];
        List<(int, int)> galaxies = [];
        int expansionFactor = Part1 ? 2 : TestMode ? 100 : 1_000_000;
        for (int i = 0; i < Inputs.Length; i++)
            if (!Inputs[i].Contains('#'))
                emptyRows.Add(i);

        for (int x = 0; x < Inputs[0].Length; x++)
        {
            bool emptyCol = true;
            int realX = x + (emptyCols.Count * (expansionFactor - 1)), realY = 0;
            for (int y = 0; y < Inputs.Length; y++)
            {
                realY = y + (emptyRows.Count(e => e <= y) * (expansionFactor - 1));
                if (Inputs[y][x] == '#')
                {
                    emptyCol = false;
                    galaxies.Add((realX, realY));
                }
            }
            if(emptyCol) emptyCols.Add(x);
        }

        foreach ((int x1, int y1) in galaxies)
            foreach ((int x2, int y2) in galaxies)
                totalDistance += Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        Output = (totalDistance / 2).ToString();
    }
}
