namespace Advent2017;

public partial class Day18 : Advent.Day
{
    /*
        *  Description -   Input is a set of instructions to act on a collection of registers (all start at 0).
        *                  Step through the instructions until the requirement is met or a program steps out of the list of instructions.
        *  
        *  Part 1 -        What was is the first value rcv'd?
        *  Part 2 -        Two programs running simultaneously on the same instructions (register p starts at 0 for the first, 1 for the second).
        *                  snd queues a value for the other program, rcv dequeues a previously queued value.
        *                  End if both programs are waiting on a rcv or both step out of bounds.  How many snd's does program 1 do?
    */
    public Day18(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("set a 1;add a 2;mul a a;mod a 5;snd a;set a 0;rcv a;jgz a -1;set a 1;jgz a -2;");
                Expecteds.Add("4");
                break;
            case (1, false):
                AddInput("set i 31;set a 1;mul p 17;jgz p p;mul a 2;add i -1;jgz i -2;add a -1;set i 127;set p 316;mul p 8505;mod p a;mul p 129749;add p 12345;mod p a;set b p;mod b 10000;snd b;add i -1;jgz i -9;jgz a 3;rcv b;jgz b -1;set f 0;set i 126;rcv a;rcv b;set p a;mul p -1;add p b;jgz p 4;snd a;set a b;jgz 1 3;snd b;set f 1;add i -1;jgz i -11;snd a;jgz f -16;jgz a -19");
                Expecteds.Add("2951");
                break;
            case (2, true):
                AddInput("snd 1;snd 2;snd p;rcv a;rcv b;rcv c;rcv d");
                Expecteds.Add("3");
                break;
            case (2, false):
                AddInput("set i 31;set a 1;mul p 17;jgz p p;mul a 2;add i -1;jgz i -2;add a -1;set i 127;set p 316;mul p 8505;mod p a;mul p 129749;add p 12345;mod p a;set b p;mod b 10000;snd b;add i -1;jgz i -9;jgz a 3;rcv b;jgz b -1;set f 0;set i 126;rcv a;rcv b;set p a;mul p -1;add p b;jgz p 4;snd a;set a b;jgz 1 3;snd b;set f 1;add i -1;jgz i -11;snd a;jgz f -16;jgz a -19");
                Expecteds.Add("7366");
                break;
        }
    }
}
