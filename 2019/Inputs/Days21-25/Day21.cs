namespace Advent2019;

public partial class Day21 : Advent.Day
{
    /*
        *  Description -    Input is an IntCode program for controlling a droid.  The droid can detect (and report) where there are holes ahead of it.
        *                   You can tell it when and how far to jump.
        *  
        *  Part 1 -         How much damage does the droid detect?
        *  Part 2 - 
    */
    public Day21(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        BatchStatus = DayBatchStatus.NotDoneYet;
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("Part One, Test");
                Expecteds.Add("Part One, Test");
                break;
            case (1, false):
                Inputs.Add("Part One, Live");
                Expecteds.Add("Part One, Live");
                break;
            case (2, true):
                Inputs.Add("Part Two, Test");
                Expecteds.Add("Part Two, Test");
                break;
            case (2, false):
                Inputs.Add("Part Two, Live");
                Expecteds.Add("Part Two, Live");
                break;
        }
    }
}
