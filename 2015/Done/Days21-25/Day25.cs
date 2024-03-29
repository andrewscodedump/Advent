﻿namespace Advent2015;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        if (Part2) return;

        Point target = new((int)InputNumbers[0][0], (int)InputNumbers[0][1]);
        Point currentPos = new(1, 1);
        long currentValue = 20151125;
        do
        {
            currentPos = currentPos.X == 1 ? new Point(currentPos.Y + 1, 1) : new Point(currentPos.X - 1, currentPos.Y + 1);
            currentValue = currentValue * 252533 % 33554393;
        } while (currentPos != target);
        Output = currentValue.ToString();
    }
}
