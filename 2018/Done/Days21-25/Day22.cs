namespace Advent2018;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        long depth = InputNumbers[0][0];
        (long x, long y) target = (InputNumbers[1][0], InputNumbers[1][1]);
        Dictionary<(int x, int y), (long gi, long el, string type, int bestTime)> map = new();
        long gi, el, typeScore, totalScore = 0, bestTime = (target.x * 8) + (target.y * 8) + 7;
        string type;

        for (int y = 0; y < target.y * 2; y++)
        {
            for (int x = 0; x < target.x * 10; x++)
            {
                gi = (x, y) == (0, 0) || (x, y) == target ? 0 : x == 0 ? y * 48271 : y == 0 ? x * 16807 : map[(x - 1, y)].el * map[(x, y - 1)].el;
                el = (gi + depth) % 20183;

                typeScore = el % 3;
                type = typeScore == 0 ? "rocky" : el % 3 == 1 ? "wet" : "narrow";
                if (x <= target.x && y <= target.y) totalScore += typeScore;
                map.Add((x, y), (gi, el, type, int.MaxValue));
            }
        }
        #endregion Setup Variables and Parse Inputs

        Queue<((int, int) pos, int time, string kit)> bfs = new();
        Dictionary<((int, int), string), int> alreadyBeen = new();
        if (Part2) bfs.Enqueue(((0, 0), 0, "Torch"));

        while (bfs.Count > 0)
        {
            ((int x, int y) pos, int time, string kit) = bfs.Dequeue();
            if (!alreadyBeen.ContainsKey((pos, kit)))
                alreadyBeen.Add((pos, kit), time);
            else if (alreadyBeen[(pos, kit)] <= time) continue;
            else alreadyBeen[(pos, kit)] = time;

            // Check time
            if (time > bestTime) continue;
            // Check target
            if (pos == target)
            {
                if (kit != "Torch") time += 7;
                bestTime = Math.Min(time, bestTime);
                continue;
            }
            string currentType = map[pos].type;
            foreach ((int x, int y) in DirectionsYDown.Values)
            {
                (int x, int y) newPos = (pos.x + x, pos.y + y);
                if (newPos.x < 0 || newPos.y < 0) continue;
                if (!map.ContainsKey(newPos))
                {
                    // Initially, just ignore.  Later, add
                    continue;
                }
                switch (map[newPos].type)
                {
                    case "rocky":
                        if (currentType != "wet")
                            if (!alreadyBeen.ContainsKey((newPos, "Torch")) || alreadyBeen[(newPos, "Torch")] > time + (kit == "Torch" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "Torch" ? 1 : 8), "Torch"));
                        if (currentType != "narrow")
                            if (!alreadyBeen.ContainsKey((newPos, "Climbing")) || alreadyBeen[(newPos, "Climbing")] > time + (kit == "Climbing" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "Climbing" ? 1 : 8), "Climbing"));
                        break;
                    case "wet":
                        if (currentType != "narrow")
                            if (!alreadyBeen.ContainsKey((newPos, "Climbing")) || alreadyBeen[(newPos, "Climbing")] > time + (kit == "Climbing" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "Climbing" ? 1 : 8), "Climbing"));
                        if (currentType != "rocky")
                            if (!alreadyBeen.ContainsKey((newPos, "")) || alreadyBeen[(newPos, "")] > time + (kit == "" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "" ? 1 : 8), ""));
                        break;
                    case "narrow":
                        if (currentType != "wet")
                            if (!alreadyBeen.ContainsKey((newPos, "Torch")) || alreadyBeen[(newPos, "Torch")] > time + (kit == "Torch" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "Torch" ? 1 : 8), "Torch"));
                        if (currentType != "rocky")
                            if (!alreadyBeen.ContainsKey((newPos, "")) || alreadyBeen[(newPos, "")] > time + (kit == "" ? 1 : 8))
                                bfs.Enqueue((newPos, time + (kit == "" ? 1 : 8), ""));
                        break;
                    default:
                        break;
                }
            }

        }
        Output = (Part1 ? totalScore : bestTime).ToString();
    }
}
