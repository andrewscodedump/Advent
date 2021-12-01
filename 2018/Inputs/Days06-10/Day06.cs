namespace Advent2018;

public partial class Day06 : Advent.Day
{
    /*
        *  Description -   Input is a series of coordinates of locations in a plane
        *                  
        *  Part 1 -        For every point in the plane, find the Manhattan distance of the nearest listed location (if there is a draw, ignore it)
        *                  What is the largest non-infinite area belonging to a single location
        *  Part 2 -        For every point in the plane, add up the Manhattan distance of every listed location
        *                  What is the largest area where the total distance is less than 10000 (32 for test).
    */
    public Day06(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("1, 1;1, 6;8, 3;3, 4;5, 5;8, 9");
                Expecteds.Add("17");
                break;
            case (1, false):
                AddInput("292, 73;204, 176;106, 197;155, 265;195, 59;185, 136;54, 82;209, 149;298, 209;274, 157;349, 196;168, 353;193, 129;94, 137;177, 143;196, 357;272, 312;351, 340;253, 115;109, 183;252, 232;193, 258;242, 151;220, 345;336, 348;196, 203;122, 245;265, 189;124, 57;276, 204;309, 125;46, 324;345, 228;251, 134;231, 117;88, 112;256, 229;49, 201;142, 108;150, 337;134, 109;288, 67;297, 231;310, 131;208, 255;246, 132;232, 45;356, 93;356, 207;83, 97");
                Expecteds.Add("4976");
                break;
            case (2, true):
                AddInput("1, 1;1, 6;8, 3;3, 4;5, 5;8, 9");
                Expecteds.Add("16");
                break;
            case (2, false):
                AddInput("292, 73;204, 176;106, 197;155, 265;195, 59;185, 136;54, 82;209, 149;298, 209;274, 157;349, 196;168, 353;193, 129;94, 137;177, 143;196, 357;272, 312;351, 340;253, 115;109, 183;252, 232;193, 258;242, 151;220, 345;336, 348;196, 203;122, 245;265, 189;124, 57;276, 204;309, 125;46, 324;345, 228;251, 134;231, 117;88, 112;256, 229;49, 201;142, 108;150, 337;134, 109;288, 67;297, 231;310, 131;208, 255;246, 132;232, 45;356, 93;356, 207;83, 97");
                Expecteds.Add("46462");
                break;
        }
    }
}
