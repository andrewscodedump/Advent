namespace Advent2015;

public partial class Day14 : Advent.Day
{
    /*
        *  Description -   The Input is a list of capabilities of the reindeer in the Reindeer Olympics.
        *                  
        *  Part 1 -        How far has the wining reindeer travelled after 2503 seconds (1000 seconds for the test data)?
        *  Part 2 -        Each reindeer is awarded a point for each second he is in the lead.  How many points does the winning reindeer have?
    */

    public Day14(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.;Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.");
                Expecteds.Add("1120");
                break;
            case (1, false):
                AddInput("Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.;Cupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.;Prancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.;Donner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.;Dasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.;Comet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.;Blitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.;Vixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.;Dancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.");
                Expecteds.Add("2696");
                break;
            case (2, true):
                AddInput("Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.;Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.");
                Expecteds.Add("689");
                break;
            case (2, false):
                AddInput("Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.;Cupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.;Prancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.;Donner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.;Dasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.;Comet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.;Blitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.;Vixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.;Dancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.");
                Expecteds.Add("1084");
                break;
        }
    }
}
