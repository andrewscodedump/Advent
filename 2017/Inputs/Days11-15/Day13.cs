namespace Advent2017;

public partial class Day13 : Advent.Day
{
    /*
        *  Description -   Input is a list of scanners which move up and down a column (first number is the column position, second is the height), once a tick. 
        *                  You move along the first row once a tick, being caught if the scanner is in a square when you enter it.
        *  
        *  Part 1 -        Every time you are caught, you get a score equal to the column number times its height.  What is your total score?
        *  Part 2 -        By how many ticks do you have to delay your departure before you can get all the way through without being caught?
    */
    public Day13(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("0: 3;1: 2;4: 4;6: 4");
                Expecteds.Add("24");
                break;
            case (1, false):
                Inputs.Add("0: 4;1: 2;2: 3;4: 4;6: 6;8: 5;10: 6;12: 6;14: 6;16: 12;18: 8;20: 9;22: 8;24: 8;26: 8;28: 8;30: 12;32: 10;34: 8;36: 12;38: 10;40: 12;42: 12;44: 12;46: 12;48: 12;50: 14;52: 14;54: 12;56: 12;58: 14;60: 14;62: 14;66: 14;68: 14;70: 14;72: 14;74: 14;78: 18;80: 14;82: 14;88: 18;92: 17");
                Expecteds.Add("1580");
                break;
            case (2, true):
                Inputs.Add("0: 3;1: 2;4: 4;6: 4");
                Expecteds.Add("10");
                break;
            case (2, false):
                Inputs.Add("0: 4;1: 2;2: 3;4: 4;6: 6;8: 5;10: 6;12: 6;14: 6;16: 12;18: 8;20: 9;22: 8;24: 8;26: 8;28: 8;30: 12;32: 10;34: 8;36: 12;38: 10;40: 12;42: 12;44: 12;46: 12;48: 12;50: 14;52: 14;54: 12;56: 12;58: 14;60: 14;62: 14;66: 14;68: 14;70: 14;72: 14;74: 14;78: 18;80: 14;82: 14;88: 18;92: 17");
                Expecteds.Add("3943252");
                break;
        }
    }
}
