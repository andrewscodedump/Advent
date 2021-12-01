namespace Advent2015;

public partial class Day04 : Advent.Day
{
    /*
        *  Description -   The input gives a secret key used as the basis for a hashing algorithm.  The MD5 hash of the key plus an increasing integer is taken.
        *  
        *  Part 1 -        What is the first hash to start with five zeros?
        *  Part 2 -        As part one but with six zeros?
    */

    public Day04(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "abcdef", "pqrstuv" };
                Expecteds = new() { "609043", "1048970" };
                break;
            case (1, false):
                AddInput("ckczppom");
                Expecteds.Add("117946");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("ckczppom");
                Expecteds.Add("3938038");
                break;
        }
    }
}
