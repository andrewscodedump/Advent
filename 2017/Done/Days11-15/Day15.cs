﻿namespace Advent2017;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        long gen1Value = InputNumbers[0][0];
        long gen2Value = InputNumbers[1][0];
        int matches = 0;

        for (int i = 0; i < (Part1 ? 40_000_000 : 5_000_000); i++)
        {
            do
                gen1Value = gen1Value * 16807 % int.MaxValue;
            while (Part2 && gen1Value % 4 != 0);
            do
                gen2Value = gen2Value * 48271 % int.MaxValue;
            while (Part2 && gen2Value % 8 != 0);

            if ((gen1Value & 0xFFFF) == (gen2Value & 0xFFFF))
                matches++;
        }
        Output = matches.ToString();
    }

}
