namespace Advent2019;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        List<(string planet, string satellite)> orbits = new();
        foreach (string orbit in Inputs)
            orbits.Add((orbit.Split(')')[0], orbit.Split(')')[1]));

        #endregion Setup Variables and Parse Inputs

        Dictionary<string, int>[] paths = new Dictionary<string, int>[2] { GetPaths(new Dictionary<string, int>(), "SAN", 0, orbits), GetPaths(new Dictionary<string, int>(), "YOU", 0, orbits) };
        List<string> commonPaths = paths[0].Keys.Intersect(paths[1].Keys).ToList();

        Output = (Part1 ? GetChildren("COM", 0, orbits) : commonPaths.Count > 0 ? commonPaths.Min(d => paths[0][d] + paths[1][d]) : 0).ToString();
    }

    #region Private Classes and Methods

    private Dictionary<string, int> GetPaths(Dictionary<string, int> paths, string curr, int dist, List<(string planet, string satellite)> orbits)
    {
        string parent = orbits.FirstOrDefault(o => o.satellite == curr).planet;
        if (!string.IsNullOrEmpty(parent))
        {
            paths[parent] = dist++;
            paths = GetPaths(paths, parent, dist, orbits);
        }
        return paths;
    }

    private int GetChildren(string parent, int count, List<(string planet, string satellite)> orbits)
    {
        int childCount = count++;
        foreach ((string, string) child in orbits.Select(o => o).Where(o => o.planet == parent).ToList())
            childCount += GetChildren(child.Item2, count, orbits);
        return childCount;
    }
    #endregion Private Classes and Methods
}
