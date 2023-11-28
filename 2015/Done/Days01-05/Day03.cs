namespace Advent2015;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        Point currentSanta = new(0, 0);
        Point currentRobot = new(0, 0);
        List<Point> visited = new() { currentSanta };

        for (int i = 0; i < Inputs[0].Length; i++)
        {
            char direction = Inputs[0][i];
            if (Part1 || i % 2 == 1)
            {
                currentSanta.Y += direction == '^' ? 1 : direction == 'v' ? -1 : 0;
                currentSanta.X += direction == '>' ? 1 : direction == '<' ? -1 : 0;
                if (!visited.Contains(currentSanta))
                    visited.Add(currentSanta);
            }
            else
            {
                currentRobot.Y += direction == '^' ? 1 : direction == 'v' ? -1 : 0;
                currentRobot.X += direction == '>' ? 1 : direction == '<' ? -1 : 0;
                if (!visited.Contains(currentRobot))
                    visited.Add(currentRobot);
            }
        }
        Output = visited.Count.ToString();
    }
}
