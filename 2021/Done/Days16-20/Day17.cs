﻿namespace Advent2021;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        long[] area = InputNumbers[0];

        long bestHeight = 0, v0 = 1;
        Dictionary<long, List<long>> validXTimes = new();
        HashSet<(long, long)> validVelocities = new();
        bool overshoot = false;

        // Get all the times where an x is in the range
        do
        {
            int time = 0;
            long xpos;
            do
            {
                time++;
                xpos = time < v0 ? (v0 * time) - (time * (time - 1) / 2) : (v0 * v0) - (v0 * (v0 - 1) / 2);
                if (xpos >= area[0] && xpos <= area[1])
                    if (validXTimes.ContainsKey(time))
                        validXTimes[time].Add(v0);
                    else
                        validXTimes.Add(time, new() { v0 });
                if (xpos>area[1] && time == 1) overshoot = true;
            } while (xpos < area[1] && time < 250 && !overshoot);
            v0++;
        } while (!overshoot);

        // Get all the ys where it's in the range at the same time as one of the xs.
        overshoot = false;
        v0 = area[2]-1;
        do
        {
            long ypos, time = v0 <= 0 ? 0 : (2 * v0) + 2, height = v0 <= 0 ? 0 : v0 * (v0 + 1) / 2;
            do
            {
                ypos = (v0 * time) - (time * (time - 1) / 2);
                if (ypos >= area[2] && ypos <= area[3])
                    if (validXTimes.ContainsKey(time))
                    {
                        bestHeight = Math.Max(bestHeight, height);
                        foreach (long x in validXTimes[time])
                            validVelocities.Add((x, v0));
                    }
                if (ypos < area[2] && time == (2*v0)+2) overshoot = true;
                time++;
            } while (ypos > area[2]  && !overshoot);
            v0++;
        } while (!overshoot);

        Output = (Part1 ? bestHeight : validVelocities.Count).ToString();
    }
}
