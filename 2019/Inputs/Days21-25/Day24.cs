namespace Advent2019;

public partial class Day24 : Advent.Day
{
    /*
        *  Description -    Input is a starting grid for a cellular automaton.
        *  
        *  Part 1 -         Using powers of 2 for each cell, what is the value of the first grid layout which repeats?
        *  Part 2 - 
    */
    public Day24(bool testMode, int whichPart) : base(testMode, whichPart)
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
