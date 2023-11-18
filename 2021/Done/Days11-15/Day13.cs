namespace Advent2021;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        foreach (string line in Inputs)
        {
            if (line.Length == 0) continue;
            if (!line.StartsWith("fold"))
                SimpleMap[(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1]))] = '█';
            else
            {
                Dictionary<(int x, int y), char> afterFold = new(SimpleMap);
                char direction = line[11];
                int fold = int.Parse(line[13..]);
                foreach ((int x, int y) in SimpleMap.Keys)
                    if (direction == 'y' && y > fold)
                    {
                        afterFold[(x, (2 * fold) - y)] = '█';
                        afterFold.Remove((x, y));
                    }
                    else if (direction == 'x' && x > fold)
                    {
                        afterFold[((2 * fold) - x, y)] = '█';
                        afterFold.Remove((x, y));
                    }
                SimpleMap = new(afterFold);
                if (Part1) break;
            }
        }
        if (Part2) DrawMap(false, false);

        Output = Part1 ? SimpleMap.Count.ToString() : "Result is in debug window";
    }
}
