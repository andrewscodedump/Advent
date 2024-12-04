namespace Advent2024;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int timesFound = 0;
        PopulateMapFromInputWithBorders('.', out int width, out int height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Part1)
                {
                    if (SimpleMap[(x, y)] != 'X') continue;
                    if (y >= 3 && SimpleMap[(x, y - 1)] == 'M' && SimpleMap[(x, y - 2)] == 'A' && SimpleMap[(x, y - 3)] == 'S') timesFound++;
                    if (y >= 3 && x <= width - 4 && SimpleMap[(x + 1, y - 1)] == 'M' && SimpleMap[(x + 2, y - 2)] == 'A' && SimpleMap[(x + 3, y - 3)] == 'S') timesFound++;
                    if (x <= width - 4 && SimpleMap[(x + 1, y)] == 'M' && SimpleMap[(x + 2, y)] == 'A' && SimpleMap[(x + 3, y)] == 'S') timesFound++;
                    if (y <= height - 4 && x <= width - 4 && SimpleMap[(x + 1, y + 1)] == 'M' && SimpleMap[(x + 2, y + 2)] == 'A' && SimpleMap[(x + 3, y + 3)] == 'S') timesFound++;
                    if (y <= height - 4 && SimpleMap[(x, y + 1)] == 'M' && SimpleMap[(x, y + 2)] == 'A' && SimpleMap[(x, y + 3)] == 'S') timesFound++;
                    if (y <= height - 4 && x >= 3 && SimpleMap[(x - 1, y + 1)] == 'M' && SimpleMap[(x - 2, y + 2)] == 'A' && SimpleMap[(x - 3, y + 3)] == 'S') timesFound++;
                    if (x >= 3 && SimpleMap[(x - 1, y)] == 'M' && SimpleMap[(x - 2, y)] == 'A' && SimpleMap[(x - 3, y)] == 'S') timesFound++;
                    if (y >= 3 && x >= 3 && SimpleMap[(x - 1, y - 1)] == 'M' && SimpleMap[(x - 2, y - 2)] == 'A' && SimpleMap[(x - 3, y - 3)] == 'S') timesFound++;
                }
                else
                {
                    int mas = 0;
                    if (SimpleMap[(x, y)] != 'A') continue;
                    if (SimpleMap[(x + 1, y - 1)] == 'M' && SimpleMap[(x - 1, y + 1)] == 'S') mas++;
                    if (SimpleMap[(x + 1, y + 1)] == 'M' && SimpleMap[(x - 1, y - 1)] == 'S') mas++;
                    if (SimpleMap[(x - 1, y + 1)] == 'M' && SimpleMap[(x + 1, y - 1)] == 'S') mas++;
                    if (SimpleMap[(x - 1, y - 1)] == 'M' && SimpleMap[(x + 1, y + 1)] == 'S') mas++;
                    if (mas == 2) timesFound++;
                }
            }
        }
        Output = timesFound.ToString();
    }
}
