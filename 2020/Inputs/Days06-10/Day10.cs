namespace Advent2020;

public partial class Day10 : Advent.Day
{
    /*
        *  Description -   Input is a list of outputs of a set of converter cables.  The starting power is 0, and each cable can increase it's input by up to 3.  Final output is largest cable +3.
        *  
        *  Part 1 -        If you use every adapter what is the number of +1 steps multiplied by the number of +3 steps?
        *  Part 2 -        How many different ways are there of arranging the cables to match the final output?
    */

    public Day10(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("16;10;15;5;1;11;7;19;6;12;4");
                Expecteds.Add("35");
                Inputs.Add("28;33;18;42;31;14;46;20;48;47;24;23;49;45;19;38;39;11;1;32;25;35;8;17;7;9;4;2;34;10;3");
                Expecteds.Add("220");
                break;
            case (1, false):
                Inputs.Add("128;6;152;16;118;94;114;3;146;44;113;83;46;40;37;72;149;155;132;9;75;1;82;80;111;124;66;122;129;32;30;136;112;65;90;117;11;45;161;55;135;17;159;38;51;131;12;123;81;64;50;43;19;63;13;153;110;27;23;104;145;18;125;86;10;76;26;142;59;47;160;79;139;54;121;97;162;36;107;56;25;99;24;31;69;137;33;138;130;158;91;2;74;101;73;20;98;154;89;62;100;39");
                Expecteds.Add("2232");
                break;
            case (2, true):
                Inputs.Add("16;10;15;5;1;11;7;19;6;12;4");
                Expecteds.Add("8");
                Inputs.Add("28;33;18;42;31;14;46;20;48;47;24;23;49;45;19;38;39;11;1;32;25;35;8;17;7;9;4;2;34;10;3");
                Expecteds.Add("19208");
                break;
            case (2, false):
                Inputs.Add("128;6;152;16;118;94;114;3;146;44;113;83;46;40;37;72;149;155;132;9;75;1;82;80;111;124;66;122;129;32;30;136;112;65;90;117;11;45;161;55;135;17;159;38;51;131;12;123;81;64;50;43;19;63;13;153;110;27;23;104;145;18;125;86;10;76;26;142;59;47;160;79;139;54;121;97;162;36;107;56;25;99;24;31;69;137;33;138;130;158;91;2;74;101;73;20;98;154;89;62;100;39");
                Expecteds.Add("173625106649344");
                break;
        }
    }
}
