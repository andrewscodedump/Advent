namespace Advent2017;

public partial class Day03 : Advent.Day
{
    /*
        *  Description - Form a grid by doing a spiral starting at 0,0 with 1 and then right, up, left, down, etc.
        *  
        *  Part 1 - Give Manhattan distance from 0,0 to the square where the input is found
        *  Part 2 - As 1, but grid is populated from sum of all neighbours of the square.  Distance is to first square where value is greater than the input
    */
    public Day03(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new List<string> { "1", "12", "23", "1024" };
                Expecteds = new List<string> { "0", "3", "2", "31" };
                break;
            case (1, false):
                Inputs.Add("368078");
                Expecteds.Add("371");
                break;
            case (2, true):
                Inputs = new List<string> { "1", "12", "23", "1024" };
                Expecteds = new List<string> { "1", "23", "25", "1968" };
                break;
            case (2, false):
                Inputs.Add("368078");
                Expecteds.Add("369601");
                break;
        }
    }
}
