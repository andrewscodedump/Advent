namespace Advent2021;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        long[] scores = [0, 0];
        int sides = Part1 ? 100 : 3;
        int target = Part1 ? 1000 : 21;
        long[] positions = [InputNumbers[0][1], InputNumbers[1][1]];
        long player = 2, totalRolls = 0;

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
        long result = scores[player - 1 + (player % 2 * 2)-1] * totalRolls;
        Output = result.ToString();
    }
}
