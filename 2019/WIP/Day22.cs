namespace Advent2019;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        long bigCards = TestMode ? 10 : Part1 ? 10007 : 119315717514047;
        List<(string, int)> shuffles = [];
        long testCard = TestMode ? 9 : Part1 ? 2019 : 2020;
        long testPos, numShuffles = Part1 ? 1 : 101741582076661;
        long targetCardPos = testCard;
        int shufflesDone = 0;

        foreach (string input in Inputs)
        {
            string shuffle = input.Replace("deal with increment", "inc");
            shuffle = shuffle.Replace("deal into new stack", "new 0");

            shuffles.Add((shuffle.Split(' ')[0], int.Parse(shuffle.Split(' ')[1])));
        }

        #endregion Setup Variables and Parse Inputs

        #region Naive Solution
        /*
        int numCards = TestMode ? 10 : 10007;
        List<int> pack = Enumerable.Range(0,numCards).ToList();
        List<int> newPack = new List<int>();
        foreach ((string action, int number) in shuffles)
        {
            newPack = new List<int>();
            switch (action)
            {
                case "inc":
                    int[] temp = new int[numCards];
                    for (int i = 0; i < numCards; i++)
                    {
                        temp[(i * number) % numCards] = pack[i];
                    }
                    newPack.AddRange(temp.ToArray<int>());
                    break;
                case "new":
                    for (int i = numCards - 1; i >= 0; i--)
                    {
                        newPack.Add(pack[i]);
                    }
                    break;
                case "cut":
                    if (number == 0)
                    {
                        newPack.AddRange(pack);
                    }
                    else if (number > 0)
                    {
                        newPack.AddRange(pack.GetRange(number, numCards - number));
                        newPack.AddRange(pack.GetRange(0, number));
                    }
                    else
                    {
                        newPack.AddRange(pack.GetRange(numCards + number, -number));
                        newPack.AddRange(pack.GetRange(0, numCards + number));
                    }
                    break;
                default:
                    break;
            }
            pack = new List<int>(newPack);
        }
        testPos = pack.FindIndex(x => x == testCard);
        */

        #endregion Naive Solution

        do
        {
            shufflesDone++;
            foreach ((string action, int number) in shuffles)
            {
                switch (action)
                {
                    case "inc":
                        targetCardPos = targetCardPos * number % bigCards;
                        break;
                    case "new":
                        targetCardPos = bigCards - targetCardPos - 1;
                        break;
                    case "cut":
                        if (number > 0)
                        {
                            if (targetCardPos > number)
                                targetCardPos -= number;
                            else
                                targetCardPos += bigCards - number;
                        }
                        else if (number < 0)
                        {
                            if (targetCardPos < bigCards + number)
                                targetCardPos -= number;
                            else
                                targetCardPos -= number + bigCards;
                        }
                        break;
                    default:
                        break;
                }
            }
        } while (shufflesDone < numShuffles && targetCardPos != testCard);
        testPos = targetCardPos;

        Output = Part1 ? testPos.ToString() : "";

        #region Private Classes and Methods

        #endregion Private Classes and Methods
    }
}
