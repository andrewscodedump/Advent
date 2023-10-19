namespace Advent2015;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        Point currentSanta = new(0, 0);
        Point currentRobot = new(0, 0);
        List<Point> visited = new() { currentSanta };

        for (int i = 0; i < Input.Length; i++)
        {
            if (Part1 || i % 2 == 1)
            {
                currentSanta.Y += Input[i] == '^' ? 1 : Input[i] == 'v' ? -1 : 0;
                currentSanta.X += Input[i] == '>' ? 1 : Input[i] == '<' ? -1 : 0;
                if (!visited.Contains(currentSanta))
                    visited.Add(currentSanta);
            }
            else
            {
                currentRobot.Y += Input[i] == '^' ? 1 : Input[i] == 'v' ? -1 : 0;
                currentRobot.X += Input[i] == '>' ? 1 : Input[i] == '<' ? -1 : 0;
                if (!visited.Contains(currentRobot))
                    visited.Add(currentRobot);
            }
        }
        Output = visited.Count.ToString();
    }
}
