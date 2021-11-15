namespace Advent2016;

public partial class Day16 : Advent.Day
{
    /*
        *  Description -    You're using an algorithm to generate random data to scrub a disk.
        *                   Starting from the input, copy the string, reverse it, change all the 0s to 1s and 1s to zeros, then the
        *                   output will be the starting string plus a 0 plus the mutated string.  Repeat until it is long enough to fill the disk.
        *                   The checksum is calculated by trimming the final string to fit the disk and comparing pairs of characters; if they match, return 1 else 0.  Repeat until the checksum has an odd number of digits.
        *  
        *  Part 1 -         The disk has a capacity of 272.  What is the checksum?
        *  Part 2 -         As above, but the capacity is 35651584.
    */
    public Day16(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("20;10000");
                Expecteds.Add("01100");
                break;
            case (1, false):
                Inputs.Add("272;11101000110010100");
                Expecteds.Add("10100101010101101");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                Inputs.Add("35651584;11101000110010100");
                Expecteds.Add("01100001101101001");
                break;
        }
    }
}
