namespace Everybody2025;

public class Day12 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput(out int width, out int height);
        if (WhichPart == 1 || WhichPart == 2)
            SimpleMap = ExplodeNew((0, 0), SimpleMap);
        if (WhichPart == 2)
            SimpleMap = ExplodeNew((width - 1, height - 1), SimpleMap);
        if (WhichPart == 3)
            for (int round = 1; round <= 3; round++)
                SimpleMap = SimpleMap.Keys.Select(c => ExplodeNew(c, SimpleMap)).MinBy(x => x.Count);
        Output = ((width * height) - SimpleMap.Count).ToString();
    }

    private Dictionary<(int, int), char> ExplodeNew((int, int) start, Dictionary<(int, int), char> mapAtStart)
    {
        Dictionary<(int, int), char> newMap = new(mapAtStart);
        Queue<((int, int), char)> q = new([(start, newMap[start])]);
        do
        {
            ((int x, int y), char val) = q.Dequeue();
            if(!newMap.Remove((x, y))) continue;

            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int, int) newPos = (x + dx, y + dy);
                if(!newMap.TryGetValue(newPos, out char newVal) || newVal > val) continue;
                q.Enqueue((newPos, newVal));
            }
        } while (q.Count > 0);
        return newMap;
    }
}