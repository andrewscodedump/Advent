namespace Advent2016;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int direction = 0;
        (int x, int y) position = (0,0);
        int[] hSteps = new int[] { 0, 1, 0, -1 }, vSteps = new int[] { 1, 0, -1, 0 };
        bool gotThere = false;
        Dictionary<(int, int), bool> visited = new() { { position, true } };

        string[] steps = Inputs[0].Split(", ", StringSplitOptions.None);

        foreach (string step in steps)
        {
            direction = (direction + (step.StartsWith("R") ? 1 : 3)) % 4;

            if (Part1)
            {
                position.x += int.Parse(step[1..]) * hSteps[direction];
                position.y += int.Parse(step[1..]) * vSteps[direction];
            }
            else
            {
                for (int i = 1; i <= int.Parse(step[1..]) && !gotThere; i++)
                {
                    position.x += hSteps[direction];
                    position.y += vSteps[direction];
                    if (visited.ContainsKey(position))
                        gotThere = true;
                    else
                        visited.Add(position, true);
                }
            }
            if (gotThere) break;
        }

        Output = (Math.Abs(position.x) + Math.Abs(position.y)).ToString();
    }
}
