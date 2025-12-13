namespace Advent2024;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int counter = 0;
        Stack<((int, int), (int, int), int)> q = new();
        HashSet<((int, int), (int, int))> visited = [];
        PopulateMapFromInputWithBorders('.');

        foreach ((int, int) pos in SimpleMap.Keys.Where(k => SimpleMap[k] == '0')) q.Push((pos, pos, '0'));
        do
        {
            ((int x, int y) start, (int x, int y), int count) = q.Pop();
            foreach ((int dx, int dy) in DirectNeighbours)
            {
                (int, int) nextPos = (x + dx, y + dy);
                int nextVal = SimpleMap[nextPos];
                if (nextVal != count + 1) continue;
                if (Part1 && !visited.Add((start, nextPos))) continue;
                if (nextVal == '9') { counter++; continue; }
                q.Push((start, nextPos, nextVal));
            }
        } while (q.Count > 0);

        Output = counter.ToString();
    }
}
