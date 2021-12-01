namespace Advent2017;

public partial class Day22 : Advent.Day
{
    /*
        *  Description -   Input is a grid which gives infection status of sectors in a disk.  # = infected, . = clean.  A virus is in the middle, pointing up.
        *                  At each turn, it checks the current location, turns left if it's infected, right if it's not then inverts the infection status.
        *  
        *  Part 1 -        How many times has the virus infected a sector after 10,000 steps?
        *  Part 2 -        The infection now cycles through four phases: clean, weakened, infected, flagged, with the virus turning L, F, R, B for each respectively.
        *                  How many times has the virus infected a sector after 10,000,000 steps?
    */
    public Day22(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("..#;#..;...");
                Expecteds.Add("5587");
                break;
            case (1, false):
                AddInput("#.#...###.#.##.#....##.##;..####.#.######....#....#;###..###.#.###.##.##..#.#;...##.....##.###.##.###..;....#...##.##..#....###..;##.#..###.#.###......####;#.#.###...###..#.#.#.#.#.;###...##..##..#..##......;##.#.####.#..###....#.###;.....#..###....######..##;.##.#.###....#..#####..#.;########...##.##....##..#;.#.###.##.#..#..#.#..##..;.#.##.##....##....##.#.#.;..#.#.##.#..##..##.#..#.#;.####..#..#.###..#..#..#.;#.#.##......##..#.....###;...####...#.#.##.....####;#..##..##..#.####.#.#..#.;#...###.##..###..#..#....;#..#....##.##.....###..##;#..##...#...##...####..#.;#.###..#.#####.#..#..###.;###.#...#.##..#..#...##.#;.#...#..#.#.#.##.####....");
                Expecteds.Add("2511944");
                break;
            case (2, true):
                AddInput("..#;#..;...");
                Expecteds.Add("5399");
                break;
            case (2, false):
                AddInput("#.#...###.#.##.#....##.##;..####.#.######....#....#;###..###.#.###.##.##..#.#;...##.....##.###.##.###..;....#...##.##..#....###..;##.#..###.#.###......####;#.#.###...###..#.#.#.#.#.;###...##..##..#..##......;##.#.####.#..###....#.###;.....#..###....######..##;.##.#.###....#..#####..#.;########...##.##....##..#;.#.###.##.#..#..#.#..##..;.#.##.##....##....##.#.#.;..#.#.##.#..##..##.#..#.#;.####..#..#.###..#..#..#.;#.#.##......##..#.....###;...####...#.#.##.....####;#..##..##..#.####.#.#..#.;#...###.##..###..#..#....;#..#....##.##.....###..##;#..##...#...##...####..#.;#.###..#.#####.#..#..###.;###.#...#.##..#..#...##.#;.#...#..#.#.#.##.####....");
                Expecteds.Add("2511776");
                break;
        }
    }
}
