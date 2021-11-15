namespace Advent2018;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        StringBuilder order = new();
        int timeTaken = 0;
        int secondsPerStep = TestMode ? 0 : 60;
        int numWorkers = WhichPart == 1 ? 1 : TestMode ? 2 : 5;
        Dictionary<char, List<char>> allSteps = new();
        SortedSet<char> availSteps = new();
        Dictionary<char, int> inProgress = new();
        Queue<int> availWorkers = new(Enumerable.Range(0, numWorkers));

        foreach (string instruction in InputSplit)
        {
            char precursor = char.Parse(instruction.Split(' ')[1]), dependent = char.Parse(instruction.Split(' ')[7]);
            if (allSteps.ContainsKey(dependent))
            {
                allSteps[dependent].Add(precursor);
                availSteps.Remove(dependent);
            }
            else
                allSteps.Add(dependent, new List<char> { precursor });
            if (!allSteps.ContainsKey(precursor))
            {
                allSteps.Add(precursor, new List<char>());
                availSteps.Add(precursor);
            }
        }
        #endregion Setup Variables and Parse Inputs

        do
        {
            timeTaken++;
            while (availSteps.Count > 0 && availWorkers.Count > 0)
            {
                char nextItem = availSteps.First();
                order.Append(nextItem);
                availSteps.Remove(nextItem);
                inProgress.Add(nextItem, nextItem - 64 + secondsPerStep);
                availWorkers.Dequeue();
            }

            foreach (char ipItem in inProgress.Keys.ToList())
            {
                inProgress[ipItem] -= 1;
                if (inProgress[ipItem] == 0)
                {
                    inProgress.Remove(ipItem);
                    availWorkers.Enqueue(0);
                    foreach (char item in allSteps.Keys)
                        if (allSteps[item].Contains(ipItem))
                        {
                            allSteps[item].Remove(ipItem);
                            if (allSteps[item].Count == 0)
                                availSteps.Add(item);
                        }
                }
            }
        } while (availSteps.Count > 0 || inProgress.Count > 0);

        Output = WhichPart == 1 ? order.ToString() : timeTaken.ToString();
    }
}
