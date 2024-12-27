﻿namespace Advent2016;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int direction = 0;
        (int x, int y) position = (0,0);
        int[] hSteps = [0, 1, 0, -1], vSteps = [1, 0, -1, 0];
        bool gotThere = false;
        Dictionary<(int, int), bool> visited = new() { { position, true } };

        string[] steps = Input.Split(", ", StringSplitOptions.None);

        foreach (string step in steps)
        {
            direction = (direction + (step.StartsWith('R') ? 1 : 3)) % 4;

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
                    if (!visited.TryAdd(position, true))
                        gotThere = true;
                }
            }
            if (gotThere) break;
        }

        Output = (Math.Abs(position.x) + Math.Abs(position.y)).ToString();
    }
}
