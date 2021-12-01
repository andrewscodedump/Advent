namespace Advent2020;

public partial class Day23 : Advent.Day
{
    /*
        *  Description -   Input is the starting order for a game of cups.  Starting at position 1, move the 3 clockwise from it to the cup with value one less than it
        *                  (subtract 1 if the target isn't still on the table).
        *  
        *  Part 1 -        Starting from cup 1 (but not including it), what is the order of the cups after 10 rounds?
        *  Part 2 -        Start with 1M cups (all but the seed just consecutive numbers).
        *                  What is the multiple of the two cups to the left of cup 1 after 10M rounds?
    */
    public Day23(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "389125467;10", "389125467;100" };
                Expecteds = new() { "92658374", "67384529" };
                break;
            case (1, false):
                AddInput("974618352;100");
                Expecteds.Add("75893264");
                break;
            case (2, true):
                AddInput("389125467;10000000");
                Expecteds.Add("149245887792");
                break;
            case (2, false):
                AddInput("974618352;10000000");
                Expecteds.Add("38162588308");
                break;
        }
    }
}
