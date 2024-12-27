﻿namespace Advent2019;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        int phases = TestMode && Input == "12345678" ? 4 : 100;
        int[] baseMultipliers = [0, 1, 0, -1];
        int len = Input.Length;
        int end = int.Parse(Input[..7]);
        string work = Part1 
            ? Input 
            : string.Concat(string.Concat(Enumerable.Repeat(ReverseString(Input), ((len * 10000) - end) / len)), ReverseString(Input).AsSpan(0, ((len * 10000) - end) % len));
        int digits = work.Length;

        #endregion Setup Variables and Parse Inputs

        if (Part1)
            for (int p = 0; p < phases; p++)
            {
                StringBuilder sb = new();
                for (int d = 0; d < digits; d++)
                {
                    int calc = 0;
                    for (int c = 0; c < digits; c++)
                        calc += int.Parse(work[c].ToString()) * baseMultipliers[(c + 1) / (d + 1) % 4];
                    sb.Append(Math.Abs(calc) % 10);
                }
                work = sb.ToString();
            }

        else
            for (int p = 0; p < phases; p++)
            {
                StringBuilder sb = new();
                int runningTotal = 0;
                for (int d = 0; d < digits; d++)
                {
                    runningTotal += int.Parse(work[d].ToString());
                    sb.Append(runningTotal % 10);
                }
                work = sb.ToString();
            }
        Output = Part1 ? work[..8] : ReverseString(work[(digits - 8)..]);
    }
}
