namespace Advent2017;

public partial class Day17 : Advent.Day
{
    /*
        *  Description -   Input is a step length which a process will take.  It starts at position 0, inserts 0 then steps (wrapping at the end), inserts 1 and so on.
        *  
        *  Part 1 -        What is the value in the position after that in which 2017 is inserted?
        *  Part 2 -        What value is after 0 after 50,000,000 steps?
    */
    public Day17(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("3");
                Expecteds.Add("638");
                break;
            case (1, false):
                AddInput("316");
                Expecteds.Add("180");
                break;
            case (2, true):
                AddInput("3");
                Expecteds.Add("1222153");
                break;
            case (2, false):
                AddInput("316");
                Expecteds.Add("13326437");
                break;
        }
    }
}
