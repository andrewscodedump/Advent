namespace Advent2017;

public partial class Day24 : Advent.Day
{
    /*
        *  Description -   Input is a list of parts to build a bridge with.  The bridge is built domino-wise, starting with 0 on this end.
        *                  A bridge's length is the number of parts it is made from, and its strength is the sum of the numbers in the parts it is made from.
        *  
        *  Part 1 -        What is the strongest bridge that can be made from the parts given?
        *  Part 2 -        What is the strength of the longest bridge that can be made (if there's a draw for length, take the strongest one)?
    */
    public Day24(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("0/2;2/2;2/3;3/4;3/5;0/1;10/1;9/10");
                Expecteds.Add("31");
                break;
            case (1, false):
                AddInput("32/31;2/2;0/43;45/15;33/24;20/20;14/42;2/35;50/27;2/17;5/45;3/14;26/1;33/38;29/6;50/32;9/48;36/34;33/50;37/35;12/12;26/13;19/4;5/5;14/46;17/29;45/43;5/0;18/18;41/22;50/3;4/4;17/1;40/7;19/0;33/7;22/48;9/14;50/43;26/29;19/33;46/31;3/16;29/46;16/0;34/17;31/7;5/27;7/4;49/49;14/21;50/9;14/44;29/29;13/38;31/11");
                Expecteds.Add("1511");
                break;
            case (2, true):
                AddInput("0/2;2/2;2/3;3/4;3/5;0/1;10/1;9/10");
                Expecteds.Add("19");
                break;
            case (2, false):
                AddInput("32/31;2/2;0/43;45/15;33/24;20/20;14/42;2/35;50/27;2/17;5/45;3/14;26/1;33/38;29/6;50/32;9/48;36/34;33/50;37/35;12/12;26/13;19/4;5/5;14/46;17/29;45/43;5/0;18/18;41/22;50/3;4/4;17/1;40/7;19/0;33/7;22/48;9/14;50/43;26/29;19/33;46/31;3/16;29/46;16/0;34/17;31/7;5/27;7/4;49/49;14/21;50/9;14/44;29/29;13/38;31/11");
                Expecteds.Add("1471");
                break;
        }
    }
}
