namespace Advent2020;

public partial class Day22 : Advent.Day
{
    /*
        *  Description -   Input is the starting position for a game of cards.  Each player plays their top card and the winner adds the two to the bottom of their pile.
        *  
        *  Part 1 -        What is the sum of (position * card) after someone has all cards?
        *  Part 2 -        As above, but with recursive mini games under certain circumstances.
    */

    public Day22(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Player 1:;9;2;6;3;1;;Player 2:;5;8;4;7;10");
                Expecteds.Add("306");
                break;
            case (1, false):
                AddInput("Player 1:;7;1;9;10;12;4;38;22;18;3;27;31;43;33;47;42;21;24;50;39;8;6;16;46;11;;Player 2:;49;41;40;35;44;29;30;19;14;2;34;17;25;5;15;32;20;48;45;26;37;28;36;23;13");
                Expecteds.Add("33403");
                break;
            case (2, true):
                AddInput("Player 1:;9;2;6;3;1;;Player 2:;5;8;4;7;10");
                Expecteds.Add("291");
                break;
            case (2, false):
                AddInput("Player 1:;7;1;9;10;12;4;38;22;18;3;27;31;43;33;47;42;21;24;50;39;8;6;16;46;11;;Player 2:;49;41;40;35;44;29;30;19;14;2;34;17;25;5;15;32;20;48;45;26;37;28;36;23;13");
                Expecteds.Add("29177");
                break;
        }
    }
}
