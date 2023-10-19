namespace Advent2020;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        Queue<int>[] hands = new Queue<int>[2] { new(), new() };
        int player = -1;
        foreach (string card in InputSplit)
            if (card.StartsWith("Player")) player++;
            else hands[player].Enqueue(int.Parse(card));

        int winner = PlayGame(hands);
        Output = hands[winner].Select((card, index) => (hands[winner].Count - index) * card).Sum().ToString();
    }

    private int PlayGame(Queue<int>[] hands)
    {
        int winner;
        string cardString = string.Join(',', hands[0]) + '|' + string.Join(',', hands[1]);
        HashSet<string> handsPlayed = new() { cardString };

        while (hands[0].Count > 0 && hands[1].Count > 0)
        {
            int[] nextCards = new int[2];
            for (int player = 0; player <= 1; player++)
                nextCards[player] = hands[player].Dequeue();
            winner = Part2 && nextCards[0] <= hands[0].Count && nextCards[1] <= hands[1].Count
                ? PlayGame(new Queue<int>[] { new(hands[0].ToArray()[0..nextCards[0]]), new(hands[1].ToArray()[0..nextCards[1]]) })
                : nextCards[0] > nextCards[1] ? 0 : 1;
            hands[winner].Enqueue(nextCards[winner]);
            hands[winner].Enqueue(nextCards[Math.Abs(winner - 1)]);
            cardString = string.Join(',', hands[0]) + '|' + string.Join(',', hands[1]);
            if (Part2 && handsPlayed.Contains(cardString))
                return 0;
            else
                handsPlayed.Add(cardString);
        }
        return hands[0].Count == 0 ? 1 : 0;
    }
}
