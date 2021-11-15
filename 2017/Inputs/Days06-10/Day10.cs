namespace Advent2017;

public partial class Day10 : Advent.Day
{
    /*
        *  Description -   Start with a list of bytes.  Input is a list of seeds for an obfuscation routine.
        *                  Start at the beginning, and for each seed in turn, take that number of characters (wrapping at the end) and reverse them.
        *                  After each reversal, move to the end of the reversed bit and then a bit more (the bit more starting at 0 and increasing by one every time).
        *  
        *  Part 1 -        After all the manipulations, what is the result of multiplying the first and second numbers.
        *  Part 2 -        Instead of treating the input as a list of numbers, treat it as a string and the seeds will be the ASCII values of each character (including the spaces and the commas).
        *                  After applying all the manipulations, hash the result by XORing the output in blocks of 16 bytes and output the results as hex.
    */

    public Day10(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("3, 4, 1, 5");
                Expecteds.Add("12");
                break;
            case (1, false):
                Inputs.Add("165,1,255,31,87,52,24,113,0,91,148,254,158,2,73,153");
                Expecteds.Add("4114");
                break;
            case (2, true):
                Inputs = new List<string> { "", "AoC 2017", "1,2,3", "1,2,4" };
                Expecteds = new List<string> { "a2582a3a0e66e6e86e3812dcb672a272", "33efeb34ea91902bb2f59c9920caa6cd", "3efbe78a8d82f29979031a4aa0b16a9d", "63960835bcdc130f0b66d7ff4f6a5a8e" };
                break;
            case (2, false):
                Inputs.Add("165,1,255,31,87,52,24,113,0,91,148,254,158,2,73,153");
                Expecteds.Add("2f8c3d2100fdd57cec130d928b0fd2dd");
                break;
        }
    }
}
