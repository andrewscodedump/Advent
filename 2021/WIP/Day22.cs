namespace Advent2021;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        //Dictionary<(int x, int y, int z), bool> map = new();
        HashSet<(int x, int y, int z)> map = new();
        long result = 0;

        foreach (string line in InputSplit)
        {
            string action = line[..3].Trim();
            string[] parts = line.Split(new string[] { "on x=", "off x=", "..", ",y=", ",z=" }, StringSplitOptions.RemoveEmptyEntries);
            (int xmin, int xmax, int ymin, int ymax, int zmin, int zmax) = (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
            if (WhichPart == 1)
            {
                xmin = Math.Max(xmin, -50); xmax = Math.Min(xmax, 50);
                ymin = Math.Max(ymin, -50); ymax = Math.Min(ymax, 50);
                zmin = Math.Max(zmin, -50); zmax = Math.Min(zmax, 50);
            }
            for (int x = xmin; x <= xmax; x++)
                for (int y = ymin; y <= ymax; y++)
                    for (int z = zmin; z <= zmax; z++)
                    {
                        if (map.Contains((x, y, z)) && action == "off") map.Remove((x, y, z));
                        else if (action == "on") map.Add((x, y, z));
                    }
            result = map.Count;
        }

        Output = result.ToString();
    }
}
