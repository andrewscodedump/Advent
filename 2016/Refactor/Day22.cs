namespace Advent2016;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        if (TestMode) return;

        List<Node> used = new(), avail = new();
        Dictionary<Point, int> grid = new();

        PopulateLists(used, avail, grid);

        List<Point> numbers = new();
        int currPointer = 0;
        long currUsed = -1;
        int number = 0;
        do
        {
            if (used[currPointer].Used == currUsed)
                number++;

            if (used[currPointer].Used != currUsed || currPointer == used.Count - 1)
            {
                if (currUsed != -1)
                    numbers.Add(new Point((int)currUsed, number + 1));
                currUsed = used[currPointer].Used;
                number = 0;
            }
            currPointer++;
        } while (currPointer < used.Count);

        // for each value in numbers
        int availPointer = 0;
        int totalNumber = 0;
        foreach (Point pt in numbers)
        {
            // find first in avail with same or greater space
            // avail = howmany * (avail.len - avail[pos] - howmany) (since all of the howmany will always be in the ones with enough)
            while (availPointer < avail.Count && avail[availPointer].Avail < pt.X)
            {
                availPointer++;
            }
            number = used.Count - availPointer;
            totalNumber += (number - 1) * pt.Y;
        }

        Output = "OutputVariable".ToString();
    }

    private class Node
    {
        public Node(long x, long y, long avail, long used)
        {
            Avail = avail;
            Used = used;
            (X, Y) = (x, y);
        }

        public long Avail { get; set; }
        public long Used { get; set; }
        public long X { get; private set; }
        public long Y { get; private set; }

    }

    /// Populate Lists (initially identical)
    private void PopulateLists(List<Node> used, List<Node> avail, Dictionary<Point, int> grid)
    {
        long maxX = 0;
        long maxY = 0;

        foreach (long[] nums in InputNumbers)
        {
            Node newNode = new(nums[0], nums[1], nums[4], nums[3]);
            used.Add(newNode);
            avail.Add(newNode);
            maxX = Math.Max(maxX, newNode.X);
            maxY = Math.Max(maxY, newNode.Y);
            grid.Add(new Point((int)newNode.X, (int)newNode.Y), (int)newNode.Used);
        }

        if (Part2)
        {
            for (int y = 0; y < maxY; y++)
            {
                string line = "";
                for (int x = 0; x < maxX; x++)
                {
                    Point sqr = new(x, y);
                    int size = grid[sqr];
                    line += size.ToString() + (x == maxX - 1 ? "" : ", ");
                }
                Debug.Print(line);
            }
            return;
        }

        // Sort Lists
        used.Sort(delegate (Node a, Node b) { return a.Used.CompareTo(b.Used); });
        avail.Sort(delegate (Node a, Node b) { return a.Avail.CompareTo(b.Avail); });
    }

}
