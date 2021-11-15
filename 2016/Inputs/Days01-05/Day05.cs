namespace Advent2016;

public partial class Day05 : Advent.Day
{
    /*
        * Description -    Get MD5 hash of input plus increasing integer.  Only consider hashes wher first five characters are zero.
        *                  For part 1 the required output is 6th character of hash; for part 2, the 6th character gives the position in the password to fill (ignore any >7), 7th gives the character to use
        *                  
        * Part 1-          Get password build from first 8 valid hashes
        * Part 2 -         Get password, using only first valid entry for each position
        * 
    */

    public Day05(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("abc");
                Expecteds.Add("18f47a30");
                break;
            case (1, false):
                Inputs.Add("abbhdwsy");
                Expecteds.Add("801b56a7");
                break;
            case (2, true):
                Inputs.Add("abc");
                Expecteds.Add("05ace8e3");
                break;
            case (2, false):
                Inputs.Add("abbhdwsy");
                Expecteds.Add("424a0197");
                break;
        }
    }
}
