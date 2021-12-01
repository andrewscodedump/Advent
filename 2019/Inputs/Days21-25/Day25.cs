namespace Advent2019;

public partial class Day25 : Advent.Day
{
    /*
        *  Description -    Input is an IntCode program for communicating with a droid.  You need to give it instructions for navigating through a ship, and receive descriptions
        *                   of what it encounters.
        *  
        *  Part 1 -         What is the password for the ship's airlock?
        *  Part 2 - 
    */
    public Day25(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        BatchStatus = DayBatchStatus.NotDoneYet;
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Part One, Test");
                Expecteds.Add("Part One, Test");
                break;
            case (1, false):
                AddInput("Part One, Live");
                Expecteds.Add("Part One, Live");
                break;
            case (2, true):
                AddInput("Part Two, Test");
                Expecteds.Add("Part Two, Test");
                break;
            case (2, false):
                AddInput("Part Two, Live");
                Expecteds.Add("Part Two, Live");
                break;
        }
    }
}
