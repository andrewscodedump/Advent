namespace Advent2023;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        List<Hand> game = [.. Inputs.Select(x => new Hand(x, Part2)).Order()];
        for (int i = 0; i < game.Count; i++)
            result += game[i].Bid * (i + 1);
        Output = result.ToString();
    }

    private sealed class Hand() : IComparable<Hand>
    {
        public Hand(string Input, bool useJokers) : this()
        {
            Cards = Input.Split(' ')[0];
            Bid = Convert.ToInt32(Input.Split(' ')[1]);
            UseJokers = useJokers;
            Type = GetType(Cards);
        }

        private HandType GetType(string cards)
        {
            string allCards = "AKQJT98765432";
            HandType type = HandType.HighCard;
            foreach (char card in allCards)
            {
                if (UseJokers && card == 'J') continue;
                int count = cards.Count(c => c == card);
                if (count == 5) return HandType.Five;
                if (count == 4) type = HandType.Four;
                if (count == 3 && type == HandType.Pair) return HandType.FullHouse;
                if (count == 2 && type == HandType.Prile) return HandType.FullHouse;
                if (count == 2 && type == HandType.Pair) { type = HandType.TwoPair; continue; }
                if (count == 3) type = HandType.Prile;
                if (count == 2) type = HandType.Pair;
            }
            if (UseJokers)
            {
                type = (type, cards.Count(c => c == 'J')) switch
                {
                    (_, 0) => type,
                    (HandType.HighCard, 1) => HandType.Pair,
                    (HandType.Pair, 1) => HandType.Prile,
                    (HandType.Prile, 1) => HandType.Four,
                    (HandType.Four, 1) => HandType.Five,
                    (HandType.TwoPair, 1) => HandType.FullHouse,
                    (HandType.HighCard, 2) => HandType.Prile,
                    (HandType.Pair, 2) => HandType.Four,
                    (HandType.Prile, 2) => HandType.Five,
                    (HandType.HighCard, 3) => HandType.Four,
                    (HandType.Pair, 3) => HandType.Five,
                    _ => HandType.Five,
                };
            }
            return type;
        }
        public string Cards { get; private set; }
        public int Bid { get; private set; }
        public HandType Type { get; set; }
        public bool UseJokers { get; private set; }

        public int CompareTo(Hand other)
        {
            string cardOrder = UseJokers ? "J23456789TQKA" : "23456789TJQKA";
            if ((int)Type > (int)other.Type) return 1;
            if ((int)Type < (int)other.Type) return -1;
            for (int i = 0; i < Cards.Length; i++)
            {
                if (cardOrder.IndexOf(Cards[i]) > cardOrder.IndexOf(other.Cards[i])) return 1;
                if (cardOrder.IndexOf(Cards[i]) < cardOrder.IndexOf(other.Cards[i])) return -1;
            }
            return 0;
        }
    }

    private enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        Prile,
        FullHouse,
        Four,
        Five
    }
}
