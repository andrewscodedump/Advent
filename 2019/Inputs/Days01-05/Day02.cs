namespace Advent2019;

public partial class Day02 : Advent.Day
{
    /*
        *  Description -   Input is the code for a computer program
        *                  Each quartet of integers is an instruction.
        *                  First number gives the operation (1=add, 2=multiply)
        *                  Second and third give the addresses of the values to be operated on
        *                  Fourth gives the address to write the result to
        *                  Execution ends when the operation is 99
        *  
        *  Part 1 -        (Test Mode) - What are the contents of register 0 after the program is run
        *                  (Live Mode) - As above, but set register 1 to 12 and register 2 to 2 before running.
        *  Part 2 -        What values of registers 1 and 2 give an output of 19690720?
    */
    public Day02(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        //BatchStatus = DayBatchStatus.NotDoneYet;
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("1,9,10,3,2,3,11,0,99,30,40,50");
                Expecteds.Add("3500");
                AddInput("1,0,0,0,99");
                Expecteds.Add("2");
                AddInput("2,3,0,3,99");
                Expecteds.Add("2");
                AddInput("2,4,4,5,99,0");
                Expecteds.Add("2");
                AddInput("1,1,1,4,99,5,6,0,99");
                Expecteds.Add("30");
                break;
            case (1, false):
                AddInput("1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,5,19,23,1,23,5,27,1,27,13,31,1,31,5,35,1,9,35,39,2,13,39,43,1,43,10,47,1,47,13,51,2,10,51,55,1,55,5,59,1,59,5,63,1,63,13,67,1,13,67,71,1,71,10,75,1,6,75,79,1,6,79,83,2,10,83,87,1,87,5,91,1,5,91,95,2,95,10,99,1,9,99,103,1,103,13,107,2,10,107,111,2,13,111,115,1,6,115,119,1,119,10,123,2,9,123,127,2,127,9,131,1,131,10,135,1,135,2,139,1,10,139,0,99,2,0,14,0");
                Expecteds.Add("3562624");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,5,19,23,1,23,5,27,1,27,13,31,1,31,5,35,1,9,35,39,2,13,39,43,1,43,10,47,1,47,13,51,2,10,51,55,1,55,5,59,1,59,5,63,1,63,13,67,1,13,67,71,1,71,10,75,1,6,75,79,1,6,79,83,2,10,83,87,1,87,5,91,1,5,91,95,2,95,10,99,1,9,99,103,1,103,13,107,2,10,107,111,2,13,111,115,1,6,115,119,1,119,10,123,2,9,123,127,2,127,9,131,1,131,10,135,1,135,2,139,1,10,139,0,99,2,0,14,0");
                Expecteds.Add("8298");
                break;
        }
    }
}
