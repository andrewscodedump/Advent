namespace Advent2018;

public partial class Day14 : Advent.Day
{
    /*
        *  Description -   A list of scores for recipes is being created, starting with 3 and 7.
        *                  Elf 1 starts on the first number, elf 2 on the second.  The scores to be added to the end of the list are the
        *                  first and second digits of the 2 numbers currently being looked at.
        *                  Each elf then moves on to another number in the list by a convoluted algorithm.
        *  
        *  Part 1 -        After a number of scores equal to the input have been added, what are the next 10 scores?
        *  Part 2 -        What is the starting position of a sequence of scores equal to the input numbers?
    */
    public Day14(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("9");
                AddInput("2018");
                Expecteds.Add("5158916779");
                Expecteds.Add("5941429882");
                break;
            case (1, false):
                AddInput("990941");
                Expecteds.Add("3841138812");
                break;
            case (2, true):
                AddInput("51589");
                AddInput("59414");
                Expecteds.Add("9");
                Expecteds.Add("2018");
                break;
            case (2, false):
                AddInput("990941");
                Expecteds.Add("20200561");
                break;
        }
    }
}
