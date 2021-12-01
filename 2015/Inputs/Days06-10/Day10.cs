namespace Advent2015;

public partial class Day10 : Advent.Day
{
    public Day10(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        /*
            *  Description -   The input is the start point for a "look and say" game.  1 becomes 11 (one one), 11 becomes 21 (two ones) etc.
            *                  
            *  Part 1 -        What is the length of the result after applying the rules 40 times?
            *  Part 2 -        What is the length of the result after applying the rules 50 times?
        */

        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("1");
                Expecteds.Add("6");
                break;
            case (1, false):
                AddInput("1113122113");
                Expecteds.Add("360154");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("1113122113");
                Expecteds.Add("5103798");
                break;
        }
    }
}
