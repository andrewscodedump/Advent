namespace Advent2018;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        Dictionary<(int, int), char> scan = new() { { (500, 0), '+' } };
        Queue<((int, int), (int, int), char)> flow = new();
        flow.Enqueue(((500, 0), (0, 1), '|'));
        (int minX, int maxX, int minY, int maxY) = (int.MaxValue, 0, 0, 0);
        int water = 0;
        char[] splitter = [' ', ',', '=', '.'];
        bool debug = false;

        #endregion Setup Variables and Parse Inputs

        foreach (string line in Inputs)
        {
            string[] bits = line.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            if (bits[0] == "x")
            {
                int x = int.Parse(bits[1]);
                minX = Math.Min(minX, x); maxX = Math.Max(maxX, x);
                for (int y = int.Parse(bits[3]); y <= int.Parse(bits[4]); y++)
                {
                    maxY = Math.Max(maxY, y);
                    if (!scan.ContainsKey((x, y)))
                        scan.Add((x, y), '#');
                }
            }
            else
            {
                int y = int.Parse(bits[1]);
                maxY = Math.Max(maxY, y);
                for (int x = int.Parse(bits[3]); x <= int.Parse(bits[4]); x++)
                {
                    minX = Math.Min(minX, x); maxX = Math.Max(maxX, x);
                    if (!scan.ContainsKey((x, y)))
                        scan.Add((x, y), '#');
                }
            }
        }

        do
        {
            if (water == 0)
                PrintIt(scan, (minX, maxX, minY, maxY), debug);
            ((int x, int y) pos, (int x, int y) move, char fillChar) = flow.Dequeue();
            (int x, int y) newPos = (pos.x + move.x, pos.y + move.y);
            if (newPos.y >= 1867 && newPos.y <= 1883)
                PrintIt(scan, (430, 470, 1867, 1883), debug);
            if (newPos.y > maxY) continue;
            scan.TryAdd(newPos, '.');
            char nextChar = scan[newPos];

            if (move == (1, 0)   // Right
            || move == (-1, 0))    //Left
            {
                switch (nextChar)
                {
                    case '.':
                        water++;
                        scan[newPos] = fillChar;
                        if (!scan.ContainsKey((newPos.x, newPos.y + 1)) || scan[(newPos.x, newPos.y + 1)] == '.')
                            flow.Enqueue((newPos, (0, 1), '|'));
                        else
                            flow.Enqueue((newPos, move, fillChar));
                        break;
                    case '|':
                        scan[newPos] = fillChar;
                        if (!scan.ContainsKey((newPos.x, newPos.y + 1)) || scan[(newPos.x, newPos.y + 1)] == '|')
                            flow.Enqueue((newPos, (0, 1), fillChar));
                        else
                            flow.Enqueue((newPos, move, fillChar));
                        break;
                    default:
                        // I think that everything else is an end condition.
                        break;
                }
            }
            else if (move == (0, 1))    // Down
            {
                if (nextChar == '.')
                {
                    water++;
                    scan[newPos] = fillChar;
                    flow.Enqueue((newPos, move, fillChar));
                }
                else if (nextChar == '|')
                {
                    flow.Enqueue((newPos, move, fillChar));
                }
                else if (nextChar == '#' || nextChar == '~')
                {
                    // Go right until we find a hole or a wall
                    int testX = pos.x;
                    bool hole, wall;
                    // Find bounds
                    do
                    {
                        if (!scan.ContainsKey((++testX, pos.y))) scan[(testX, pos.y)] = '.';
                        if (!scan.ContainsKey((testX, pos.y + 1))) scan[(testX, pos.y + 1)] = '.';
                        hole = scan[(testX, pos.y + 1)] == '.';
                        wall = scan[(testX, pos.y)] == '#';
                    } while (!hole && !wall);
                    // Go left until we find a hole or a wall
                    testX = pos.x;
                    wall = false;
                    while (!hole && !wall)
                    {
                        if (!scan.ContainsKey((--testX, pos.y))) scan[(testX, pos.y)] = '.';
                        if (!scan.ContainsKey((testX, pos.y + 1))) scan[(testX, pos.y + 1)] = '.';
                        hole = scan[(testX, pos.y + 1)] == '.';
                        wall = scan[(testX, pos.y)] == '#';
                    }
                    // If we found a hole in either, set char to | and enqueue right and left
                    if (hole)
                    {
                        water++;
                        scan[pos] = '|';
                        flow.Enqueue((pos, (1, 0), '|'));
                        flow.Enqueue((pos, (-1, 0), '|'));
                    }
                    // Otherwise set char to ~ and enqueue right, left and up
                    else    // wall must be true
                    {
                        scan[pos] = '~';
                        testX = pos.x;
                        while (scan[(++testX, pos.y)] != '#')
                        {
                            water++;
                            scan[(testX, pos.y)] = '~';
                        }
                        testX = pos.x;
                        while (scan[(--testX, pos.y)] != '#')
                        {
                            water++;
                            scan[(testX, pos.y)] = '~';
                        }
                        flow.Enqueue((pos, (0, -1), '~'));
                    }
                }
            }
            else if (move == (0, -1)) //Up
            {
                if (nextChar == '~') continue;
                // Find bounds
                bool hole, wall;
                // Go right until we find a hole or a wall
                int testX = newPos.x;
                do
                {
                    if (!scan.ContainsKey((++testX, newPos.y))) scan[(testX, newPos.y)] = '.';
                    if (!scan.ContainsKey((testX, newPos.y + 1))) scan[(testX, newPos.y + 1)] = '.';
                    hole = scan[(testX, newPos.y + 1)] == '.';
                    wall = scan[(testX, newPos.y)] == '#';
                } while (!hole && !wall);
                // Go left until we find a hole or a wall
                testX = newPos.x;
                wall = false;
                while (!hole && !wall)
                {
                    if (!scan.ContainsKey((--testX, newPos.y))) scan[(testX, newPos.y)] = '.';
                    if (!scan.ContainsKey((testX, newPos.y + 1))) scan[(testX, newPos.y + 1)] = '.';
                    hole = scan[(testX, newPos.y + 1)] == '.';
                    wall = scan[(testX, newPos.y)] == '#';
                }
                // If we found a hole in either, set char to | and enqueue right and left
                if (hole)
                {
                    scan[newPos] = '|';
                    if (nextChar == '.') water++;
                    flow.Enqueue((newPos, (1, 0), '|'));
                    flow.Enqueue((newPos, (-1, 0), '|'));
                }
                // Otherwise set char to ~ and enqueue right, left and up
                else    // wall must be true
                {
                    scan[newPos] = '~';
                    testX = newPos.x;
                    while (scan[(++testX, newPos.y)] != '#')
                    {
                        water++;
                        scan[(testX, newPos.y)] = '~';
                    }
                    testX = newPos.x;
                    while (scan[(--testX, newPos.y)] != '#')
                    {
                        water++;
                        scan[(testX, newPos.y)] = '~';
                    }
                    flow.Enqueue((newPos, (0, -1), '~'));
                }
            }

        } while (flow.Count != 0);

        PrintIt(scan, (minX, maxX, minY, maxY), debug);

        water = 0;
        int pooled = 0;
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX - 1; x <= maxX + 1; x++)
                if (scan.ContainsKey((x, y)))
                {
                    if (scan[(x, y)] == '|') water++;
                    if (scan[(x, y)] == '~') pooled++;
                }
        }

        Output = (pooled + (Part1 ? water : 0)).ToString();
    }

    #region Private Classes and Methods
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private static void PrintIt(Dictionary<(int x, int y), char> scan, (int minX, int maxX, int minY, int maxY) limits, bool debug)
    {
        if (debug) return;
        string[] lines = ["     ", "     ", "     "];
        for (int x = limits.minX - 1; x <= limits.maxX + 1; x++)
        {
            lines[0] += x % 10 == 0 ? (x / 100 % 10).ToString() : " ";
            lines[1] += x % 10 == 0 ? (x / 10 % 10).ToString() : " ";
            lines[2] += x % 10;
        }
        Debug.WriteLine(lines[0]);
        Debug.WriteLine(lines[1]);
        Debug.WriteLine(lines[2]);


        for (int y = limits.minY; y <= limits.maxY; y++)
        {
            lines[0] = y.ToString("0000 ");
            for (int x = limits.minX - 1; x <= limits.maxX + 1; x++)
                lines[0] += scan.ContainsKey((x, y)) ? scan[(x, y)].ToString() : ".";
            Debug.WriteLine(lines[0]);
        }
        Debug.WriteLine("");
    }
    #endregion Private Classes and Methods

}
