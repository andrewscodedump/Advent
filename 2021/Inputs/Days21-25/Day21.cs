namespace Advent2021;

public partial class Day21 : Advent.Day
{
    /*
     *  Description -   
     *  
     *  Part 1 -        
     *  Part 2 -        
    */
    public Day21(bool testMode, int whichPart) : base(testMode, whichPart)
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
