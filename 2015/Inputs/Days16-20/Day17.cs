namespace Advent2015;

public partial class Day17 : Advent.Day
{
    /*
        *  Description -   The Input gives various sizes of container that you need to fit into a 150l fridge (25l in the test data).
        *  
        *  Part 1 -        How many different ways are there of reaching capacity?
        *  Part 2 -        How many different ways are there of filling it using the minimum possible number of containers?
    */

    public Day17(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("20,15,10,5,5");
                Expecteds.Add("4");
                break;
            case (1, false):
                Inputs.Add("11,30,47,31,32,36,3,1,5,3,32,36,15,11,46,26,28,1,19,3");
                Expecteds.Add("4372");
                break;
            case (2, true):
                Inputs.Add("20,15,10,5,5");
                Expecteds.Add("3");
                break;
            case (2, false):
                Inputs.Add("11,30,47,31,32,36,3,1,5,3,32,36,15,11,46,26,28,1,19,3");
                Expecteds.Add("4");
                break;
        }
    }
}
