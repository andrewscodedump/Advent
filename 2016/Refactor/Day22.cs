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
        int currUsed = -1;
        int number = 0;
        do
        {
            if (used[currPointer].Used == currUsed)
                number++;

            if (used[currPointer].Used != currUsed || currPointer == used.Count - 1)
            {
                if (currUsed != -1)
                    numbers.Add(new Point(currUsed, number + 1));
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
        public Node(string address, int avail, int used)
        {
            Address = address;
            Avail = avail;
            Used = used;
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                foreach (string bit in address.Split('-'))
                {
                    if (bit.StartsWith("x"))
                        X = int.Parse(bit[1..]);
                    else if (bit.StartsWith("y"))
                        Y = int.Parse(bit[1..]);
                }
            }
        }
        public int Avail { get; set; }
        public int Used { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

    }

    /// Populate Lists (initially identical)
    private void PopulateLists(List<Node> used, List<Node> avail, Dictionary<Point, int> grid)
    {
        int maxX = 0;
        int maxY = 0;

        foreach (string line in InputSplit)
        {
            if (!line.StartsWith("/dev/grid"))
                continue;

            string[] cols = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Node newNode = new(cols[0], int.Parse(cols[3][0..^1]), int.Parse(cols[2][0..^1]));
            used.Add(newNode);
            avail.Add(newNode);
            maxX = Math.Max(maxX, newNode.X);
            maxY = Math.Max(maxY, newNode.Y);
            grid.Add(new Point(newNode.X, newNode.Y), newNode.Used);
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
