namespace Advent2024;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInput(out int width, out int height);
        HashSet<(int, int)> antinodes = [];

        foreach (char frequency in SimpleMap.Values.Where(f => f != '.').Distinct())
        {
            var locations = SimpleMap.Where(kvp => kvp.Value == frequency).ToList();
            var pairs = locations.SelectMany(l1 => locations.Select(l2 => Tuple.Create(l1.Key, l2.Key)));
            foreach (((int x, int y) a1, (int x, int y) a2) in pairs)
            {
                if (a1 == a2) continue;
                if (Part2) { antinodes.Add(a1); antinodes.Add(a2); }
                HashSet<((int, int), (int, int))> pairsChecked = [];
                if (!pairsChecked.Add((a1, a2)) || !pairsChecked.Add((a2, a1))) continue;
                int xdiff = a2.x - a1.x, ydiff = a2.y - a1.y;
                (int x, int y) antinode, start = a1;
                bool inbounds = true;
                do
                {
                    antinode = (start.x - xdiff, start.y - ydiff);
                    inbounds = antinode.x >= 0 && antinode.x < width && antinode.y >= 0 && antinode.y < height;
                        if (inbounds)
                            antinodes.Add(antinode);
                    start = antinode;
                    if (Part1) break;
                } while (inbounds);
                start = a2;
                do
                {
                    antinode = (start.x + xdiff, start.y + ydiff);
                    inbounds = antinode.x >= 0 && antinode.x < width && antinode.y >= 0 && antinode.y < height;
                    if (inbounds)
                        antinodes.Add(antinode);
                    start = antinode;
                    if (Part1) break;
                } while (inbounds);
            }
        }
        Output = antinodes.Count.ToString();
    }
}