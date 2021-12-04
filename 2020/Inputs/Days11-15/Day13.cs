namespace Advent2020;

public partial class Day13 : Advent.Day
{
    /*
        *  Description -   Input is a time and a list of bus IDs.  Each bus leaves at time 0 and then at each multiple of it's ID.
        *  
        *  Part 1 -        What is the earliest bus to leave after the given time?  (Return id * how long after the time it is).
        *  Part 2 -        What is the earliest time on which bus 1 leaves at that time, bus 2 at that time + 1 etc?
    */

    public Day13(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("939;7,13,x,x,59,x,31,19");
                Expecteds.Add("295");
                break;
            case (1, false):
                AddInput("1001938;41,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,431,x,x,x,x,x,x,x,23,x,x,x,x,13,x,x,x,17,x,19,x,x,x,x,x,x,x,x,x,x,x,863,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29");
                Expecteds.Add("4315");
                break;
            case (2, true):
                Inputs = new() { "939;7,13,x,x,59,x,31,19", "0;17,x,13,19", "0;67,7,59,61", "0;67,x,7,59,61", "0;67,7,x,59,61", "0;1789,37,47,1889" };
                Expecteds = new() { "1068781", "3417", "754018", "779210", "1261476", "1202161486" };
                break;
            case (2, false):
                AddInput("1001938;41,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,431,x,x,x,x,x,x,x,23,x,x,x,x,13,x,x,x,17,x,19,x,x,x,x,x,x,x,x,x,x,x,863,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29");
                Expecteds.Add("556100168221141");
                break;
        }
    }
}
