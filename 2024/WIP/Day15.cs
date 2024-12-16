namespace Advent2024;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        string[] inputs = Inputs.Where(l => l.StartsWith('#')).ToArray();
        if (Part2) inputs = inputs.Select(l => l.Replace("#", "##").Replace("O", "[]").Replace("*", "**").Replace(".", "..").Replace("@", "@.")).ToArray();
        PopulateMap(inputs);
        string moves = string.Join("", Inputs.Where(l => !l.StartsWith('#')));
        (int x, int y) robot = SimpleMap.Keys.First(k => SimpleMap[k] == '@');
        foreach (char move in moves)
        {
            HashSet<(int x, int y)> crates = [];
            if (Part1 || (Part2 && (move == '<' || move == '>')))
            {
                if (CheckMoveSimple(robot, move, out (int, int) spacePos)) MoveSimple(ref robot, move, spacePos);
            }
            else if (CheckMove([robot], move, ref crates)) Move(ref robot, move, crates);
        }

        long result = SimpleMap.Keys.Where(k => SimpleMap[k] == 'O' || SimpleMap[k] == '[').Sum(k => k.Item1 + (100 * k.Item2));

        Output = result.ToString();
    }

    private bool CheckMove(HashSet<(int, int)> locations, char move, ref HashSet<(int x, int y)> crates)
    {
        (int dx, int dy) = DirectionsYDown[move];
        HashSet<(int, int)> nextSet = [];
        foreach ((int x, int y) in locations)
        {
            crates.Add((x, y));
            (int x, int y) nextPos = (x + dx, y + dy), nextPosR = (x + dx + 1, y + dy);
            char nextChar = SimpleMap[nextPos], nextCharR = SimpleMap[nextPosR];
            ///*
            if (SimpleMap[(x, y)] == '@')
            {
                crates = [(x, y)];
                return nextChar switch
                {
                    '#' => false,
                    'O' or '[' => CheckMove([nextPos], move, ref crates),
                    ']' => CheckMove([(nextPos.x - 1, nextPos.y)], move, ref crates),
                    _ => true,
                };
            }
            //*/
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
        SimpleMap[robot] = '.';
        foreach((int x, int y) in crates.Reverse())
        {
            SimpleMap[(x + dx, y + dy)] = SimpleMap[(x, y)];
            SimpleMap[(x, y)] = '.';
            if (Part2 && (x, y) != robot)
            {
                SimpleMap[(x + dx + 1, y + dy)] = SimpleMap[(x + 1, y)];
                SimpleMap[(x + 1, y)] = '.';
            }
        }
        robot = (robot.x + dx, robot.y + dy);
        SimpleMap[robot] = '@';
    }

    private bool CheckMoveSimple((int, int) robot, char move, out (int, int) spacePos)
    {
        (int dx, int dy) = DirectionsYDown[move];
        bool canMove = false, hitWall = false;
        (int x, int y) nextPos = robot;
        spacePos = (0, 0);
        do
        {
            nextPos.x += dx; nextPos.y += dy;
            if (SimpleMap[nextPos] == '#') hitWall = true;
            else if (SimpleMap[nextPos] == '.') { canMove = true; spacePos = nextPos; }
        } while (!canMove && !hitWall);
        return canMove;
    }

    private void MoveSimple(ref (int x, int y) robot, char move, (int x, int y) spacePos)
    {
        (int dx, int dy) = DirectionsYDown[move];
        SimpleMap[robot] = '.';
        int y = robot.y;
        robot = (robot.x + dx, robot.y + dy);
        SimpleMap[robot] = '@';
        if (Part1)
        {
            if (robot != spacePos) SimpleMap[spacePos] = 'O';
        }
        else
        {
            switch (move)
            {
                case '<':
                    if (robot != spacePos)
                    {
                        for (int x = spacePos.x; x < robot.x; x += 2)
                        {
                            SimpleMap[(x, y)] = '[';
                            SimpleMap[(x + 1, y)] = ']';
                        }
                    }
                    break;
                case '>':
                    if (robot != spacePos)
                    {
                        for (int x = spacePos.x; x > robot.x; x -= 2)
                        {
                            SimpleMap[(x, y)] = ']';
                            SimpleMap[(x - 1, y)] = '[';
                        }
                    }
                    break;
            }
        }
    }
}
