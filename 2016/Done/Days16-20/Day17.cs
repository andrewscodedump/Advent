namespace Advent2016;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        Queue theQueue = new();
        State currentPos = new();
        theQueue.Enqueue(currentPos);
        string shortestPath = "";
        int longest = 0;
        do
        {
            State check = (State)theQueue.Dequeue();
            if (check.Position == new Point(3, 3))
            {
                if (Part1 && (check.Path.Length < shortestPath.Length || string.IsNullOrEmpty(shortestPath)))
                {
                    shortestPath = check.Path;
                    break;
                }
                else if (Part2)
                    if (check.Path.Length > longest)
                        longest = check.Path.Length;
                continue;
            }
            string hash = GetMD5Hash(MD5, Input + check.Path);
            // Get available doors
            if ("bcdef".Contains(hash[0]) && check.Position.Y > 0)
                theQueue.Enqueue(new State(check.Path + "U", check.Position.X, check.Position.Y - 1));
            if ("bcdef".Contains(hash[1]) && check.Position.Y < 3)
                theQueue.Enqueue(new State(check.Path + "D", check.Position.X, check.Position.Y + 1));
            if ("bcdef".Contains(hash[2]) && check.Position.X > 0)
                theQueue.Enqueue(new State(check.Path + "L", check.Position.X - 1, check.Position.Y));
            if ("bcdef".Contains(hash[3]) && check.Position.X < 3)
                theQueue.Enqueue(new State(check.Path + "R", check.Position.X + 1, check.Position.Y));
        } while (theQueue.Count > 0);

        Output = Part1 ? shortestPath.ToString() : longest.ToString();
    }

    private class State
    {
        public State() { }
        public State(string path, int x, int y)
        {
            Path = path;
            Position = new Point(x, y);
        }
        public string Path = "";
        public Point Position = new(0, 0);
    }
}
