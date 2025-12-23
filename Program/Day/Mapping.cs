namespace Advent;

public abstract partial class Day
{
    public bool DrawMaps { get; set; }
    protected readonly Dictionary<char, (int x, int y)> Directions = new() { { 'N', (0, 1) }, { 'S', (0, -1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, 1) }, { 'D', (0, -1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, 1) }, { 'v', (0, -1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly Dictionary<char, (int x, int y)> DirectionsYDown = new() { { 'N', (0, -1) }, { 'S', (0, 1) }, { 'E', (1, 0) }, { 'W', (-1, 0) }, { 'U', (0, -1) }, { 'D', (0, 1) }, { 'L', (-1, 0) }, { 'R', (1, 0) }, { '^', (0, -1) }, { 'v', (0, 1) }, { '>', (1, 0) }, { '<', (-1, 0) } };
    protected readonly List<(int x, int y)> DirectNeighbours = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    protected readonly List<(int x, int y)> Neighbours = [(-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1)];
    protected readonly Dictionary<(char, char), char> turns = new() { { ('^', 'L'), '<' }, { ('^', 'R'), '>' }, { ('>', 'L'), '^' }, { ('>', 'R'), 'v' }, { ('v', 'L'), '>' }, { ('v', 'R'), '<' }, { ('<', 'L'), 'v' }, { ('<', 'R'), '^' }, { ('N', 'L'), 'W' }, { ('N', 'R'), 'E' }, { ('E', 'L'), 'N' }, { ('E', 'R'), 'S' }, { ('S', 'L'), 'E' }, { ('S', 'R'), 'W' }, { ('W', 'L'), 'S' }, { ('W', 'R'), 'N' } };
    protected readonly Dictionary<((int, int), char), (int, int)> changeDirection = new() { { ((0, -1), 'L'), (-1, 0) }, { ((0, -1), 'R'), (1, 0) }, { ((1, 0), 'L'), (0, -1) }, { ((1, 0), 'R'), (0, 1) }, { ((0, 1), 'L'), (1, 0) }, { ((0, 1), 'R'), (-1, 0) }, { ((-1, 0), 'L'), (0, 1) }, { ((-1, 0), 'R'), (0, -1) } };
    protected Dictionary<(int, int), char> SimpleMap = [];
    protected Dictionary<(int, int), char> StartingMap = [];
    protected int CountNeighbours(Dictionary<(int, int), char> area, int x, int y, char type) => Neighbours.Count(nbr => area[(x + nbr.x, y + nbr.y)] == type);
    protected int CountDirectNeighbours(Dictionary<(int, int), char> area, int x, int y, char type) => DirectNeighbours.Count(nbr => area[(x + nbr.x, y + nbr.y)] == type);
    protected List<(int, int)> GetNeighbours((int x, int y) pos) => [.. Neighbours.Select(n => (pos.x + n.x, pos.y + n.y))];
    protected List<(int, int)> GetDirectNeighbours((int x, int y) pos) => [.. DirectNeighbours.Select(n => (pos.x + n.x, pos.y + n.y))];

    public void PopulateMapFromInput() => PopulateMapFromInput(out _, out _);
    public void PopulateMapFromInput(out int width, out int height) => PopulateMap(Inputs, out width, out height);

    public void PopulateMap(string[] inputs) => PopulateMap(inputs, out _, out _);
    public void PopulateMap(string[] inputs, out int width, out int height)
    {
        SimpleMap = [];
        width = inputs[0].Length; height = inputs.Length;
        for (int y = 0; y < height; y++)
        {
            string work = inputs[y];
            for (int x = 0; x < work.Length; x++)
            {
                SimpleMap[(x, y)] = work[x];
            }
        }
        StartingMap = new(SimpleMap);
    }
    public void PopulateMapFromInputWithBorders(char borderChar) => PopulateMapFromInputWithBorders(borderChar, out _, out _);
    public void PopulateMapFromInputWithBorders(char borderChar, out int width, out int height) => PopulateMapWithBorders(Inputs, borderChar, out width, out height);
    public void PopulateMapWithBorders(string[] inputs, char borderChar) => PopulateMapWithBorders(inputs, borderChar, out _, out _);

    public void PopulateMapWithBorders(string[] inputs, char borderChar, out int width, out int height)
    {
        SimpleMap = [];
        width = inputs[0].Length; height = inputs.Length;
        for (int y = -1; y <= height; y++)
            for (int x = -1; x <= width; x++)
            {
                if (x == -1 || y == -1 || x == width || y == height)
                    SimpleMap[(x, y)] = borderChar;
                else
                    SimpleMap[(x, y)] = inputs[y][x];
            }
        StartingMap = new(SimpleMap);
    }

    public void RestoreMap() => SimpleMap = new(StartingMap);

    public void DrawMap() => DrawMap(true, false);

    public void DrawMap(bool yUp, bool showCoords)
    {
        int maxX = SimpleMap.Keys.Max(x => x.Item1), maxY = SimpleMap.Keys.Max(x => x.Item2);
        int minX = SimpleMap.Keys.Min(x => x.Item1), minY = SimpleMap.Keys.Min(x => x.Item2);
        DrawMap(yUp, showCoords, minX, minY, maxX, maxY);
    }

    public void DrawMap(bool yUp, bool showCoords, int minX, int minY, int maxX, int maxY)
    {
        if (!DrawMaps) return;
        StringBuilder s = new();
        Debug.Print("---------------------------------------------------------------------");
        if (showCoords)
        {
            s.Append("     ");
            for (int x = minX; x <= maxX; x++)
                s.Append(x % 10);
            Debug.Print(s.ToString());
        }

        if (yUp)
            for (int y = maxY; y >= minY; y--)
            {
                s.Clear();
                if (showCoords)
                    s.Append(y.ToString("D4") + " ");
                for (int x = minX; x <= maxX; x++)
                    s.Append(SimpleMap.ContainsKey((x, y)) ? SimpleMap[(x, y)] : ' ');
                Debug.Print(s.ToString());
            }
        else
            for (int y = minY; y <= maxY; y++)
            {
                s.Clear();
                if (showCoords)
                    s.Append(y.ToString("D4") + " ");
                for (int x = minX; x <= maxX; x++)
                    s.Append(SimpleMap.ContainsKey((x, y)) ? SimpleMap[(x, y)] : ' ');
                Debug.Print(s.ToString());
            }
        if (showCoords)
        {
            s.Clear();
            s.Append("     ");
            for (int x = minX; x <= maxX; x++)
                s.Append(x % 10);
            Debug.Print(s.ToString());
        }
    }
}
