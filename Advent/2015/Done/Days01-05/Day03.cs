namespace Advent2015;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        Point currentSanta = new(0, 0), currentRobot = new(0, 0);
        HashSet<Point> visited = [currentSanta];

        for (int i = 0; i < Input.Length; i++)
        {
            char direction = Input[i];
            if (Part1 || i % 2 == 1)
            {
                currentSanta.X += Directions[direction].x;
                currentSanta.Y += Directions[direction].y;
                visited.Add(currentSanta);
            }
            else
            {
                currentRobot.X += Directions[direction].x;
                currentRobot.Y += Directions[direction].y;
                visited.Add(currentRobot);
            }
        }
        Output = visited.Count.ToString();
    }
}
