namespace Advent2024;

public partial class Day21 : Advent.Day
{
    Dictionary<(int, int), char> numericPad = [];
    Dictionary<(int, int), char> directionalPad = [];
    (int, int) numericStart = (2, 3);
    (int, int) directionalStart = (2, 0);
    readonly Dictionary<(int, int), char> directionalKeys = new() { { (0, 0), 'A' }, { (0, -1), '^' }, { (1, 0), '>' }, { (0, 1), 'v' }, { (-1, 0), '<' } };

    public override void DoWork()
    {
        long result = 0;
        PopulateMapWithBorders(["789", "456", "123", "X0A"], 'X');
        numericPad = new(SimpleMap);

        PopulateMapWithBorders(["X^A", "<v>"], 'X');
        directionalPad = new(SimpleMap);

        foreach(string code in Inputs)
        {
            result += Solve(code);
        }

        Output = result.ToString();
    }

    private long Solve(string code)
    {
        long result = long.Parse(code[..3].ToString());
        string[] robot1 = Generate(code, false);
        string[] robot2 = Generate(robot1);
        string[] robot3 = Generate(robot2);

        return result * robot3[0].Length;
    }

    private string[] Generate(string[] directions)
    {
        string[] results = [];
        foreach (string direction in directions)
        {
            string[] latest=Generate(direction, true);
            if (results.Length == 0 || latest[0].Length < results[0].Length)
                results = [.. latest];
            else
                results = [.. results, .. latest];
        }
        return results;
    }

    private string[] Generate(string directions, bool directional)
    {
        (int, int) start = directional ? directionalStart : numericStart;
        Dictionary<(int, int), char> keyPad = directional ? directionalPad : numericPad;
        string[] results = [];
        Queue<((int, int), string, int, HashSet<(int, int)>)> q = new([(start, "", 0, [start])]);
        int[] shortest = Enumerable.Repeat(int.MaxValue, directions.Length).ToArray();
        do
        {
            ((int x, int y) pos, string path, int target, HashSet<(int, int)> visited) = q.Dequeue();
            if (keyPad[pos] == directions[target])
            {
                if(!path.EndsWith('A'))
                    path = $"{path}A";
                if (path.Length > shortest[target]) continue;
                shortest[target] = path.Length;
                visited = [];
                target++;
            }

            if (target == directions.Length)
            {
                if (results.Length == 0 || path.Length < results[0].Length)
                    results = [path];
                else
                    results = [.. results, path];
                continue;
            }

            foreach ((int x, int y) key in directionalKeys.Keys)
            {
                (int x, int y) newPos = (pos.x + key.x, pos.y + key.y);
                if (keyPad[newPos] == 'X' || !visited.Add(newPos)) continue;
                string newPath = path + directionalKeys[key];
                if (newPath.Length > shortest[target]) continue;
                q.Enqueue((newPos, newPath, target, new(visited)));
            }
        } while (q.Count > 0);

        return results;
    }
}
