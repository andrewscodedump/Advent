namespace Everybody2025;

public class Day15 : Advent.Day
{
    private readonly HashSet<((int x, int y), (int x, int y))> walls = [];
    private (int x, int y) start = (0, 0), end = (0, 0);
    int minX, maxX, minY, maxY;

    public override void DoWork()
    {
        DrawWall();
        Output = FindRoute().ToString();
    }

    public void DrawWall()
    {
        walls.Clear();
        int x=0, y = 0;

        (int x, int y) dirn = (0, 0);
        foreach (string instruction in Input.Split(","))
        {
            if (dirn == (0, 0)) dirn = DirectionsYDown[instruction[0]];
            else dirn = changeDirection[(dirn, instruction[0])];
            int len = int.Parse(instruction[1..]);
            int endx = x + (dirn.x * len);
            int endy = y + (dirn.y * len);
            walls.Add(((x, y), (endx, endy)));
            (x, y) = (endx, endy);
        }
        end = (x, y);
        minX = Math.Min(walls.Min(p => p.Item1.x), walls.Min(p => p.Item2.x));
        maxX = Math.Max(walls.Max(p => p.Item1.x), walls.Max(p => p.Item2.x));
        minY = Math.Min(walls.Min(p => p.Item1.y), walls.Min(p => p.Item2.y));
        maxY = Math.Max(walls.Max(p => p.Item1.y), walls.Max(p => p.Item2.y));
    }

    private bool IsWall((int x, int y) pos)
    {
        if (pos == end) return false;
        foreach (((int sx, int sy), (int ex, int ey)) in walls)
        {
            bool samex = pos.x == sx && pos.x == ex, samey = pos.y == sy && pos.y == ey;
            if (!samex & !samey) continue;
            if ((samex && pos.y <= Math.Max(sy, ey) && pos.y >= Math.Min(sy, ey))
            || (samey && pos.x <= Math.Max(sx, ex) && pos.x >= Math.Min(sx, ex)))
                return true;
        }
        return false;
    }

    private int FindRoute()
    {
        Queue<((int, int), int)> q = new();
        q.Enqueue((start, 0));
        Dictionary<(int, int), int> visited = new() { { end, int.MaxValue } };
        do
        {
            ((int x, int y), int len) = q.Dequeue();
            if (len >= visited[end]) continue;
            if(visited.TryGetValue((x,y), out int bestLen))
            {
                if (bestLen <= len) continue;
                else visited[(x, y)] = len;
            }
            else
            {
                visited[(x, y)] = len;
            }
            if (len >= visited[end]) continue;
            if ((x, y) == end) continue;
            foreach((int dx, int dy) in DirectNeighbours)
            {
                (int nx, int ny) = (x + dx, y + dy);
                if (nx < minX || nx > maxX || ny < minY || ny > maxY || IsWall((nx, ny))) continue;
                if (visited.TryGetValue((nx, ny), out bestLen) && bestLen <= len + 1) continue;
                q.Enqueue(((nx, ny), len + 1));
            }

        } while(q.Count > 0);
        return visited[end];
    }
}