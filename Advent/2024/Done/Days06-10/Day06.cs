namespace Advent2024;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('@');
        (int, int) startPos = SimpleMap.First(p => p.Value == '^').Key;

        CheckRoute(out IEnumerable<(int, int)> baseRoute, startPos, (-1, -1));
        int locations = Part1 ? baseRoute.Count() : baseRoute.Count(pos => CheckRoute(out _, startPos, pos));
        Output = locations.ToString();
    }
    
    private bool CheckRoute(out IEnumerable<(int, int)> route, (int x, int y) curPos, (int, int) newBlocker)
    {
        if (newBlocker != (-1, -1)) SimpleMap[newBlocker] = '#';
        bool loop = false, leftArea = false;
        (int x, int y) nextPos, curDir = (0, -1);
        HashSet<((int, int) pos, (int, int))> visited = [(curPos, curDir)];
        do
        {
            nextPos = (curPos.x + curDir.x, curPos.y + curDir.y);
            switch (SimpleMap[nextPos])
            {
                case '#':
                    curDir = changeDirection[(curDir, 'R')];
                    break;
                case '@':
                    leftArea = true;
                    break;
                default:
                    loop = !visited.Add((nextPos, curDir));
                    curPos = nextPos;
                    break;
            }
        } while (!leftArea && !loop);
        route = visited.DistinctBy(v => v.pos).Select(v => v.pos);
        if (newBlocker != (-1, -1)) SimpleMap[newBlocker] = '.';
        return loop;
    }
}
