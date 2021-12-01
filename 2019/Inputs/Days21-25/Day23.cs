namespace Advent2019;

public partial class Day23 : Advent.Day
{
    /*
        *  Description -    Input is IntCode for a Network Controller.  There are 50 computers talking to each other using this code.  You start each computer by sending it its address.
        *                   They will then send packets to send to other computers.
        *  
        *  Part 1 -         What is the Y value of the first packet sent to address 255?
        *  Part 2 - 
    */
    public Day23(bool testMode, int whichPart) : base(testMode, whichPart)
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
