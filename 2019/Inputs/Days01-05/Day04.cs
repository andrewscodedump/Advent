namespace Advent2019;

public partial class Day04 : Advent.Day
{
    /*
        *  Description -   Input gives the upper and lower bounds of a numeric password, whose digits must be in ascending (or equal) order
        *  
        *  Part 1 -        The password must have a consecutive pair of digits.  How many in the range meet this criterion?
        *  Part 2 -        As above, but the pair must not be part of a larger set of the same digit.
    */
    public Day04(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (1, false):
                Inputs.Add("307237-769058");
                Expecteds.Add("889");
                break;
            case (2, false):
                Inputs.Add("307237-769058");
                Expecteds.Add("589");
                break;
        }
    }
}
