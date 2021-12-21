namespace Advent2021;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        // populate translations
        Dictionary<(int, int), int> translations = new();
        Dictionary<(int, int), int> map = new();

        // populate map
        int rounds = 2;
        for (int i = 0; i < rounds; i++)
        {
            map = Process(map);
        }

        int result = map.Values.Sum();
        Output = result.ToString();
    }

    public Dictionary<(int,int),int> Process(Dictionary<(int, int), int> map)
    {
        Dictionary<(int, int), int> copy = new(map);
        ExtendMap(copy);
        // loop from minx-1,miny-1 to maxx+1, maxy=1
        // processpixel
        return new(copy);
    }

    Dictionary<(int, int), int> ExtendMap(Dictionary<(int, int), int> map)
    {
        // add 2 lines of 0s to each side
        return new(map);
    }

    public int ProcessPixel(Dictionary<(int, int), int> map, (int x, int y) pos)
    {
        int output = '0';
        // process
        return output;
    }
}
