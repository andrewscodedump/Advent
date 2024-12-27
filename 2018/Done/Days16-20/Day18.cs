﻿namespace Advent2018;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        int width = Input.Length, height = Inputs.Length;
        Dictionary<(int, int), char> area = [];
        Dictionary<int, List<(int gen, Dictionary<(int, int), char> dict)>> history = [];
        int trees = 0, yards = 0, empty = 0;
        int limit = Part1 ? 10 : 1000000000;
        int result, generation = 0;
        char newChar;
        bool repeatFound = false;

        for (int y = -1; y < width + 1; y++)
            for (int x = -1; x < height + 1; x++)
            {
                area[(x, y)] = newChar = y != -1 && y != width && x != -1 && x != width ? Inputs[y][x] : '*';
                yards += newChar == '#' ? 1 : 0; trees += newChar == '|' ? 1 : 0; empty += newChar == '.' ? 1 : 0;
            }
        Dictionary<(int, int), char> work = new(area);
        #endregion Setup Variables and Parse Inputs

        do
        {
            generation++;
            for (int y = 0; y < width; y++)
                for (int x = 0; x < height; x++)
                    switch (area[(x, y)])
                    {
                        case '.':   // Tree if 3+ trees
                            if (CountNeighbours(area, x, y, '|') >= 3)
                            {
                                work[(x, y)] = '|';
                                empty--; trees++;
                            }
                            break;
                        case '#':   // Yard if 1+ yard and 1+ tree else empty
                            if (CountNeighbours(area, x, y, '#') >= 1 && CountNeighbours(area, x, y, '|') >= 1)
                                work[(x, y)] = '#';
                            else
                            {
                                work[(x, y)] = '.';
                                yards--; empty++;
                            }
                            break;
                        case '|':   // Yard if 3+ yards
                            if (CountNeighbours(area, x, y, '#') >= 3)
                            {
                                work[(x, y)] = '#';
                                trees--; yards++;
                            }
                            break;
                        default:
                            break;
                    }

            area = new Dictionary<(int, int), char>(work);
            result = trees * yards;

            if (Part2 && history.TryGetValue(result, out List<(int gen, Dictionary<(int, int), char> dict)> value) && !repeatFound)
            {
                // The result's the same - do a deep compare
                for (int i = 0; i < value.Count; i++)
                {
                    repeatFound = DictionaryCompare(area, value[i].dict);
                    if (repeatFound)
                        limit = generation + ((limit - generation) % (generation - value[i].gen));
                }
                if (!repeatFound)
                    value.Add((generation, area));
            }
            else if (!repeatFound)
                history.Add(result, new List<(int, Dictionary<(int, int), char>)> { (generation, area) });
        } while (generation < limit);

        Output = result.ToString();
    }
}
