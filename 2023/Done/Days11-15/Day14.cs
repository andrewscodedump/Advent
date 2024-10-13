namespace Advent2023;

public partial class Day14 : Advent.Day
{
    int width = 0, height = 0;
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

    private int RunSpinCycle(int targetCycles)
    {
        bool repeatFound = false;
        List<string> seenStates = [GetState(SimpleMap)];
        int result = 0, cycles = 0;
        do
        {
            cycles++;
            result = RunSpinCycle();
            string state = GetState(SimpleMap);
            if (seenStates.Contains(state))
            {
                repeatFound = true;
                int startOfRepeat = seenStates.IndexOf(state), endOfRepeat = cycles;
                int targetSet = ((targetCycles - startOfRepeat) % (endOfRepeat - startOfRepeat)) + startOfRepeat;
                result = GetWeightFromState(seenStates[targetSet]);
            }
            else
                seenStates.Add(state);
        } while (!repeatFound && cycles < targetCycles);

        return result;
    }

    private int GetWeightFromState(string state)
    {
        int result = 0;
        Debug.Print(state);
        for (int i = 0; i < state.Length; i++)
            if (state[i] == 'O')
                result += height - (i / (width + 2)) + 1;
        return result;
    }

    private int RunSpinCycle()
    {
        MoveStones((0, -1));
        MoveStones((-1, 0));
        MoveStones((0, 1));
        int result = MoveStones((1, 0));
        return result;
    }

    private static string GetState(Dictionary<(int x, int y), char> dict)
    {
        StringBuilder state = new();
        foreach (KeyValuePair<(int, int), char> kvp in dict.OrderBy(d => (d.Key.y * 100) + d.Key.x))
            state.Append(kvp.Value);
        return state.ToString();

    }
    private int MoveStones((int dx, int dy) move)
    {
        int totalScore = 0;
        int xStart = 0, xEnd = width, xOffset = 1;
        int yStart = 0, yEnd = width, yOffset = 1;
        if (move.dx == 1) { xStart = width - 1; xEnd = 1; xOffset = -1; }
        if (move.dy == 1) { yStart = height - 1; yEnd = 1; yOffset = -1; }
        for (int y = yStart; y * yOffset < yEnd; y += yOffset)
        {
            for (int x = xStart; x * xOffset < xEnd; x += xOffset)
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
                }
                totalScore += height - currentPos.y;
            }
        }
        return totalScore;
    }
}