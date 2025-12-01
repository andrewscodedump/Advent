namespace Advent2019;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        for (int y = 0; y < Inputs.Length; y++)
        {
            string work = Inputs[y];
            for (int x = 0; x < work.Length; x++)
            {
                SimpleMap[(x, y)] = work[x];
            }
        }
        DrawMap();
        AnalyzeMap();

        #endregion Setup Variables and Parse Inputs

        Output = "OutputVariable".ToString();
    }

    #region Private Classes and Methods

    private void AnalyzeMap()
    {
        Dictionary<(int, int), char> work = new(SimpleMap);
        // Loop round map
        int maxX = work.Keys.Max(x => x.Item1), maxY = work.Keys.Max(x => x.Item2);
        int minX = work.Keys.Min(x => x.Item1);
        int gapMinX = 0, gapMinY = 0, gapMaxX = 0, gapMaxY = 0;
        (int, int) startPos, endPos, pos = (0, 0);
        string pair = string.Empty;

        for (int y = 0; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                // If letter
                if (".# ".Contains(work[(x, y)])) continue;
                // Check neighbours
                // if up=. down =letter
                if (y == 0 || y == gapMaxY - 1)
                {
                    // vertical, down
                    pos = (x, y + 2);
                    pair = work[(x, y)].ToString() + work[(x, y + 1)].ToString();
                }
                if (y == maxY - 1 || y == gapMinY)
                {
                    // vertical, up
                }
                if (x == 0 || x == gapMaxX - 1)
                {
                    // horizontal, right
                }
                if (x == maxX - 1 || x == gapMinX)
                {
                    // horizontal, left
                }
                // If AA or ZZ , set start/end point
                if (pair == "AA") startPos = pos;
                if (pair == "ZZ") endPos = pos;
                // If already exists, reverse
                // Check for reverse
                // Add to dict
                // Clear map
            }
        }
    }

    #endregion Private Classes and Methods

}
