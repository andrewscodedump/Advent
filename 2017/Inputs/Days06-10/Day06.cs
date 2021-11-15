namespace Advent2017;

public partial class Day06 : Advent.Day
{
    /*
        *  Description -   Input is a list of values.  Find the largest, clear it and distribute it among the others one at a time, starting at the next position and looping back at the end
        *  
        *  Part 1 -        How many iterations until a pattern is repeated?
        *  Part 2 -        After finding a repeat, how many iterations until it's repeated again?
    */

    public Day06(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("0;2;7;0");
                Expecteds.Add("9");
                break;
            case (1, false):
                Inputs.Add("5;1;10;0;1;7;13;14;3;12;8;10;7;12;0;6");
                Expecteds.Add("5042");
                break;
            case (2, true):
                Inputs.Add("0;2;7;0");
                Expecteds.Add("4");
                break;
            case (2, false):
                Inputs.Add("5;1;10;0;1;7;13;14;3;12;8;10;7;12;0;6");
                Expecteds.Add("1086");
                break;
        }
    }
}
