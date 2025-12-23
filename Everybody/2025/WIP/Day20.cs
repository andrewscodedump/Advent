namespace Everybody2025;

public class Day20 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('.', out int width, out int height);
        (int, int) start = SimpleMap.Keys.FirstOrDefault(k => SimpleMap[k] == 'S');
        (int, int) end = SimpleMap.Keys.FirstOrDefault(k => SimpleMap[k] == 'E');
        List<((int, int), (int, int))> pairs;
        Dictionary<(int, int), char>[] maps = [SimpleMap, Rotate(SimpleMap), []];
        maps[2] = Rotate(maps[1]);
        switch (WhichPart)
        {
            case 1:
                Output = (GetPairs(height, width).Count / 2).ToString();
                break;
            case 2:
                int best = int.MaxValue;
                Dictionary<(int, int), int> visited = [];
                pairs = GetPairs(height, width);
                Queue<((int, int), int)> q = [];
                q.Enqueue((start, 0));
                do
                {
                    ((int x, int y), int jumps)=q.Dequeue();
                    if (jumps >= best) continue;
                    if ((x, y) == end)
                    {
                        best=Math.Min(best, jumps);
                        continue;
                    }
                    if (!visited.TryGetValue((x, y), out int prev))
                        visited[(x, y)] = jumps;
                    else
                    {
                        if (prev <= jumps) continue;
                        visited[(x, y)] = jumps;
                    }
                    foreach (((int, int) from, (int, int) to) in pairs.Where(p => p.Item1 == (x, y)))
                    {
                        q.Enqueue((to, jumps + 1));
                    }
                } while (q.Count > 0);
                Output = best.ToString();
                break;
            case 3:
                Output = "OutputVariable".ToString();
                break;
        }
    }

    private List<((int, int), (int, int))> GetPairs(int height, int width)
    {
        List<((int, int), (int, int))> pairs = [];
        char[] trampolines = ['S', 'T', 'E'];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (!trampolines.Contains(SimpleMap[(x, y)])) continue;
                if (y % 2 != x % 2 && trampolines.Contains(SimpleMap[(x, y + 1)]))
                {
                    pairs.Add(((x, y), (x, y + 1)));
                    pairs.Add(((x, y + 1), (x, y)));
                }
                if (trampolines.Contains(SimpleMap[(x + 1, y)]))
                {
                    pairs.Add(((x, y), (x + 1, y)));
                    pairs.Add(((x + 1, y), (x, y)));
                }
            }
        return pairs;
    }

    private List<((int, int), (int, int))> GetTriplets(int height, int width)
    {
        // For part 3, need to extend to include which maps we're jumping between (I think)
        List<((int, int), (int, int))> pairs = [];
        char[] trampolines = ['S', 'T', 'E'];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (!trampolines.Contains(SimpleMap[(x, y)])) continue;
                if (y % 2 != x % 2 && trampolines.Contains(SimpleMap[(x, y + 1)]))
                {
                    pairs.Add(((x, y), (x, y + 1)));
                    pairs.Add(((x, y + 1), (x, y)));
                }
                if (trampolines.Contains(SimpleMap[(x + 1, y)]))
                {
                    pairs.Add(((x, y), (x + 1, y)));
                    pairs.Add(((x + 1, y), (x, y)));
                }
            }
        return pairs;
    }

    Dictionary<(int, int), char> Rotate(Dictionary<(int, int), char> original)
    {
        Dictionary<(int, int), char> result = [];
        // Start from 0,0, move down diagonal LDLDLDLD..., moving to row 0 in target (backwards); (width-1),0 - 0,0
        // Move to 2,0 & repeat, copying to row 1; (width-2),1 - 1,1
        // ...
        // Ending on (width-1),0, which will be a single cell, copied to (height-1),width/2.
        return result;
    }
}

