namespace Advent2016;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        if (TestMode && Part2) return;
        int bestSteps = int.MaxValue;
        Location currentRoom = new(1, 1, 0);
        Point target = TestMode ? new Point(7, 4) : new Point(31, 39);
        Queue bfs = new();
        bfs.Enqueue(currentRoom);
        HashSet<Point> alreadyVisited = new();

        do
        {
            currentRoom = (Location)bfs.Dequeue();
            // It's not a room, or it is but we've already been here - skip to the next one
            if (!IsRoom(currentRoom, int.Parse(Input)) || alreadyVisited.Contains(currentRoom.Position))
                continue;

            if (Part1)
            {
                // If we're already past the best one, don't do any more
                if (currentRoom.Steps >= bestSteps)
                    continue;
                // Check if we've reached the target
                if (currentRoom.Position == target)
                {
                    bestSteps = Math.Min(currentRoom.Steps, bestSteps);
                    continue;
                }
            }

            if (Part2 && currentRoom.Steps > 50)
                continue;

            alreadyVisited.Add(currentRoom.Position);

            bfs.Enqueue(currentRoom.Up());
            bfs.Enqueue(currentRoom.Down());
            bfs.Enqueue(currentRoom.Left());
            bfs.Enqueue(currentRoom.Right());

        } while (bfs.Count > 0);

        Output = Part1 ? bestSteps.ToString() : alreadyVisited.Count.ToString();
    }

    private class Location
    {
        public Location(int x, int y, int steps) { Position = new Point(x, y); Steps = steps; }
        public Point Position { get; set; }
        public int Steps { get; set; }
        public Location Up() => new(Position.X, Position.Y - 1, Steps + 1);
        public Location Down() => new(Position.X, Position.Y + 1, Steps + 1);
        public Location Left() => new(Position.X - 1, Position.Y, Steps + 1);
        public Location Right() => new(Position.X + 1, Position.Y, Steps + 1);
    }

    private static bool IsRoom(Location location, int favNum)
    {
        int x = location.Position.X;
        int y = location.Position.Y;
        if (x < 0 || y < 0)
            return false;
        int sum = 0;
        int calc = (x * x) + (3 * x) + (2 * x * y) + y + (y * y) + favNum;
        string bin = Convert.ToString(calc, 2);
        foreach (char c in bin)
        {
            sum += int.Parse(c.ToString());
        }
        return sum % 2 == 0;
    }
}
