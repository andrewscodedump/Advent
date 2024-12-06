namespace Advent2024;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int locations = 0;
        PopulateMapFromInputWithBorders('@',out int width, out int height);
        (int, int) curPos = Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height), (x, y) => (x, y)).First(c => SimpleMap[(c.x, c.y)] == '^');
        SimpleMap[curPos] = 'X';

        if (Part1)
            CheckRoute(out locations, curPos);
        else
        {
            Dictionary<(int, int), char> startingMap = new(SimpleMap);
            foreach ((int x, int y) in Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height), (x, y) => (x, y)))
            {
                SimpleMap = new(startingMap);
                if (SimpleMap[(x, y)] == '.')
                {
                    SimpleMap[(x, y)] = '#';
                    if (CheckRoute(out _, curPos)) locations++;
                }
            }
        }
        Output = locations.ToString();
    }
    
    private bool CheckRoute(out int locations, (int x, int y) curPos)
    {
        HashSet<((int, int), char)> visited = [];
        bool loop = false;
        locations = 1;
        bool leftArea = false;
        char nextType, curHeading = '^';
        (int x, int y) nextPos, curDir = DirectionsYDown[curHeading];
        do
        {
            nextPos = (curPos.x + curDir.x, curPos.y + curDir.y);
            nextType = SimpleMap[nextPos];
            switch (nextType)
            {
                case '.':
                    visited.Add((nextPos, curHeading));
                    locations++;
                    curPos = nextPos;
                    break;
                case '#':
                    curHeading = turns[(curHeading, 'R')];
                    curDir = DirectionsYDown[curHeading];
                    break;
                case 'X':
                    if(!visited.Add((nextPos,curHeading))) loop = true;
                    curPos = nextPos;
                    break;
                default:
                    leftArea = true;
                    break;
            }
            SimpleMap[curPos] = 'X';
        } while (!leftArea && !loop);
        return loop;
    }
}
