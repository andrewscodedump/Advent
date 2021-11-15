namespace Advent2021;

public partial class Day04 : Advent.Day
{
    /*
     *  Description -   
     *  
     *  Part 1 -        
     *  Part 2 -        
    */
    public Day04(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
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
