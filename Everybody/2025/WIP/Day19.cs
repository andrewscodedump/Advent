namespace Everybody2025;

public class Day19 : Advent.Day
{
    public override void DoWork()
    {
        long maxGap = InputNumbers.Max(t => t[1] + t[2] - 1), exit = InputNumbers.Max(t => t[0]);
        HashSet<(int, int)> walls = [], gaps = [];
        foreach (long[] triplet in InputNumbers)
        {
            (int x, int w, int g) = ((int)triplet[0], (int)triplet[1], (int)triplet[2]);
            for (int i = 0; i < w; i++)
                if(!gaps.Contains((x,i)))
                    walls.Add((x, i));
            for (int i = w; i < w + g; i++)
            {
                gaps.Add((x, i));
                walls.Remove((x, i));
            }
            for (int i = w + g; i <= maxGap + 1; i++)
                if (!gaps.Contains((x, i)))
                    walls.Add((x, i));
        }
        switch (WhichPart)
        {
            case 1:
            case 2:
                Dictionary<(int, int), int> visited = [];
                int bestRun = int.MaxValue;
                Queue<((int, int), bool, int)> q = [];
                q.Enqueue(((0, 0), true, 0));
                do
                {
                    ((int x, int y), bool flap, int flaps) = q.Dequeue();
                    (int nx, int ny) = (x + 1, y + (flap ? 1 : -1));
                    flaps += flap ? 1 : 0;
                    if (walls.Contains((nx, ny)) || ny > maxGap || ny < 0) continue;
                    if (visited.TryGetValue((nx, ny), out int oldVal))
                    {
                        if (flaps >= oldVal) continue;
                        else visited[(nx, ny)] = flaps;
                    }
                    else
                        visited[(nx, ny)] = flaps;
                    if (flaps >= bestRun) continue;
                    if (nx == exit)
                    {
                        bestRun = Math.Min(flaps, bestRun);
                        continue;
                    }
                    q.Enqueue(((nx, ny), true, flaps));
                    q.Enqueue(((nx, ny), false, flaps));

                } while (q.Count > 0);
                Output = bestRun.ToString();
                break;
            case 3:
                Output = "OutputVariable".ToString();
                break;
        }
    }

}
