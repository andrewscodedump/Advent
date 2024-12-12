namespace Advent2024;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        HashSet<(int, int)> visited = [];
        long total = 0, area, sides;
        PopulateMapFromInputWithBorders('@');

        foreach ((int x, int y) loc in SimpleMap.Keys)
        {
            Dictionary<(char, int), List<int>> allSides = [];
            if (visited.Contains(loc)) continue;
            if (SimpleMap[loc] == '@') continue;
            area = 0; sides = 0;
            char currentPlant = SimpleMap[loc];
            Stack<(int, int)> stack = [];
            stack.Push(loc);
            do
            {
                (int x, int y) pos = stack.Pop();
                visited.Add(pos);
                area++;
                // If no neighbours above, add to tops etc
                if (SimpleMap[(pos.x, pos.y - 1)] != currentPlant && !allSides.TryAdd(('t', pos.y), [pos.x]))
                    allSides[('t', pos.y)].Add(pos.x);
                if (SimpleMap[(pos.x, pos.y + 1)] != currentPlant && !allSides.TryAdd(('b', pos.y), [pos.x]))
                    allSides[('b', pos.y)].Add(pos.x);
                if (SimpleMap[(pos.x - 1, pos.y)] != currentPlant && !allSides.TryAdd(('l', pos.x), [pos.y]))
                    allSides[('l', pos.x)].Add(pos.y);
                if (SimpleMap[(pos.x + 1, pos.y)] != currentPlant && !allSides.TryAdd(('r', pos.x), [pos.y]))
                    allSides[('r', pos.x)].Add(pos.y);
                foreach ((int dx, int dy) in DirectNeighbours)
                {
                    (int x, int y) newPos = (pos.x + dx, pos.y + dy);
                    if (visited.Contains(newPos)) continue;
                    if (SimpleMap[newPos] != currentPlant) continue;
                    if(stack.Contains(newPos)) continue;
                    stack.Push(newPos);
                }
            } while (stack.Count > 0);
            // Get distinct ranges in tops etc
            foreach ((char type, int val) in allSides.Keys)
            {
                sides++;
                allSides[(type, val)].Sort();
                for (int i = 0; i < allSides[(type, val)].Count - 1; i++)
                    if (allSides[(type, val)][i] != allSides[(type, val)][i + 1]-1) sides++;
            }
            long perimeter = allSides.Values.Sum(s => s.Count);
            total += area * (Part1 ? perimeter : sides);
        }

        Output = total.ToString();
    }
}
