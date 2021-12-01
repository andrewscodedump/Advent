namespace Advent2015;

public partial class Day11 : Advent.Day
{
    /*
        *  Description -   Input is a password that needs changed.  It is changed by incrementing the rightmost letters, wrapping round at z and then checking it's valid.
        *                  A password is valid if it includes one increasing straight of at least three letters; does not contain the letters i, o, or l; contains at least two different, non-overlapping pairs of letters.
        *                  
        *  Part 1 -        What is the next valid password?
        *  Part 2 -        What is the next one after the one we've just found?
    */

    public Day11(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new List<string> { "abcdefgh", "ghijklmn" };
                Expecteds = new List<string> { "abcdffaa", "ghjaabcc" };
                break;
            case (1, false):
                AddInput("hepxcrrq");
                Expecteds.Add("hepxxyzz");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("hepxxyzz");
                Expecteds.Add("heqaabcc");
                break;
        }
    }
}
