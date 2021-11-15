namespace Advent2016;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        string message = string.Empty;
        int messageLength = InputSplit[0].Length;
        int numberOfMessages = InputSplit.Length;

        string[][] test = new string[messageLength][];

        for (int pos = 0; pos < messageLength; pos++)
            test[pos] = new string[numberOfMessages];

        for (int i = 0; i < numberOfMessages; i++)
            for (int pos = 0; pos < messageLength; pos++)
                test[pos][i] = InputSplit[i].Substring(pos, 1);

        for (int pos = 0; pos < messageLength; pos++)
        {
            string currentLetter = "|";
            string bestLetter = "|";
            int currentCount = WhichPart == 1 ? 0 : int.MaxValue;
            int bestCount = WhichPart == 1 ? 0 : int.MaxValue;
            Array.Sort(test[pos]);
            for (int i = 0; i < numberOfMessages; i++)
            {
                string newLetter = test[pos][i];
                if (newLetter != currentLetter)
                {
                    if ((WhichPart == 1 && currentCount > bestCount)
                        || (WhichPart == 2 && currentCount < bestCount))
                    {
                        bestLetter = currentLetter;
                        bestCount = currentCount;
                    }
                    currentCount = 1;
                    currentLetter = newLetter;
                }
                else
                    currentCount++;

                if (i == numberOfMessages - 1 &&
                    ((WhichPart == 1 && currentCount > bestCount)
                    ||  (WhichPart == 2 && currentCount < bestCount)))
                {
                    bestLetter = currentLetter;
                    bestCount = currentCount;
                }
            }
            message += bestLetter;
        }
        Output = message.ToString();
    }
}
