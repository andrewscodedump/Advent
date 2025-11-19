namespace Everybody2025;

public class Day12 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('a', out int width, out int height);
        HashSet<(int, int)> destroyed = [];
        switch (WhichPart)
        {
            case 1:
                Output = GetCount((0, 0), destroyed).Count.ToString();
                break;
            case 2:
                Output = GetCount((0, 0), (width - 1, height - 1), destroyed).Count.ToString();
                break;
            case 3:
                HashSet<(int, int)> mostDestroyed = [];
                for (int round = 1; round <= 3; round++)
                {
                    int bestCount = 0;
                    for(int x = 0; x < width; x++)
                    {
                        for(int y = 0; y < height; y++)
                        {
                            HashSet<(int, int)> testDestroyed = GetCount((x, y), destroyed);
                            if(testDestroyed.Count > bestCount)
                            {
                                bestCount = testDestroyed.Count;
                                mostDestroyed = [.. testDestroyed];
                            }
                        }
                    }
                    destroyed = mostDestroyed;
                }
                Output = destroyed.Count.ToString();

                break;
        }
    }
    private HashSet<(int, int)> GetCount((int, int) start, HashSet<(int, int)> destroyedin) => GetCount(start, (-1, -1), destroyedin);

    private HashSet<(int, int)> GetCount((int, int) start, (int, int) start2, HashSet<(int, int)> destroyedin)
    {
        HashSet<(int, int)> destroyed = [.. destroyedin];
        if (destroyed.Contains(start)) return destroyed;
        Queue<((int, int), char)> q = new([(start, SimpleMap[start])]);
        if (start2.Item1 != -1) q.Enqueue((start2, SimpleMap[start2]));
        do
        {
            ((int x, int y), char val) = q.Dequeue();
            if (destroyed.Contains((x, y))) continue;
            destroyed.Add((x, y));

            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int, int) newPos = (x + dx, y + dy);
                if (destroyed.Contains(newPos) || SimpleMap[newPos] == 'a') continue;
                if (SimpleMap[newPos] <= val) q.Enqueue((newPos, SimpleMap[newPos]));
            }
        } while (q.Count > 0);
        return destroyed;
    }
}