namespace Advent2015;

public partial class Day20 : Advent.Day
{
    /*
        *  Description -   The elves are delivering presents.  The first elf delivers to all houses, the second to every second house and so on.
        *                  Each elf delivers 10 times his number of presents to each house.
        *                  
        *  Part 1 -        What is the lowerst house number to get at least the number of presents in the input?
        *  Part 2 -        As above, but each elf stops after fifty houses, and delivers 11 times their number each time.
    */

    public Day20(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        BatchStatus = DayBatchStatus.Performance;
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new List<string> { "140", "66", "67" };
                Expecteds = new List<string> { "8", "4", "4" };
                break;
            case (1, false):
                AddInput("33100000");
                Expecteds.Add("776160");
                break;
            case (2, true):
                Inputs = new List<string> { "140", "66", "67" };
                Expecteds = new List<string> { "8", "4", "4" };
                break;
            case (2, false):
                AddInput("33100000");
                Expecteds.Add("786240");
                break;
        }
    }
}
