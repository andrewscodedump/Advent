namespace Advent2024;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        string[] inputs = Inputs.Where(l => l.StartsWith('#')).ToArray();
        if (Part2) inputs = inputs.Select(l => l.Replace("#", "##").Replace("O", "[]").Replace("*", "**").Replace(".", "..").Replace("@", "@.")).ToArray();
        PopulateMap(inputs);
        (int x, int y) robot = SimpleMap.Keys.First(k => SimpleMap[k] == '@');
        foreach (char move in string.Join("", Inputs.Where(l => !l.StartsWith('#'))))
        {
            HashSet<(int x, int y)> cratesToMove = [];
            if (CheckMove([robot], move, ref cratesToMove)) Move(ref robot, move, cratesToMove);
        }
        Output = SimpleMap.Where(k => k.Value == 'O' || k.Value == '[').Sum(k => k.Key.Item1 + (100 * k.Key.Item2)).ToString();
    }

    private bool CheckMove(HashSet<(int, int)> locations, char move, ref HashSet<(int x, int y)> crates)
    {
        (int dx, int dy) = DirectionsYDown[move];
        HashSet<(int, int)> nextSet = [];
        foreach ((int x, int y) in locations)
        {
            crates.Add((x, y));
            bool isVert = Math.Abs(dx) == 0, isCrate = SimpleMap[(x, y)] == '[';
            (int x, int y) nextPos = (x + dx + (move == '>' && isCrate ? 1 : 0), y + dy), nextPosR = (isCrate && isVert) ? (x + dx + 1, y + dy) : nextPos;
            char nextChar = SimpleMap[nextPos], nextCharR = SimpleMap[nextPosR];
            if (nextChar == '#' || nextCharR == '#') return false;
            if (nextChar == '.' && nextCharR == '.') continue;

            if (nextChar == '[' || nextChar == 'O') nextSet.Add(nextPos);
            if (nextCharR == '[') nextSet.Add(nextPosR);
            if (nextChar == ']') nextSet.Add((nextPos.x - 1, nextPos.y));
        }
        if (nextSet.Count == 0) return true;
        return CheckMove(nextSet, move, ref crates);
    }

    private void Move(ref (int x, int y) robot, char move, HashSet<(int x, int y)> crates)
    {
        (int dx, int dy) = DirectionsYDown[move];
        foreach((int x, int y) in crates.Reverse())
        {
            bool isCrate = SimpleMap[(x, y)] == '[';
            SimpleMap[(x + dx, y + dy)] = SimpleMap[(x, y)];
            SimpleMap[(x, y)] = '.';
            if (isCrate)
            {
                SimpleMap[(x + dx + 1, y + dy)] = ']';
                if (move != '>') SimpleMap[(x + 1, y)] = '.';
            }
        }
        robot = (robot.x + dx, robot.y + dy);
    }
}
