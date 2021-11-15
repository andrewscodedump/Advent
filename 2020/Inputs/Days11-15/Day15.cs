namespace Advent2020;

public partial class Day15 : Advent.Day
{
    /*
        *  Description -   Input is a list of starting numbers for a game.  Next number is 0 if the last number hasn't appeared before, otherwise how long ago it did.
        *  
        *  Part 1 -        What is the last number after 2020 turns?
        *  Part 2 -        Ditto, but for 30000000 turns.
    */
    public Day15(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "0,3,6", "1,3,2", "2,1,3", "1,2,3", "2,3,1", "3,2,1", "3,1,2" };
                Expecteds = new() { "436", "1", "10", "27", "78", "438", "1836" };
                break;
            case (1, false):
                Inputs.Add("8,0,17,4,1,12");
                Expecteds.Add("981");
                break;
            case (2, true):
                Inputs = new() { "0,3,6", "1,3,2", "2,1,3", "1,2,3", "2,3,1", "3,2,1", "3,1,2" };
                Expecteds = new() { "175594", "2578", "3544142", "261214", "6895259", "18", "362" };
                break;
            case (2, false):
                Inputs.Add("8,0,17,4,1,12");
                Expecteds.Add("164878");
                break;
        }
    }
}
