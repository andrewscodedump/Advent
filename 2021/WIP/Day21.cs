namespace Advent2021;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        int[] scores = new int[2] { 0, 0 };
        int sides = Part1 ? 100 : 3;
        int target = Part1 ? 1000 : 21;
        int[] positions = new int[2] { int.Parse(InputSplit[0].Split()[4]), int.Parse(InputSplit[1].Split()[4]) };
        int player = 2, totalRolls = 0;

        int nextRoll = 1;

        do
        {
            player = player - 1 + (player % 2 * 2);
            int score;
            if (nextRoll == sides-1)
            {
                score = (2 * nextRoll) + 2;
                nextRoll = 2;
            }
            else if(nextRoll == sides){
                score = nextRoll+3;
                nextRoll = 3;
            }
            else
            {
                score = (nextRoll * 3) + 3;
                nextRoll += 3;
                if (nextRoll == sides + 1) nextRoll = 1;
            }
            positions[player - 1] += score % 10;
            positions[player - 1] = positions[player - 1] % 10 == 0 ? 10 : positions[player - 1] % 10;
            scores[player - 1]+=positions[player - 1];
            totalRolls += 3;
        } while (scores[player-1] < target);
        int result = scores[player - 1 + (player % 2 * 2)-1] * totalRolls;
        Output = result.ToString();
    }
}
