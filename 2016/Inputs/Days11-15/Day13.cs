namespace Advent2016;

public partial class Day13 : Advent.Day
{
    /*
        *  Description -    Generate a maze from the input:
        *                   Calculate x2 + 3x + 2xy + y + y2; convert to binary; count the 1s.  If the result is even, it's a space otherwise it's a wall.
        *  
        *  Part 1 -         What is the fewest steps to reach 31, 39
        *  Part 2 -         How many locations (including your start) can you reach in 50 steps?
    */
    public Day13(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("10");
                Expecteds.Add("11");
                break;
            case (1, false):
                AddInput("1350");
                Expecteds.Add("92");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("1350");
                Expecteds.Add("124");
                break;
        }
    }
}
