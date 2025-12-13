namespace Advent2024;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        bool doingRules = true;
        List<(long, long)> rules = [];
        List<List<long>> updates = [];
        long result = 0;

        foreach (string line in Inputs)
        {
            if (string.IsNullOrEmpty(line))
                doingRules = false;
            else if (doingRules)
                rules.Add((int.Parse(line.Split('|')[0]), int.Parse(line.Split('|')[1])));
            else
                updates.Add(line.Split(',').Select(long.Parse).ToList());
        }

        foreach (List<long> update in updates)
        {
            List<long> fixedUpdate = FixUpdate(update, rules);
            bool changesMade = !fixedUpdate.SequenceEqual(update);
            if ((Part1 && !changesMade) || (Part2 && changesMade))
                result += fixedUpdate[fixedUpdate.Count / 2];
        }

        Output = result.ToString();
    }

    private static List<long> FixUpdate(List<long> update, List<(long, long)> rules)
    {
        List<long> fixedUpdate = [];
        List<long> remainingPages = [.. update];
        List<(long, long)> localRules = [.. rules];
        int pointer = 0;

        // Remove all rules where either side does not appear in the update
        localRules.RemoveAll((x) => !update.Contains(x.Item1) || !update.Contains(x.Item2));

        do
        {
            long next = 0;
            // if pointer at end, get next from remaining pages
            if (pointer == fixedUpdate.Count)
            {
                next = remainingPages[0];
            }
            // get first page from any rules where second page is the current item
            else if (localRules.Exists(x => x.Item2 == fixedUpdate[pointer]))
            {
                next = localRules.First(x => x.Item2 == fixedUpdate[pointer]).Item1;
            }
            if (next > 0)
            {
                // insert next page at pointer, remove all rules beginning with it and remove it from remaining pages
                fixedUpdate = [.. fixedUpdate[..pointer], next, .. fixedUpdate[pointer..]];
                localRules.RemoveAll(r => r.Item1 == next);
                remainingPages.RemoveAll(p => p == next);
                continue;
            }
            pointer++;
        } while (remainingPages.Count > 0);

        return fixedUpdate;
    }
}