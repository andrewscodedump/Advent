namespace Advent2018;

public partial class Day22 : Advent.Day
{
    /*
        *  Description -   The structure of the ground under your feet (to a depth given in the input) is derived from a convoluted algorithm.
        *                  Depending on the nature of the ground, you may need special equipment.  It takes time to move from one area to another
        *                  and to equip/dequip.
        *  
        *  Part 1 -        What is the score of the ground (calculated from the number of each type)?
        *  Part 2 -        What is the fastest time to get to the target point (given in the input)?
    */
    public Day22(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("depth: 510;target: 10,10");
                Expecteds.Add("114");
                break;
            case (1, false):
                Inputs.Add("depth: 8103;target: 9,758");
                Expecteds.Add("7743");
                break;
            case (2, true):
                Inputs.Add("depth: 510;target: 10,10");
                Expecteds.Add("45");
                break;
            case (2, false):
                Inputs.Add("depth: 8103;target: 9,758");
                Expecteds.Add("1029");
                break;
        }
    }
}
