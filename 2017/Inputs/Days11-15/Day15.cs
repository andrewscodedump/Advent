namespace Advent2017;

public partial class Day15 : Advent.Day
{
    /*
        *  Description -   Input is a pair of seed values for a series of manipulations.  Multiply this by a given value then take modulo 7FFFFFFF.
        *                  Repeat, taking the output of the previous round as the input of the next.
        *                  After each round, compare the 16 LSBs of the two machines - pass if they are equal.
        *  
        *  Part 1 -        How many matches after 40M iterations?
        *  Part 2 -        As part 1, but 5M iterations, and machine only passes value for comparison if it is a multiple of 4 for machine 1 and 8 for machine 2. 
    */
    public Day15(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("65;8921");
                Expecteds.Add("588");
                break;
            case (1, false):
                AddInput("116;299");
                Expecteds.Add("569");
                break;
            case (2, true):
                AddInput("65;8921");
                Expecteds.Add("309");
                break;
            case (2, false):
                AddInput("116;299");
                Expecteds.Add("298");
                break;
        }
    }
}
