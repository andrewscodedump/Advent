namespace Everybody2025;

public class Day17 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput();
        (int, int) volcano = SimpleMap.First(k => k.Value == '@').Key;
        SimpleMap[volcano] = '0';
        switch (WhichPart)
        {
            case 1:
                Output = SimpleMap.Where(p => InDanger(p.Key, volcano, 10)).Sum(p => p.Value - 48).ToString();
                break;
            case 2:
                (int radius, int effect) maxEffect = (0, 0);
                for (int r = 0; r < (Inputs[0].Length - 1) / 2; r++)
                {
                    int total = 0;
                    foreach ((int, int) target in SimpleMap.Keys)
                    {
                        if (InDanger(target, volcano, r))
                        {
                            total += SimpleMap[target] - 48;
                            SimpleMap[target] = '0';
                        }
                    }
                    if (total > maxEffect.effect)
                        maxEffect = (r, total);
                }
                Output = (maxEffect.radius * maxEffect.effect).ToString();
                break;
            case 3:
                (int, int) start = SimpleMap.First(k => k.Value == 'S').Key;
                int radius = 0, result = 0;
                do
                {
                    result = TryLoop(start, volcano, radius);
                } while (result != 0);
                Output = (result * radius).ToString();
                break;
        }
    }

    private static bool InDanger((int x, int y) t, (int x, int y) v, int r) => ((v.x - t.x) * (v.x - t.x)) + ((v.y - t.y) * (v.y - t.y)) <= r * r;

    private int TryLoop((int, int) start, (int, int) volcano, int radius)
    {
        Queue<((int, int), int)> q = [];
        q.Enqueue((start, 0));
        bool completed = false;
        Dictionary<(int, int), int> visited = new() {{ start, 0} };
        do
        {
            if (IsEnclosed(volcano, [.. visited.Keys]))
            {

                // Favour dl, dr, ur, ul in the tl, bl, br, tr quadrants

            }
        } while (q.Count > 0);
        return completed ? visited.Keys.Count : 0;
    }

    private bool IsEnclosed((int x, int y) volcano, (int, int)[] chain)
    {
        // Assumes that the loop is closed (although there are some weird scenarios this won't catch)
        int result = 0;
        foreach ((int dx, int dy) in DirectNeighbours)
        {
            for (int i = 1; i < (Inputs[0].Length - 1) / 2; i++)
            {
                if (chain.Contains((volcano.x + (dx * i), volcano.y + (dy * i))))
                {
                    result++;
                    break;
                }
            }
        }
        return result == 4;
    }
}
