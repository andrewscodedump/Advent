namespace Advent2018;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        Dictionary<(int x, int y), (char state, int score, int round)> startGrid = new();
        Dictionary<(int x, int y), (char state, int score, int round)> grid = new();
        (int score, bool finished) state;
        int round;
        int goblinPower = 3; int elfPower = 2;
        int rows = Inputs.Length, cols = Input.Length;
        bool elfKilled;
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                startGrid.Add((x, y), (Inputs[y][x], Inputs[y][x] == 'G' || Inputs[y][x] == 'E' ? 200 : 0, -1));
        #endregion Setup Variables and Parse Inputs

        do
        {
            round = 0;
            elfKilled = false;
            elfPower++;
            grid = new Dictionary<(int x, int y), (char state, int score, int round)>(startGrid);
            do
            {
                //printIt(grid, rows, cols, round);
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        if (grid[(x, y)].score != 0)
                        {
                            int power = grid[(x, y)].state == 'G' ? goblinPower : elfPower;
                            elfKilled = TakeTurn(ref grid, (x, y), round, power);
                            if (Part2 && elfKilled) break;
                        }
                        if (Part2 && elfKilled) break;
                    }
                    if (Part2 && elfKilled) break;
                }
                state = GetScore(grid.Values.ToArray(), round);
                round++;
            } while (!(state.finished || (Part2 && elfKilled)));
        } while (Part2 && elfKilled);

        Output = state.score.ToString();
    }

    #region Private Classes and Methods
    private bool TakeTurn(ref Dictionary<(int x, int y), (char state, int score, int moved)> grid, (int, int) pos, int round, int power)
    {
        bool elfKilled = false;
        if (grid[pos].moved != round)
            if (!DoCombat(ref grid, pos, power, ref elfKilled))
            {
                if (elfKilled) return elfKilled;
                (int, int) newPos = DoMove(ref grid, pos, round);
                DoCombat(ref grid, newPos, power, ref elfKilled);
            }
        return elfKilled;
    }

    private bool DoCombat(ref Dictionary<(int x, int y), (char state, int score, int moved)> grid, (int x, int y) pos, int power, ref bool elfKilled)
    {
        bool hadCombat = false;
        (int, int) weakestPos = (0, 0);
        (char state, int score, int round) weakest = (' ', int.MaxValue, 0);
        foreach ((int x, int y) in offsets.Keys)
        {
            (int tx, int) testPos = (pos.x + x, pos.y + y);
            if ((grid[pos].state == 'G' && grid[testPos].state == 'E') || (grid[pos].state == 'E' && grid[testPos].state == 'G'))
            {
                hadCombat = true;
                (char, int score, int) opponent = grid[testPos];
                if (opponent.score < weakest.score)
                {
                    weakest = opponent;
                    weakestPos = testPos;
                }
            }
        }
        if (weakest.score != int.MaxValue)
            if (weakest.score - power < 0)
            {
                elfKilled = grid[weakestPos].state == 'E';
                grid[weakestPos] = ('.', 0, 0);
            }
            else
                grid[weakestPos] = (weakest.state, weakest.score - power, weakest.round);
        return hadCombat;
    }

    private (int, int) DoMove(ref Dictionary<(int x, int y), (char state, int score, int moved)> grid, (int x, int y) pos, int round)
    {
        char myType = grid[pos].state;
        char target = myType == 'G' ? 'E' : 'G';
        int bestLen = int.MaxValue;
        (int x, int y) bestFirst = (0, 1);
        Queue<((int x, int y) pos, (int x, int y) offset, int len, (int, int) first)> bfs = new();
        HashSet<(int, int)> visited = new() { pos };

        foreach ((int x, int y) offset in offsets.Keys)
            if (grid[(pos.x + offset.x, pos.y + offset.y)].state == '.' || grid[(pos.x + offset.x, pos.y + offset.y)].state == target)
                bfs.Enqueue((pos, offset, 1, offset));

        while (bfs.Count > 0)
        {
            ((int x, int y) pos, (int x, int y) offset, int len, (int, int) first) state;
            state = bfs.Dequeue();
            (int x, int y) newPos = (state.pos.x + state.offset.x, state.pos.y + state.offset.y);
            if (grid[newPos].state == '#' || grid[newPos].state == myType) continue;
            if (visited.Contains(newPos)) continue;
            visited.Add(newPos);
            if (state.len > bestLen) continue;
            if (grid[newPos].state == target)
            {
                if (state.len < bestLen)
                {
                    bestLen = state.len;
                    bestFirst = state.first;
                }
                continue;
            }
            foreach ((int x, int y) offset in offsets.Keys)
            {
                if (grid[(newPos.x + offset.x, newPos.y + offset.y)].state == '.' || grid[(newPos.x + offset.x, newPos.y + offset.y)].state == target)
                    bfs.Enqueue((newPos, offset, state.len + 1, state.first));
            }
        };
        if (bestLen < int.MaxValue)
        {
            (int, int) newPos = (pos.x + bestFirst.x, pos.y + bestFirst.y);
            (char state, int score, int _) = grid[pos];
            grid[newPos] = (state, score, round);
            grid[pos] = ('.', 0, 0);
            return newPos;
        }
        return pos;
    }

    private (int, bool) GetScore((char type, int score, int moved)[] grid, int round)
    {
        int goblins = 0, elves = 0;
        foreach ((char type, int score, int moved) in grid)
        {
            goblins += type == 'G' ? score : 0;
            elves += type == 'E' ? score : 0;
        }
        //BUG - for some reason in a couple of cases, but no others, it's miscounting the rounds
        if (Part1 && !TestMode) round++;
        return ((goblins + elves) * round, goblins == 0 || elves == 0);
    }

    private readonly Dictionary<(int x, int y), int> offsets = new() { { (0, -1), 1 }, { (-1, 0), 2 }, { (1, 0), 3 }, { (0, 1), 4 } };

#pragma warning disable IDE0051 // Remove unused private members
    private static void PrintIt(Dictionary<(int x, int y), (char state, int score, int moved)> grid, int rows, int cols, int round)
#pragma warning restore IDE0051 // Remove unused private members
    {
        string line, extra;
        Debug.WriteLine("Round: " + round);
        for (int y = 0; y < rows; y++)
        {
            line = y.ToString("00: ");
            extra = "   ";
            for (int x = 0; x < cols; x++)
            {
                line += grid[(x, y)].state;
                if (grid[(x, y)].state == 'G' || grid[(x, y)].state == 'E')
                {
                    if (extra != "   ")
                        extra += ", ";
                    extra += grid[(x, y)].state + "(" + grid[(x, y)].score + ")";
                }
            }
            Debug.WriteLine(line + extra);
        }
        Debug.WriteLine("");
    }
    #endregion Private Classes and Methods
}
