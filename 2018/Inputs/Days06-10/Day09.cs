namespace Advent2018;

public partial class Day09 : Advent.Day
{
    /*
        *  Description -   Input is the number of players and the maximum number of marbles played in an Elvish game
        *                  Marbles are placed sequentially in a circle, each new one being placed two clockwise from the latest one
        *                  If the marble number is divisible by 23, it is not played, and its number is added to that elf's score,
        *                  along with the number of the one seven anti-clockwise from the one last placed (which is then removed).
        *  
        *  Part 1 -        After all marbles have been played, what is the highest score?
        *  Part 2 -        As above, but using 100 times as many marbles.
    */
    public Day09(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("9 players; last marble is worth 32 points");
                AddInput("30 players; last marble is worth 5807 points");
                Expecteds.Add("32");
                Expecteds.Add("37305");
                break;
            case (1, false):
                AddInput("473 players; last marble is worth 70904 points");
                Expecteds.Add("371284");
                break;
            case (2, true):
                AddInput("30 players; last marble is worth 5807 points");
                Expecteds.Add("320997431");
                break;
            case (2, false):
                AddInput("473 players; last marble is worth 70904 points");
                Expecteds.Add("3038972494");
                break;
        }
    }
}
