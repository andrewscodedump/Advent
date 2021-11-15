namespace Advent2018;

public partial class Day19 : Advent.Day
{
    /*
        *  Description -   Input is a program in machine code.
        *  
        *  Part 1 -        What is the result of running the code?
        *  Part 2 -        As above, but with register zero starting off with a value of 1 instead of 0.
    */
    public Day19(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("#ip 0;seti 5 0 1;seti 6 0 2;addi 0 1 0;addr 1 2 3;setr 1 0 0;seti 8 0 4;seti 9 0 5");
                Expecteds.Add("6");
                break;
            case (1, false):
                Inputs.Add("#ip 3;addi 3 16 3;seti 1 2 5;seti 1 3 2;mulr 5 2 1;eqrr 1 4 1;addr 1 3 3;addi 3 1 3;addr 5 0 0;addi 2 1 2;gtrr 2 4 1;addr 3 1 3;seti 2 5 3;addi 5 1 5;gtrr 5 4 1;addr 1 3 3;seti 1 2 3;mulr 3 3 3;addi 4 2 4;mulr 4 4 4;mulr 3 4 4;muli 4 11 4;addi 1 6 1;mulr 1 3 1;addi 1 21 1;addr 4 1 4;addr 3 0 3;seti 0 3 3;setr 3 4 1;mulr 1 3 1;addr 3 1 1;mulr 3 1 1;muli 1 14 1;mulr 1 3 1;addr 4 1 4;seti 0 3 0;seti 0 7 3");
                Expecteds.Add("1056");
                break;
            case (2, true):
                Inputs.Add("#ip 0;seti 5 0 1;seti 6 0 2;addi 0 1 0;addr 1 2 3;setr 1 0 0;seti 8 0 4;seti 9 0 5");
                Expecteds.Add("6");
                break;
            case (2, false):
                Inputs.Add("#ip 3;addi 3 16 3;seti 1 2 5;seti 1 3 2;mulr 5 2 1;eqrr 1 4 1;addr 1 3 3;addi 3 1 3;addr 5 0 0;addi 2 1 2;gtrr 2 4 1;addr 3 1 3;seti 2 5 3;addi 5 1 5;gtrr 5 4 1;addr 1 3 3;seti 1 2 3;mulr 3 3 3;addi 4 2 4;mulr 4 4 4;mulr 3 4 4;muli 4 11 4;addi 1 6 1;mulr 1 3 1;addi 1 21 1;addr 4 1 4;addr 3 0 3;seti 0 3 3;setr 3 4 1;mulr 1 3 1;addr 3 1 1;mulr 3 1 1;muli 1 14 1;mulr 1 3 1;addr 4 1 4;seti 0 3 0;seti 0 7 3");
                Expecteds.Add("10915260");
                break;
        }
    }
}
