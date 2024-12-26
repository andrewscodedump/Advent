namespace Advent2020;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        IEnumerable<int> offsets = Enumerable.Range(-1, 3);
        (int dx, int dy, int dz, int dw)[] neighbours4D = offsets.SelectMany(a => offsets.SelectMany(b => offsets.SelectMany(c => offsets.Select(d => (a, b, c, d))))).Where(t => t != (0, 0, 0, 0)).ToArray();
        string input = Input;

        int cycles = 6, startWidth = (int)Math.Sqrt(input.Length), activeCells = 0;
        IEnumerable<int> xyRange = Enumerable.Range(-cycles - 1, (2 * cycles) + startWidth + 2), zwRange = Enumerable.Range(-cycles - 1, (2 * cycles) + 3);
        Dictionary<(int, int, int, int), int> emptyMap = xyRange.SelectMany(x => xyRange.SelectMany(y => zwRange.SelectMany(z => zwRange.Select(w => (x, y, z, w))))).ToDictionary(t => t, t => 0);
        Dictionary<(int, int, int, int), int> map = new(emptyMap);
        for (int i = 0; i < input.Length; i++) if (input[i] == '#') map[(i % startWidth, i / startWidth, 0, 0)] = 1;

        for (int c = 1; c <= cycles; c++)
        {
            Dictionary<(int, int, int, int), int> newMap = new(emptyMap);
            for (int x = -c; x <= c + startWidth - 1; x++)
                for (int y = -c; y <= c + startWidth - 1; y++)
                    for (int z = -c; z <= c; z++)
                        for (int w = Part1 ? 0 : -c; w <= (Part1 ? 0 : c); w++)
                        {
                            int neighbourCount = Part1 ? neighbours4D.Where(n => n.dw == 0).Where(n => map[(x + n.dx, y + n.dy, z + n.dz, 0)] == 1).Count() : neighbours4D.Where(n => map[(x + n.dx, y + n.dy, z + n.dz, w + n.dw)] == 1).Count();
                            if ((map[(x, y, z, w)] == 0 && neighbourCount == 3) || (map[(x, y, z, w)] == 1 && (neighbourCount == 2 || neighbourCount == 3)))
                                newMap[(x, y, z, w)] = 1;
                            if (c == cycles)
                                activeCells += newMap[(x, y, z, w)];
                        }
            map = new(newMap);
        }

        Output = activeCells.ToString();
    }
}
