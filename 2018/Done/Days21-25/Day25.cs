namespace Advent2018;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        List<MyPoint> points = new();
        int nextSetNumber = 0;
        foreach (string coord in InputSplit)
        {
            string[] coords = coord.Split(new char[] { ',' });
            points.Add(new MyPoint(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]), int.Parse(coords[3]), -1));
        }

        for (int first = 0; first < points.Count; first++)
            for (int second = 0; second < points.Count; second++)
            {
                int set1 = points[first].Set, set2 = points[second].Set;
                if (points[first].Dist(points[second]) <= 3)    // within distance
                {
                    if (set1 == -1 && set2 == -1)               // neither in set
                        points[first].Set = points[second].Set = nextSetNumber++;
                    else if (set1 == -1 && set2 != -1)          // second one only in set
                        points[first].Set = points[second].Set;
                    else if (set1 != -1 && set2 == -1)          // first one only in set
                        points[second].Set = points[first].Set;
                    else if (set1 == set2) { }                  // both in same set
                    else                                        // both in different sets
                        foreach (MyPoint point in points)
                            if (point.Set == set2) point.Set = set1;
                }

            }

        Output = points.GroupBy(p => p.Set).Count().ToString();
    }

    #region Private Classes and Methods
    private class MyPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int T { get; set; }
        public int Set { get; set; }
        public MyPoint(int x, int y, int z, int t, int set) { X = x; Y = y; Z = z; T = t; Set = set; }
        public int Dist(MyPoint other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z) + Math.Abs(T - other.T);
    }
    #endregion Private Classes and Methods
}
