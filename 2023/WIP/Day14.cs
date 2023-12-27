namespace Advent2023;

public partial class Day14 : Advent.Day
{
    int width = 0, height = 0;
    bool debug = false;

    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('*', out width, out height);
        int totalScore = 0;
        if (Part1)
        {
            totalScore += MoveStones((0, -1));
        }
        else
        {
            totalScore = RunSpinCycle(1_000_000_000);
        }

        Output = totalScore.ToString();
    }

    private int RunSpinCycle(int times)
    {
        bool repeat = false;
        //HashSet<Dictionary<(int, int), char>> seenPositions = [];
        HashSet<int> seenPositions = [];
        int result = 0, cycles = 0;
        do
        {
            result = RunSpinCycle();
            repeat = !seenPositions.Add(SimpleMap.GetHashCode());
            cycles++;
        } while (!repeat && cycles < times);

        return result;
    }

    private int RunSpinCycle()
    {
        MoveStones((0, -1));
        MoveStones((-1, 0));
        MoveStones((0, 1));
        int result = MoveStones((1, 0));
        if (debug) DrawMap(false, false);
        return result;
    }
    private int MoveStones((int dx, int dy) move)
    {
        int totalScore = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (SimpleMap[(x, y)] != 'O') continue;
                (int x, int y) currentPos = (x, y);
                (int x, int y) nextPos = (x + move.dx, y + move.dy);

                while (SimpleMap[nextPos] == '.')
                {
                    SimpleMap[(currentPos.x, currentPos.y)] = '.';
                    SimpleMap[nextPos] = 'O';
                    currentPos = nextPos;
                    nextPos = (currentPos.x + move.dx, currentPos.y + move.dy);
                    //DrawMap(false, false);
                }
                totalScore += height - currentPos.y;
                if (debug)
                {
                    Debug.Print(SimpleMap.GetHashCode().ToString());
                    DrawMap(false, false);
                }
            }
        }
        return totalScore;
    }
}
