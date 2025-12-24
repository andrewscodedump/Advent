namespace Advent2015;

public partial class Day19 : Advent.Day
{
    (string from, string to)[] convs = [];

    public override void DoWork()
    {
        convs = [.. Inputs[..^1].Select(i => i.Split(" => ")).Select(i => (i[0], i[1]))];
        Output = Part1 ? DoPart1() : DoPart2();
    }  

    private string DoPart1()
    {
        string[] targetMolecule = [.. Regex.Matches(Inputs[^1], "[A-Z][a-z]|[A-Z](?![a-z])|[A-Z]$").Select(m => m.Value)];
        HashSet<string> outputs = [];

        for (int pos = 0; pos < targetMolecule.Length; pos++)
            foreach ((string from, string to) in convs.Where(c => c.from == targetMolecule[pos]))
                outputs.Add(string.Join("", [.. targetMolecule[..pos], to, .. targetMolecule[(pos + 1)..]]));

        return outputs.Count.ToString();
    }

    private string DoPart2()
    {
        // This is horrible, and it really shouldn't work as well as it does.
        string molecule;
        string targetMolecule = Inputs[^1];
        int moves;
        int bestMoves = int.MaxValue;

        int sequenceTarget = 10;
        int sequenceLength = 0;
        int numTries = 0;
        int prevBest = 0;
        int randomTries = 1;

        bool endLoop;
        do
        {
            do
            {
                do
                {
                    moves = 0;
                    molecule = targetMolecule;
                    int pos = -1;
                    do
                    {
                        //Pick a conv at random
                        RandomizeArray(convs);
                        for (int i = 0; i < convs.Length; i++)
                        {
                            string atom = convs[i].from;
                            string after = convs[i].to;
                            //Find the first occurrence and replace it
                            pos = molecule.IndexOf(after);
                            if (pos == -1)
                                continue;
                            moves++;
                            molecule = molecule[..pos] + atom + molecule[(pos + after.Length)..];
                            break;
                        }
                        //Loop until none found
                    } while (pos != -1);

                    //Loop until e
                } while (molecule != "e");
                bestMoves = Math.Min(moves, bestMoves);
                numTries++;
            } while (numTries < randomTries);

            endLoop = false;
            if (sequenceLength == 0 || bestMoves == prevBest)
            {
                // Try a few times, and if they all give the same best answer, assume it's the right one
                prevBest = bestMoves;
                sequenceLength++;
                if (sequenceLength == sequenceTarget)
                    endLoop = true;
                else
                {
                    bestMoves = int.MaxValue;
                    numTries = 0;
                }
            }
            else
            {
                // We haven't found a stable answer, so double the amount of random shuffles to try each time.
                sequenceLength = 0;
                numTries = 0;
                prevBest = 0;
                bestMoves = int.MaxValue;
                randomTries *= 2;
            }
        } while (!endLoop);
        return bestMoves.ToString();
    }

    protected static void RandomizeArray<T>(T[] listIn)
    {
        Random rnd = new();
        for (int pos = 0; pos < listIn.Length; pos++)
        {
            int swapPos = rnd.Next(pos, listIn.Length);
            (listIn[swapPos], listIn[pos]) = (listIn[pos], listIn[swapPos]);
        }
    }
}
