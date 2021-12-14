namespace Advent2021;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        foreach (string line in InputSplit)
        {
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
                if (WhichPart == 1) break;
            }
        }
        if (WhichPart==2) DrawMap(false, false);

        Output = WhichPart == 1 ? SimpleMap.Count.ToString() : "Result is in debug window";
    }
}
