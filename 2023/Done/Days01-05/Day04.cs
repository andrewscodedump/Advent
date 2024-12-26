namespace Advent2023;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int points = 0, numCards = InputNumbers.Length;
        Dictionary<int, int> cards = Enumerable.Range(0, numCards).ToDictionary(k => k, v => 1);
        int divider = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries).TakeWhile(i => i != "|").Count() - 1;

        for (int i = 0; i < numCards; i++)
        {
            long[] winning = InputNumbers[i][1..divider]; long[] numbers = InputNumbers[i][divider..];
            int matches = winning.Intersect(numbers).Count();
            points += matches == 0 ? 0 : Convert.ToInt32(Math.Pow(2, matches - 1));
            for (int j = i + 1; j <= i + matches && j < numCards; j++)
            {
                cards[j] += cards[i];
            }
        }
        Output = (Part1 ? points : cards.Values.Sum()).ToString();
    }
}
