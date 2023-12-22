namespace Advent2023;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        if(Part1)
            PopulateMapFromInputWithBorders('#',out int width, out int height);
        else
            PopulateMapFromInput(out int width, out int height);

        int stepGoal = TestMode ? Part1 ? 6 : 5000 : Part1 ? 64 : 26501365;
        // 26501365 = 5 x 11 x 481843
        (int x, int y) start = SimpleMap.Keys.First(k => SimpleMap[k] == 'S');
        SimpleMap[start] = '.';
        Queue<((int, int), int)> queue = [];
        HashSet<((int, int) pos, int steps)> reachable = [];
        queue.Enqueue((start, 0));
        do
        {
            ((int x, int y), int steps) = queue.Dequeue();
            steps++;
            foreach((int dx, int dy) in DirectNeighbours)
            {
                (int, int) newPos = (x + dx, y + dy);
                if (SimpleMap[newPos] == '.')
                {
                    if (reachable.Contains((newPos, steps))) continue;
                    reachable.Add((newPos, steps));
                    if (steps == stepGoal) continue;
                    queue.Enqueue((newPos, steps));
                }
            }
        } while (queue.Count > 0);
        
        Output = reachable.Where(r => r.steps == stepGoal).Count().ToString();
    }
}
