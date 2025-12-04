namespace Advent2025;

public class Day04 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('.');
        int result = 0, sub = 0;
        do
        {
            sub = 0;
            foreach (KeyValuePair<(int x, int y), char> k in SimpleMap.Where(k => k.Value == '@'))
            {
                if (CountNeighbours(SimpleMap, k.Key.x, k.Key.y, '@') < 4)
                {
                    sub++;
                    StartingMap[k.Key] = '.';
                }
            }
            result += sub;
            SimpleMap = new(StartingMap);
        } while (sub > 0 && WhichPart == 2);

        Output = result.ToString();
    }
}
