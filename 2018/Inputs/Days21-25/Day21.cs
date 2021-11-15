namespace Advent2018;

public partial class Day21 : Advent.Day
{
    /*
        *  Description -   Input is a set of code which seems to loop infinitely.
        *  
        *  Part 1 -        What is the lowest value that can be put into register zero at the beginning
        *                  that will allow the code to complete in the lowest possible number of operations?
        *  Part 2 -        What is the lowest value that will allow the code to complete in the highest possible
        *                  number of operations?
    */
    public Day21(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (1, false):
                Inputs.Add("#ip 4;seti 123 0 1;bani 1 456 1;eqri 1 72 1;addr 1 4 4;seti 0 0 4;seti 0 6 1;bori 1 65536 3;seti 6780005 8 1;bani 3 255 2;addr 1 2 1;bani 1 16777215 1;muli 1 65899 1;bani 1 16777215 1;gtir 256 3 2;addr 2 4 4;addi 4 1 4;seti 27 5 4;seti 0 5 2;addi 2 1 5;muli 5 256 5;gtrr 5 3 5;addr 5 4 4;addi 4 1 4;seti 25 4 4;addi 2 1 2;seti 17 7 4;setr 2 1 3;seti 7 3 4;eqrr 1 0 2;addr 2 4 4;seti 5 4 4");
                Expecteds.Add("2525738");
                break;
            case (2, false):
                Inputs.Add("#ip 4;seti 123 0 1;bani 1 456 1;eqri 1 72 1;addr 1 4 4;seti 0 0 4;seti 0 6 1;bori 1 65536 3;seti 6780005 8 1;bani 3 255 2;addr 1 2 1;bani 1 16777215 1;muli 1 65899 1;bani 1 16777215 1;gtir 256 3 2;addr 2 4 4;addi 4 1 4;seti 27 5 4;seti 0 5 2;addi 2 1 5;muli 5 256 5;gtrr 5 3 5;addr 5 4 4;addi 4 1 4;seti 25 4 4;addi 2 1 2;seti 17 7 4;setr 2 1 3;seti 7 3 4;eqrr 1 0 2;addr 2 4 4;seti 5 4 4");
                Expecteds.Add("11316540");
                break;
        }
    }
}
