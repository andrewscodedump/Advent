namespace Advent2016;

public partial class Day17 : Advent.Day
{
    /*
        *  Description -    You're in a 4x4 grid of rooms.  At each stage of your path through the grid, the state of the doors is given by the MD5 hash of the passkey (input) plus a LRUD string giving your path so far.
        *                   The first 4 characters of hash give the state of the UDLR doors; open if the character is b-f, closed otherwise.
        *  
        *  Part 1 -         What is the shortest path to reach the bottom right room?
        *  Part 2 -         What is the length of the longest path that will take you to that room?
    */
    public Day17(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "ihgpwlah", "kglvqrro", "ulqzkmiv" };
                Expecteds = new() { "DDRRRD", "DDUDRLRRUDRD", "DRURDRUDDLLDLUURRDULRLDUUDDDRR" };
                break;
            case (1, false):
                AddInput("yjjvjgan");
                Expecteds.Add("RLDRUDRDDR");
                break;
            case (2, true):
                Inputs = new() { "ihgpwlah", "kglvqrro", "ulqzkmiv" };
                Expecteds = new() { "370", "492", "830" };
                break;
            case (2, false):
                AddInput("yjjvjgan");
                Expecteds.Add("498");
                break;
        }
    }
}
