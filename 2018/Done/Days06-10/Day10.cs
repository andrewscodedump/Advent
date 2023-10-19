namespace Advent2018;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        List<(int x, int y, int vx, int vy)> points = new();
        string output = "None found";
        int seconds = 0;

        foreach (string state in InputSplit)
        {
            string[] bits = state.Split(new char[] { '=', '<', '>', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            points.Add((int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[4]), int.Parse(bits[5])));
        }
        #endregion Setup Variables and Parse Inputs

        do
        {
            seconds++;
            (int minx, int miny, int maxx, int maxy) limits = (int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = (points[i].x + points[i].vx, points[i].y + points[i].vy, points[i].vx, points[i].vy);
                limits.minx = Math.Min(points[i].x, limits.minx); limits.miny = Math.Min(points[i].y, limits.miny);
                limits.maxx = Math.Max(points[i].x, limits.maxx); limits.maxy = Math.Max(points[i].y, limits.maxy);
            }
            if (limits.maxx - limits.minx > 100 || limits.maxy - limits.miny > 100) continue;
            if (CheckForLines(points, limits))
            {
                output = BatchRun ? Expected : AWInputBox("Check output window and enter letters (if any)", "Is it OK?", "None found");
            }
        } while (output == "None found");

        Output = Part1 ? output : seconds.ToString();
    }

    #region Private Classes and Methods
    private static bool CheckForLines(List<(int x, int y, int vx, int vy)> points, (int minx, int miny, int maxx, int maxy) limits)
    {
        bool[,] array = new bool[limits.maxx - limits.minx + 1, limits.maxy - limits.miny + 1];
        foreach ((int x, int y, int _, int _) in points)
            array[x - limits.minx, y - limits.miny] = true;
        for (int x = 0; x < array.GetLength(0); x++)
        {
            int len = 0;
            for (int y = 0; y < array.GetLength(1); y++)
                if (!array[x, y]) break;
                else len++;
            if (len >= 8)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    StringBuilder line = new();
                    for (int x2 = 0; x2 < array.GetLength(0); x2++)
                        line.Append(array[x2, y] ? "*" : " ");
                    Debug.WriteLine(line);
                }
                return true;
            }
        }
        return false;
    }
    #endregion Private Classes and Methods
}
