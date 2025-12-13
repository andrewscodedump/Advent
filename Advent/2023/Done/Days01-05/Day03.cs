namespace Advent2023;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        Dictionary<(int x, int y), List<int>> gears = [];
        PopulateMapFromInputWithBorders('.', out int width, out int height);

        for (int y = 0; y < height; y++)
        {
            int number = 0;
            bool isValid = false;
            List<(int, int)> stars = [];
            for (int x = 0; x <= width; x++)
            {
                char item = SimpleMap[(x, y)];
                if (!char.IsDigit(item) || x == width + 1)
                {
                    if (number == 0) continue;
                    else
                    {
                        if (isValid)
                        {
                            result += number;
                            foreach((int, int) pos in stars)
                            {
                                if (gears.TryGetValue(pos, out List<int> value))
                                    value.Add(number);
                                else
                                    gears[pos] = [number];
                            }
                        }
                        number = 0;
                        isValid = false;
                        stars = [];
                    }
                }
                else
                {
                    number = (number * 10) + (item - '0');
                    foreach ((int dx, int dy) in Neighbours)
                    {
                        char neighbour = SimpleMap[(x + dx, y + dy)];
                        if (!char.IsDigit(neighbour) && neighbour != '.') isValid = true;
                        if (neighbour == '*' && !stars.Contains((x + dx, y + dy))) stars.Add((x + dx, y + dy));
                    }
                }
            }
        }

        if(Part2)
            result = gears.Where(g => g.Value.Count == 2).Select(g => g.Value[0] * g.Value[1]).Sum();

        Output = result.ToString();
    }
}
