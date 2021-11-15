namespace Advent2020;

public partial class Day25 : Advent.Day
{
    /*
        *  Description -   Input is the public keys for a door and a card.  Apply a looping hash to a starting value of 1.
        *  
        *  Part 1 -        Find the number of loops to produce the PK for one of them and then apply this number of loops to the other one's PK to get the encryption key.
        *  Part 2 -        Complete both parts of all proceeding problems!!
    */

    public Day25(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("5764801;17807724");
                Expecteds.Add("14897079");
                break;
            case (1, false):
                Inputs.Add("8421034;15993936");
                Expecteds.Add("9177528");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
            case (2, false):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
        }
    }
}
