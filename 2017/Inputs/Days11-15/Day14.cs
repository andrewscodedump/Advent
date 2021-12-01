﻿namespace Advent2017;

public partial class Day14 : Advent.Day
{
    /*
        *  Description -   Input is a string that has to be hashed 128 times using the algorithm from day 10 part 2.  The hex results should be converted to binary.
        *  
        *  Part 1 -        How many 1s are there in the results?
        *  Part 2 -        Placing the 128 results in an array, how many contiguous blocks of 1s are there (no diagonals)?
    */
    public Day14(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("");
                Expecteds.Add("8198");
                break;
            case (1, false):
                AddInput("stpzcrnm");
                Expecteds.Add("8250");
                break;
            case (2, true):
                AddInput("flqrgnkx");
                Expecteds.Add("1242");
                break;
            case (2, false):
                AddInput("stpzcrnm");
                Expecteds.Add("`1113");
                break;
        }
    }

}
