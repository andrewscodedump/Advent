namespace Advent2015;

public partial class Day09 : Advent.Day
{
    /*
        *  Description -   Input is a set of distances between pairs of planets.  You need to visit each one once and once only.
        *                  
        *  Part 1 -        What is the length of the shortest route?
        *  Part 2 -        What is length the of the longest route?
    */

    public Day09(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("London to Dublin = 464;London to Belfast = 518;Dublin to Belfast = 141");
                Expecteds.Add("605");
                break;
            case (1, false):
                Inputs.Add("Tristram to AlphaCentauri = 34;Tristram to Snowdin = 100;Tristram to Tambi = 63;Tristram to Faerun = 108;Tristram to Norrath = 111;Tristram to Straylight = 89;Tristram to Arbre = 132;AlphaCentauri to Snowdin = 4;AlphaCentauri to Tambi = 79;AlphaCentauri to Faerun = 44;AlphaCentauri to Norrath = 147;AlphaCentauri to Straylight = 133;AlphaCentauri to Arbre = 74;Snowdin to Tambi = 105;Snowdin to Faerun = 95;Snowdin to Norrath = 48;Snowdin to Straylight = 88;Snowdin to Arbre = 7;Tambi to Faerun = 68;Tambi to Norrath = 134;Tambi to Straylight = 107;Tambi to Arbre = 40;Faerun to Norrath = 11;Faerun to Straylight = 66;Faerun to Arbre = 144;Norrath to Straylight = 115;Norrath to Arbre = 135;Straylight to Arbre = 127");
                Expecteds.Add("251");
                break;
            case (2, true):
                Inputs.Add("London to Dublin = 464;London to Belfast = 518;Dublin to Belfast = 141");
                Expecteds.Add("982");
                break;
            case (2, false):
                Inputs.Add("Tristram to AlphaCentauri = 34;Tristram to Snowdin = 100;Tristram to Tambi = 63;Tristram to Faerun = 108;Tristram to Norrath = 111;Tristram to Straylight = 89;Tristram to Arbre = 132;AlphaCentauri to Snowdin = 4;AlphaCentauri to Tambi = 79;AlphaCentauri to Faerun = 44;AlphaCentauri to Norrath = 147;AlphaCentauri to Straylight = 133;AlphaCentauri to Arbre = 74;Snowdin to Tambi = 105;Snowdin to Faerun = 95;Snowdin to Norrath = 48;Snowdin to Straylight = 88;Snowdin to Arbre = 7;Tambi to Faerun = 68;Tambi to Norrath = 134;Tambi to Straylight = 107;Tambi to Arbre = 40;Faerun to Norrath = 11;Faerun to Straylight = 66;Faerun to Arbre = 144;Norrath to Straylight = 115;Norrath to Arbre = 135;Straylight to Arbre = 127");
                Expecteds.Add("898");
                break;
        }
    }
}
