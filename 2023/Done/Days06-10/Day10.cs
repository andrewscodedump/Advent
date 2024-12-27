using System.Linq;

namespace Advent2023;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        bool debug = false;
        PopulateMapFromInput(out int width, out int height);

        (int x, int y) location = (0, 0); (int dx, int dy) previousMove = (0, 0);
        int steps = 0;
        List<(int x, int y)> mainLoop = [];
        Dictionary<(char next, int pdx, int pdy), (int dx, int dy)> moves = new()
        {
            { ('7', 1, 0), (0, 1) }, { ('7', 0, -1), (-1, 0) },
            { ('|', 0, -1), (0, -1) }, { ('|', 0, 1), (0, 1) },
            { ('J', 1, 0), (0, -1) }, { ('J', 0, 1), (-1, 0) },
            { ('L', -1, 0), (0, -1) }, { ('L', 0, 1), (1, 0) },
            { ('-', -1, 0), (-1, 0) }, { ('-', 1, 0), (1, 0) },
            { ('F', -1, 0), (0, 1) }, { ('F', 0, -1), (1, 0) },
        };

        //Get the start point
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                if (SimpleMap[(x, y)] == 'S')
                {
                    location = (x, y);
                    mainLoop.Add(location);
                    break;
                }

        // Get a valid next move from the start point
        foreach ((int dx, int dy) in DirectNeighbours.Where(n => moves.ContainsKey((SimpleMap[(location.x + n.x, location.y + n.y)], n.x, n.y))))
        {
            previousMove = (dx, dy);
            location = (location.x + dx, location.y + dy);
            steps++;
        }

        // Follow the path
        do
        {
            mainLoop.Add(location);
            (int dx, int dy) nextMove = moves[(SimpleMap[location], previousMove.dx, previousMove.dy)];
            location = (location.x + nextMove.dx, location.y + nextMove.dy);
            previousMove = nextMove;
            steps++;
        } while (SimpleMap[location] != 'S');

        // Clear all the junk pipes
        foreach ((int, int) test in SimpleMap.Keys.Where(k=>!mainLoop.Contains(k)))
                SimpleMap[test] = '.';

        // Stretch the map
        if (debug) DrawMap(false, false);
        StretchMap(width, height);
        width = (width * 2) + 1; height = (height * 2) + 1;

        // Loop round all the unchecked locations
        int unmappedCount = SimpleMap.Values.Count(v => v == '.');
        do
        {
            foreach (KeyValuePair<(int x, int y), char> kvp in SimpleMap)
            {
                if (kvp.Value != '.') continue;
                // If any neighbours are outside mark as outside
                foreach ((int dx, int dy) in Neighbours)
                {
                    if (SimpleMap[(kvp.Key.x + dx, kvp.Key.y + dy)] == 'O')
                    {
                        SimpleMap[(kvp.Key.x, kvp.Key.y)] = 'O';
                    }
                }
            }
            int test = SimpleMap.Values.Count(v => v == '.');
            // Repeat until outside after = outside before
            if (test == unmappedCount)
                break;
            else
                unmappedCount = test;
        } while (true);

        if (debug) DrawMap(false, false);
        SquishMap(width, height);
        if (debug) DrawMap(false, false);
        unmappedCount = SimpleMap.Values.Count(v => v == '.');

        Output = (Part1 ? steps / 2 : unmappedCount).ToString();
    }

    private void StretchMap(int width, int height)
    {
        Dictionary<(int, int), char> newMap = [];

        for (int y = -1; y < height * 2; y++)
        {
            newMap[(-1, y)] = 'O';
            newMap[((width * 2) - 1, y)] = 'O';
        }
        for (int x = -1; x < width * 2; x++)
        {
            newMap[(x, -1)] = 'O';
            newMap[(x, (height * 2) - 1)] = 'O';
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                newMap[(x * 2, y * 2)] = SimpleMap[(x, y)];
                if (x != width - 1)
                {
                    if (y != height - 1)
                        newMap[((x * 2) + 1, (y * 2) + 1)] = '.';
                    switch (SimpleMap[(x, y)])
                    {
                        case '.':
                        case 'J':
                        case '|':
                        case '7':
                            newMap[((x * 2) + 1, y * 2)] = '.';
                            break;
                        case 'L':
                        case '-':
                        case 'F':
                            newMap[((x * 2) + 1, y * 2)] = '-';
                            break;
                        case 'S':
                            // This isn't guaranteed to work, but it does
                            // For completeness, should map differently depending on what the character to the right is
                            newMap[((x * 2) + 1, y * 2)] = SimpleMap[(x + 1, y)];
                            break;
                    }
                }
                if (y != height - 1)
                    switch (SimpleMap[(x, y + 1)])
                    {
                        case '.':
                        case 'F':
                        case '-':
                        case '7':
                            newMap[(x * 2, (y * 2) + 1)] = '.';
                            break;
                        case 'L':
                        case '|':
                        case 'J':
                            newMap[(x * 2, (y * 2) + 1)] = '|';
                            break;
                        case 'S':
                            // This isn't guaranteed to work, but it does
                            // For completeness, should map differently depending on what the character below is
                            newMap[(x * 2, (y * 2) + 1)] = SimpleMap[(x, y + 1)];
                            break;
                    }
            }
        }
        SimpleMap =newMap;
    }

    private void SquishMap(int width, int height)
    {
        Dictionary<(int, int), char> newMap = [];
        for (int y = 0; y < height / 2; y++)
            for (int x = 0; x < width / 2; x++)
                newMap[(x, y)] = SimpleMap[(x * 2, y * 2)];
        SimpleMap = newMap;
    }
}
